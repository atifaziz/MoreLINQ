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
    using System.Collections;
    using System.Collections.Generic;

    static partial class MoreEnumerable
    {
        #if !NO_VALUE_TUPLES

        /// <summary>
        /// Partitions a sequence in two where each part contains components
        /// of elements from the original sequence.
        /// </summary>
        /// <typeparam name="T">Type of elements in the source sequence.</typeparam>
        /// <typeparam name="T1">
        /// Type of first component of <typeparamref name="T"/>.</typeparam>
        /// <typeparam name="T2">
        /// Type of second component of <typeparamref name="T"/>.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="firstSelector">Function to determine the first component.</param>
        /// <param name="secondSelector">Function to determine the second component.</param>
        /// <returns>
        /// A tuple of two sequences where the first sequence contains the
        /// components returned by <paramref name="firstSelector"/> and the
        /// second sequence the components returned by
        /// <paramref name="secondSelector"/>.</returns>

        public static (IEnumerable<T1>, IEnumerable<T2>) Unzip<T, T1, T2>(this IEnumerable<T> source,
            Func<T, T1> firstSelector, Func<T, T2> secondSelector) =>
            source.Unzip(firstSelector, secondSelector, ValueTuple.Create);

        #endif

        /// <summary>
        /// Partitions a sequence in two where each part contains components
        /// of elements from the original sequence. An additional parameter
        /// specifies a function that projects are a result from the two parts.
        /// </summary>
        /// <typeparam name="T">Type of elements in the source sequence.</typeparam>
        /// <typeparam name="T1">
        /// Type of first component of <typeparamref name="T"/>.</typeparam>
        /// <typeparam name="T2">
        /// Type of second component of <typeparamref name="T"/>.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="firstSelector">Function to determine the first component.</param>
        /// <param name="secondSelector">Function to determine the second component.</param>
        /// <param name="resultSelector">Function to project the result given
        /// a sequence of the first components and a sequence of second
        /// components, respectively.</param>
        /// <returns>
        /// The result from <paramref name="resultSelector"/>.</returns>

        public static TResult Unzip<T, T1, T2, TResult>(this IEnumerable<T> source,
            Func<T, T1> firstSelector, Func<T, T2> secondSelector,
            Func<IEnumerable<T1>, IEnumerable<T2>, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (firstSelector == null) throw new ArgumentNullException(nameof(firstSelector));
            if (secondSelector == null) throw new ArgumentNullException(nameof(secondSelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            if (source is ICollection<T> collection)
            {
                T1[] array1 = null;
                T2[] array2 = null;

                return resultSelector(new LazyList<T1[], T1>(() => { _(); return array1; }),
                                      new LazyList<T2[], T2>(() => { _(); return array2; }));

                void _()
                {
                    if (source != null)
                    {
                        var i = 0;
                        var a = new T1[collection.Count];
                        var b = new T2[collection.Count];
                        foreach (var item in source)
                        {
                            a[i] = firstSelector(item);
                            b[i] = secondSelector(item);
                            i++;
                        }
                        array1 = a;
                        array2 = b;
                        source = null;
                    }
                }
            }
            else
            {
                List<T1> list1 = null;
                List<T2> list2 = null;

                return resultSelector(new LazyList<List<T1>, T1>(() => { _(); return list1; }),
                                      new LazyList<List<T2>, T2>(() => { _(); return list2; }));

                void _()
                {
                    if (source != null)
                    {
                        var a = new List<T1>();
                        var b = new List<T2>();
                        foreach (var item in source)
                        {
                            a.Add(firstSelector(item));
                            b.Add(secondSelector(item));
                        }
                        list1 = a;
                        list2 = b;
                        source = null;
                    }
                }
            }

        }

        sealed class LazyList<TList, TItem> : IList<TItem>
            where TList : class, IList<TItem>
        {
            Func<TList> _factory;
            TList _list;

            public LazyList(Func<TList> factory)
            {
                _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            }

            TList List
            {
                get // Not tread-safe!
                {
                    return _list ?? (_list = CreateList());
                    TList CreateList()
                    {
                        var list = (_factory ?? throw new InvalidOperationException())();
                        _factory = null;
                        return list;
                    }
                }
            }

            public int Count => List.Count;
            public TItem this[int index] { get => List[index]; set => throw UnsupportedError(); }

            public IEnumerator<TItem> GetEnumerator() => List.GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            public int IndexOf(TItem item) => List.IndexOf(item);
            public bool Contains(TItem item) => List.Contains(item);
            public void CopyTo(TItem[] array, int arrayIndex) => List.CopyTo(array, arrayIndex);

            // read-only ...

            public bool IsReadOnly => true;

            static Exception UnsupportedError() =>
                new NotSupportedException("Collection is read-only.");

            public void Add(TItem item)               => throw UnsupportedError();
            public void Clear()                       => throw UnsupportedError();
            public bool Remove(TItem item)            => throw UnsupportedError();
            public void Insert(int index, TItem item) => throw UnsupportedError();
            public void RemoveAt(int index)           => throw UnsupportedError();
        }
    }
}
