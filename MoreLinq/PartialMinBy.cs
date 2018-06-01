#region License and Terms
// MoreLINQ - Extensions to LINQ to Objects
// Copyright (c) 2018 Atif Aziz. All rights reserved.
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
        /// TODO
        /// </summary>

        public static IEnumerable<TSource> PartialMinBy<TSource, TKey>(this IEnumerable<TSource> source,
            Extremity extremity, int limit, Func<TSource, TKey> selector) =>
            PartialMinBy(source, extremity, limit, selector, null);

        /// <summary>
        /// TODO
        /// </summary>

        public static IEnumerable<TSource> PartialMinBy<TSource, TKey>(this IEnumerable<TSource> source,
            Extremity extremity, int limit,
            Func<TSource, TKey> selector, IComparer<TKey> comparer)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (extremity != Extremity.First && extremity != Extremity.Last)
                throw new ArgumentOutOfRangeException(nameof(extremity));
            if (limit < 0) throw new ArgumentOutOfRangeException(nameof(limit));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            comparer = comparer ?? Comparer<TKey>.Default;
            return limit == 0
                 ? Enumerable.Empty<TSource>()
                 : ExtremaBy(source, extremity, limit,
                             selector, (x, y) => -Math.Sign(comparer.Compare(x, y)));
        }
    }
}
