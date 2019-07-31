#region License and Terms
// MoreLINQ - Extensions to LINQ to Objects
// Copyright (c) 2012 Atif Aziz. All rights reserved.
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
        /// Returns a sequence resulting from applying a function to each
        /// element in the source sequence and its
        /// predecessor, with the exception of the first element which is
        /// only returned as the predecessor of the second element.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TResult">The type of the element of the returned sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="resultSelector">A transform function to apply to
        /// each pair of sequence.</param>
        /// <returns>
        /// Returns the resulting sequence.
        /// </returns>
        /// <remarks>
        /// This operator uses deferred execution and streams its results.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// var source = new[] { "a", "b", "c", "d" };
        /// var result = source.Pairwise((a, b) => a + b);
        /// ]]></code>
        /// The <c>result</c> variable, when iterated over, will yield
        /// "ab", "bc" and "cd", in turn.
        /// </example>

        public static IEnumerable<TResult> Pairwise<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TSource, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return _(); IEnumerable<TResult> _()
            {
                using var e = source.GetEnumerator();

                var algo = new PairwiseAlgorithm<TSource, TResult>(resultSelector);

                if (!e.MoveNext())
                    yield break;

                algo.Init(e.Current);
                while (e.MoveNext())
                {
                    yield return algo.OnItem(e.Current);
                    algo.Loop();
                }
            }
        }

        struct PairwiseAlgorithm<TSource, TResult>
        {
            TSource item;
            TSource previous;
            readonly Func<TSource, TSource, TResult> resultSelector;

            public PairwiseAlgorithm(Func<TSource, TSource, TResult> resultSelector)
            {
                item = default;
                previous = default;
                this.resultSelector = resultSelector;
            }

            public void Init(TSource previous) =>
                this.previous = previous;

            public TResult OnItem(TSource item)
            {
                this.item = item;
                return resultSelector(previous, item);
            }

            public void Loop()
            {
                previous = item;
                item = default;
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

    static partial class MoreEnumerable
    {
        /// <summary>
        /// Returns a sequence resulting from applying a function to each
        /// element in the source sequence and its
        /// predecessor, with the exception of the first element which is
        /// only returned as the predecessor of the second element.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TResult">The type of the element of the returned sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="resultSelector">A transform function to apply to
        /// each pair of sequence.</param>
        /// <returns>
        /// Returns the resulting sequence.
        /// </returns>
        /// <remarks>
        /// This operator uses deferred execution and streams its results.
        /// </remarks>

        public static IAsyncEnumerable<TResult> Pairwise<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, TSource, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return _(); async IAsyncEnumerable<TResult> _()
            {
                await using var e = source.ConfigureAwait(false).GetAsyncEnumerator();

                var algo = new PairwiseAlgorithm<TSource, TResult>();

                if (!await e.MoveNextAsync())
                    yield break;

                algo.Init(e.Current);
                while (await e.MoveNextAsync())
                {
                    yield return algo.OnItem(e.Current);
                    algo.Loop();
                }
            }
        }
    }
}

#endif
