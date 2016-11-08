#region License and Terms
// MoreLINQ - Extensions to LINQ to Objects
// Copyright (c) 2016 Atif Aziz. All rights reserved.
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
    using System.Linq;

    static partial class MoreEnumerable
    {
        /// <summary>
        /// Returns distinct elements from a sequence by using the default
        /// equality comparer to compare values. This is a streaming version
        /// of <see cref="Enumerable.Distinct{TSource}(IEnumerable{TSource})"/>.
        /// </summary>
        /// <typeparam name="TSource">Type of the elements in the source sequence.</typeparam>
        /// <param name="source">The sequence to remove duplicate elements from.</param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> that contains distinct elements
        /// from the source sequence.
        /// </returns>
        /// <summary>
        /// This method uses deferred execution semantics and streams its
        /// results.
        /// </summary>

        public static IEnumerable<TSource> Unique<TSource>(this IEnumerable<TSource> source)
        {
            return Unique(source, null);
        }

        /// <summary>
        /// Returns distinct elements from a sequence by using a specified
        /// <see cref="IEqualityComparer{T}"/> to compare values. This is a
        /// streaming version of <see cref="Enumerable.Distinct{TSource}(IEnumerable{TSource},IEqualityComparer{TSource})"/>
        /// </summary>
        /// <typeparam name="TSource">Type of the elements in the source sequence.</typeparam>
        /// <param name="source">The sequence to remove duplicate elements from.</param>
        /// <param name="comparer">An <see cref="IEqualityComparer{T}"/> to compare values.</param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> that contains distinct elements
        /// from the source sequence.
        /// </returns>
        /// <summary>
        /// This method uses deferred execution semantics and streams its
        /// results.
        /// </summary>
        public static IEnumerable<TSource> Unique<TSource>(this IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return UniqueImpl(source, comparer ?? EqualityComparer<TSource>.Default);
        }

        static IEnumerable<TSource> UniqueImpl<TSource>(IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
        {
            HashSet<TSource> hs = null;
            foreach (var item in source)
            {
                if ((hs ?? (hs = new HashSet<TSource>(comparer))).Add(item))
                    yield return item;
            }
        }
    }
}
