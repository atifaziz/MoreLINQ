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
        #if !NO_VALUE_TUPLES
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="source"></param>
        /// <param name="firstSelector"></param>
        /// <param name="secondSelector"></param>
        /// <returns></returns>
        public static (IEnumerable<T1>, IEnumerable<T2>) Unzip<T, T1, T2>(this IEnumerable<T> source,
            Func<T, T1> firstSelector, Func<T, T2> secondSelector) =>
            source.Unzip(firstSelector, secondSelector, ValueTuple.Create);

        #endif

        /// <summary>
        /// Partitions a sequence into two where each part contains
        /// components of elements from the original sequence.
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
        /// <returns>The result of invoking <paramref name="resultSelector"/>.</returns>

        public static TResult Unzip<T, T1, T2, TResult>(this IEnumerable<T> source,
            Func<T, T1> firstSelector, Func<T, T2> secondSelector,
            Func<IEnumerable<T1>, IEnumerable<T2>, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (firstSelector == null) throw new ArgumentNullException(nameof(firstSelector));
            if (secondSelector == null) throw new ArgumentNullException(nameof(secondSelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            var list1 = new List<T1>();
            var list2 = new List<T2>();
            foreach (var item in source)
            {
                list1.Add(firstSelector(item));
                list2.Add(secondSelector(item));
            }
            return resultSelector(list1, list2);
        }
    }
}
