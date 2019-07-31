#region License and Terms
// MoreLINQ - Extensions to LINQ to Objects
// Copyright (c) 2009 Atif Aziz. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

namespace MoreLinq
{
    using System;
    using System.Collections.Generic;

    static partial class MoreEnumerable
    {
        /// <summary>
        /// Batches the source sequence into sized buckets.
        /// </summary>
        /// <typeparam name="TSource">Type of elements in <paramref name="source"/> sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="size">Size of buckets.</param>
        /// <returns>A sequence of equally sized buckets containing elements of the source collection.</returns>
        /// <remarks>
        /// This operator uses deferred execution and streams its results (buckets and bucket content).
        /// </remarks>

        public static IEnumerable<IEnumerable<TSource>> Batch<TSource>(this IEnumerable<TSource> source, int size) =>
            source.Batch(size, x => x);

        /// <summary>
        /// Batches the source sequence into sized buckets and applies a projection to each bucket.
        /// </summary>
        /// <typeparam name="TSource">Type of elements in <paramref name="source"/> sequence.</typeparam>
        /// <typeparam name="TResult">Type of result returned by <paramref name="resultSelector"/>.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="size">Size of buckets.</param>
        /// <param name="resultSelector">The projection to apply to each bucket.</param>
        /// <returns>A sequence of projections on equally sized buckets containing elements of the source collection.</returns>
        /// <remarks>
        /// This operator uses deferred execution and streams its results (buckets and bucket content).
        /// </remarks>

        public static IEnumerable<TResult> Batch<TSource, TResult>(this IEnumerable<TSource> source, int size,
            Func<IEnumerable<TSource>, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (size <= 0) throw new ArgumentOutOfRangeException(nameof(size));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return _(); IEnumerable<TResult> _()
            {
                var algo = new BatchAlgorithm<TSource, TResult>(size, resultSelector);

                foreach (var item in source)
                {
                    if (algo.OnItem(item, out var result))
                    {
                        yield return result;
                        algo.OnNextBucket();
                    }
                }

                if (algo.OnFinished(out var lastResult))
                    yield return lastResult;
            }
        }

        struct BatchAlgorithm<TSource, TResult>
        {
            TSource[] _bucket;
            int _count;
            readonly int _size;
            readonly Func<IEnumerable<TSource>, TResult> _resultSelector;

            public BatchAlgorithm(int size, Func<IEnumerable<TSource>, TResult> resultSelector)
            {
                _bucket = null;
                _count = 0;
                _size = size;
                _resultSelector = resultSelector;
            }

            public bool OnItem(TSource item, out TResult result)
            {
                if (_bucket == null)
                    _bucket = new TSource[_size];

                _bucket[_count++] = item;

                // The bucket is fully buffered before it's yielded
                if (_count != _size)
                {
                    result = default;
                    return false;
                }

                result = _resultSelector(_bucket);
                return true;
            }

            public void OnNextBucket()
            {
                _bucket = null;
                _count = 0;
            }

            public bool OnFinished(out TResult result)
            {
                // Return the last bucket with all remaining elements
                if (_bucket != null && _count > 0)
                {
                    Array.Resize(ref _bucket, _count);
                    result = _resultSelector(_bucket);
                    return true;
                }
                else
                {
                    result = default;
                    return false;
                }
            }
        }
    }
}

#if !NO_ASYNC_STREAMS

namespace MoreLinq
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    partial class MoreEnumerable
    {
        /// <summary>
        /// Batches the source sequence into sized buckets.
        /// </summary>
        /// <typeparam name="TSource">Type of elements in <paramref name="source"/> sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="size">Size of buckets.</param>
        /// <returns>A sequence of equally sized buckets containing elements of the source collection.</returns>
        /// <remarks>
        /// This operator uses deferred execution and streams its results (buckets and bucket content).
        /// </remarks>

        public static IAsyncEnumerable<IEnumerable<TSource>> Batch<TSource>(this IAsyncEnumerable<TSource> source, int size) =>
            source.Batch(size, x => x);

        /// <summary>
        /// Batches the source sequence into sized buckets and applies a projection to each bucket.
        /// </summary>
        /// <typeparam name="TSource">Type of elements in <paramref name="source"/> sequence.</typeparam>
        /// <typeparam name="TResult">Type of result returned by <paramref name="resultSelector"/>.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="size">Size of buckets.</param>
        /// <param name="resultSelector">The projection to apply to each bucket.</param>
        /// <returns>A sequence of projections on equally sized buckets containing elements of the source collection.</returns>
        /// <remarks>
        /// This operator uses deferred execution and streams its results (buckets and bucket content).
        /// </remarks>

        public static IAsyncEnumerable<TResult> Batch<TSource, TResult>(this IAsyncEnumerable<TSource> source, int size,
            Func<IEnumerable<TSource>, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (size <= 0) throw new ArgumentOutOfRangeException(nameof(size));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return _(); async IAsyncEnumerable<TResult> _()
            {
                var batcher = new BatchAlgorithm<TSource, TResult>(size, resultSelector);

                await foreach (var item in source.ConfigureAwait(false))
                {
                    if (batcher.OnItem(item, out var result))
                    {
                        yield return result;
                        batcher.OnNextBucket();
                    }
                }

                if (batcher.OnFinished(out var lastResult))
                    yield return lastResult;
            }
        }
    }
}

#endif
