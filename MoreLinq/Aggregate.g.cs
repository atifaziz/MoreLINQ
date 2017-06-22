#region License and Terms
// MoreLINQ - Extensions to LINQ to Objects
// Copyright (c) 2017 Atif Aziz. All rights reserved.
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

#if !NO_VALUE_TUPLES

namespace MoreLinq
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    partial class MoreEnumerable
    {
        /// <summary>
        /// Applies 2 accumulator functions over a sequence.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TState1">The type of the first accumulator value.</typeparam>
        /// <typeparam name="TState2">The type of the second accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="source">The sequence to aggregate over.</param>
        /// <param name="seed1">The first initial accumulator value.</param>
        /// <param name="accumulator1">The first accumulator function to be invoked on each element.</param>
        /// <param name="seed2">The second initial accumulator value.</param>
        /// <param name="accumulator2">The second accumulator function to be invoked on each element.</param>
        /// <param name="resultSelector">
        /// A function to transform the final accumulator value into the result value.</param>
        /// <returns>The transformed final accumulator value.</returns>
        /// <remarks>This method uses immediate execution semantics.</remarks>

        public static TResult Aggregate<TSource, TState1, TState2, TResult>(
            this IEnumerable<TSource> source,
            TState1 seed1, Func<TState1, TSource, TState1> accumulator1,
            TState2 seed2, Func<TState2, TSource, TState2> accumulator2,
            Func<TState1, TState2, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (accumulator1 == null) throw new ArgumentNullException(nameof(accumulator1));
            if (accumulator2 == null) throw new ArgumentNullException(nameof(accumulator2));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                source.Aggregate(
                    (seed1, seed2),
                    (s, e) => (accumulator1(s.Item1, e),
                               accumulator2(s.Item2, e)),
                    s => resultSelector(s.Item1, s.Item2));
        }

        /// <summary>
        /// Applies 3 accumulator functions over a sequence.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TState1">The type of the first accumulator value.</typeparam>
        /// <typeparam name="TState2">The type of the second accumulator value.</typeparam>
        /// <typeparam name="TState3">The type of the third accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="source">The sequence to aggregate over.</param>
        /// <param name="seed1">The first initial accumulator value.</param>
        /// <param name="accumulator1">The first accumulator function to be invoked on each element.</param>
        /// <param name="seed2">The second initial accumulator value.</param>
        /// <param name="accumulator2">The second accumulator function to be invoked on each element.</param>
        /// <param name="seed3">The third initial accumulator value.</param>
        /// <param name="accumulator3">The third accumulator function to be invoked on each element.</param>
        /// <param name="resultSelector">
        /// A function to transform the final accumulator value into the result value.</param>
        /// <returns>The transformed final accumulator value.</returns>
        /// <remarks>This method uses immediate execution semantics.</remarks>

        public static TResult Aggregate<TSource, TState1, TState2, TState3, TResult>(
            this IEnumerable<TSource> source,
            TState1 seed1, Func<TState1, TSource, TState1> accumulator1,
            TState2 seed2, Func<TState2, TSource, TState2> accumulator2,
            TState3 seed3, Func<TState3, TSource, TState3> accumulator3,
            Func<TState1, TState2, TState3, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (accumulator1 == null) throw new ArgumentNullException(nameof(accumulator1));
            if (accumulator2 == null) throw new ArgumentNullException(nameof(accumulator2));
            if (accumulator3 == null) throw new ArgumentNullException(nameof(accumulator3));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                source.Aggregate(
                    (seed1, seed2, seed3),
                    (s, e) => (accumulator1(s.Item1, e),
                               accumulator2(s.Item2, e),
                               accumulator3(s.Item3, e)),
                    s => resultSelector(s.Item1, s.Item2, s.Item3));
        }

        /// <summary>
        /// Applies 4 accumulator functions over a sequence.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TState1">The type of the first accumulator value.</typeparam>
        /// <typeparam name="TState2">The type of the second accumulator value.</typeparam>
        /// <typeparam name="TState3">The type of the third accumulator value.</typeparam>
        /// <typeparam name="TState4">The type of the fourth accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="source">The sequence to aggregate over.</param>
        /// <param name="seed1">The first initial accumulator value.</param>
        /// <param name="accumulator1">The first accumulator function to be invoked on each element.</param>
        /// <param name="seed2">The second initial accumulator value.</param>
        /// <param name="accumulator2">The second accumulator function to be invoked on each element.</param>
        /// <param name="seed3">The third initial accumulator value.</param>
        /// <param name="accumulator3">The third accumulator function to be invoked on each element.</param>
        /// <param name="seed4">The fourth initial accumulator value.</param>
        /// <param name="accumulator4">The fourth accumulator function to be invoked on each element.</param>
        /// <param name="resultSelector">
        /// A function to transform the final accumulator value into the result value.</param>
        /// <returns>The transformed final accumulator value.</returns>
        /// <remarks>This method uses immediate execution semantics.</remarks>

        public static TResult Aggregate<TSource, TState1, TState2, TState3, TState4, TResult>(
            this IEnumerable<TSource> source,
            TState1 seed1, Func<TState1, TSource, TState1> accumulator1,
            TState2 seed2, Func<TState2, TSource, TState2> accumulator2,
            TState3 seed3, Func<TState3, TSource, TState3> accumulator3,
            TState4 seed4, Func<TState4, TSource, TState4> accumulator4,
            Func<TState1, TState2, TState3, TState4, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (accumulator1 == null) throw new ArgumentNullException(nameof(accumulator1));
            if (accumulator2 == null) throw new ArgumentNullException(nameof(accumulator2));
            if (accumulator3 == null) throw new ArgumentNullException(nameof(accumulator3));
            if (accumulator4 == null) throw new ArgumentNullException(nameof(accumulator4));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                source.Aggregate(
                    (seed1, seed2, seed3, seed4),
                    (s, e) => (accumulator1(s.Item1, e),
                               accumulator2(s.Item2, e),
                               accumulator3(s.Item3, e),
                               accumulator4(s.Item4, e)),
                    s => resultSelector(s.Item1, s.Item2, s.Item3, s.Item4));
        }

        /// <summary>
        /// Applies 5 accumulator functions over a sequence.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TState1">The type of the first accumulator value.</typeparam>
        /// <typeparam name="TState2">The type of the second accumulator value.</typeparam>
        /// <typeparam name="TState3">The type of the third accumulator value.</typeparam>
        /// <typeparam name="TState4">The type of the fourth accumulator value.</typeparam>
        /// <typeparam name="TState5">The type of the fifth accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="source">The sequence to aggregate over.</param>
        /// <param name="seed1">The first initial accumulator value.</param>
        /// <param name="accumulator1">The first accumulator function to be invoked on each element.</param>
        /// <param name="seed2">The second initial accumulator value.</param>
        /// <param name="accumulator2">The second accumulator function to be invoked on each element.</param>
        /// <param name="seed3">The third initial accumulator value.</param>
        /// <param name="accumulator3">The third accumulator function to be invoked on each element.</param>
        /// <param name="seed4">The fourth initial accumulator value.</param>
        /// <param name="accumulator4">The fourth accumulator function to be invoked on each element.</param>
        /// <param name="seed5">The fifth initial accumulator value.</param>
        /// <param name="accumulator5">The fifth accumulator function to be invoked on each element.</param>
        /// <param name="resultSelector">
        /// A function to transform the final accumulator value into the result value.</param>
        /// <returns>The transformed final accumulator value.</returns>
        /// <remarks>This method uses immediate execution semantics.</remarks>

        public static TResult Aggregate<TSource, TState1, TState2, TState3, TState4, TState5, TResult>(
            this IEnumerable<TSource> source,
            TState1 seed1, Func<TState1, TSource, TState1> accumulator1,
            TState2 seed2, Func<TState2, TSource, TState2> accumulator2,
            TState3 seed3, Func<TState3, TSource, TState3> accumulator3,
            TState4 seed4, Func<TState4, TSource, TState4> accumulator4,
            TState5 seed5, Func<TState5, TSource, TState5> accumulator5,
            Func<TState1, TState2, TState3, TState4, TState5, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (accumulator1 == null) throw new ArgumentNullException(nameof(accumulator1));
            if (accumulator2 == null) throw new ArgumentNullException(nameof(accumulator2));
            if (accumulator3 == null) throw new ArgumentNullException(nameof(accumulator3));
            if (accumulator4 == null) throw new ArgumentNullException(nameof(accumulator4));
            if (accumulator5 == null) throw new ArgumentNullException(nameof(accumulator5));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                source.Aggregate(
                    (seed1, seed2, seed3, seed4, seed5),
                    (s, e) => (accumulator1(s.Item1, e),
                               accumulator2(s.Item2, e),
                               accumulator3(s.Item3, e),
                               accumulator4(s.Item4, e),
                               accumulator5(s.Item5, e)),
                    s => resultSelector(s.Item1, s.Item2, s.Item3, s.Item4, s.Item5));
        }

        /// <summary>
        /// Applies 6 accumulator functions over a sequence.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TState1">The type of the first accumulator value.</typeparam>
        /// <typeparam name="TState2">The type of the second accumulator value.</typeparam>
        /// <typeparam name="TState3">The type of the third accumulator value.</typeparam>
        /// <typeparam name="TState4">The type of the fourth accumulator value.</typeparam>
        /// <typeparam name="TState5">The type of the fifth accumulator value.</typeparam>
        /// <typeparam name="TState6">The type of the sixth accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="source">The sequence to aggregate over.</param>
        /// <param name="seed1">The first initial accumulator value.</param>
        /// <param name="accumulator1">The first accumulator function to be invoked on each element.</param>
        /// <param name="seed2">The second initial accumulator value.</param>
        /// <param name="accumulator2">The second accumulator function to be invoked on each element.</param>
        /// <param name="seed3">The third initial accumulator value.</param>
        /// <param name="accumulator3">The third accumulator function to be invoked on each element.</param>
        /// <param name="seed4">The fourth initial accumulator value.</param>
        /// <param name="accumulator4">The fourth accumulator function to be invoked on each element.</param>
        /// <param name="seed5">The fifth initial accumulator value.</param>
        /// <param name="accumulator5">The fifth accumulator function to be invoked on each element.</param>
        /// <param name="seed6">The sixth initial accumulator value.</param>
        /// <param name="accumulator6">The sixth accumulator function to be invoked on each element.</param>
        /// <param name="resultSelector">
        /// A function to transform the final accumulator value into the result value.</param>
        /// <returns>The transformed final accumulator value.</returns>
        /// <remarks>This method uses immediate execution semantics.</remarks>

        public static TResult Aggregate<TSource, TState1, TState2, TState3, TState4, TState5, TState6, TResult>(
            this IEnumerable<TSource> source,
            TState1 seed1, Func<TState1, TSource, TState1> accumulator1,
            TState2 seed2, Func<TState2, TSource, TState2> accumulator2,
            TState3 seed3, Func<TState3, TSource, TState3> accumulator3,
            TState4 seed4, Func<TState4, TSource, TState4> accumulator4,
            TState5 seed5, Func<TState5, TSource, TState5> accumulator5,
            TState6 seed6, Func<TState6, TSource, TState6> accumulator6,
            Func<TState1, TState2, TState3, TState4, TState5, TState6, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (accumulator1 == null) throw new ArgumentNullException(nameof(accumulator1));
            if (accumulator2 == null) throw new ArgumentNullException(nameof(accumulator2));
            if (accumulator3 == null) throw new ArgumentNullException(nameof(accumulator3));
            if (accumulator4 == null) throw new ArgumentNullException(nameof(accumulator4));
            if (accumulator5 == null) throw new ArgumentNullException(nameof(accumulator5));
            if (accumulator6 == null) throw new ArgumentNullException(nameof(accumulator6));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                source.Aggregate(
                    (seed1, seed2, seed3, seed4, seed5, seed6),
                    (s, e) => (accumulator1(s.Item1, e),
                               accumulator2(s.Item2, e),
                               accumulator3(s.Item3, e),
                               accumulator4(s.Item4, e),
                               accumulator5(s.Item5, e),
                               accumulator6(s.Item6, e)),
                    s => resultSelector(s.Item1, s.Item2, s.Item3, s.Item4, s.Item5, s.Item6));
        }

        /// <summary>
        /// Applies 7 accumulator functions over a sequence.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TState1">The type of the first accumulator value.</typeparam>
        /// <typeparam name="TState2">The type of the second accumulator value.</typeparam>
        /// <typeparam name="TState3">The type of the third accumulator value.</typeparam>
        /// <typeparam name="TState4">The type of the fourth accumulator value.</typeparam>
        /// <typeparam name="TState5">The type of the fifth accumulator value.</typeparam>
        /// <typeparam name="TState6">The type of the sixth accumulator value.</typeparam>
        /// <typeparam name="TState7">The type of the seventh accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="source">The sequence to aggregate over.</param>
        /// <param name="seed1">The first initial accumulator value.</param>
        /// <param name="accumulator1">The first accumulator function to be invoked on each element.</param>
        /// <param name="seed2">The second initial accumulator value.</param>
        /// <param name="accumulator2">The second accumulator function to be invoked on each element.</param>
        /// <param name="seed3">The third initial accumulator value.</param>
        /// <param name="accumulator3">The third accumulator function to be invoked on each element.</param>
        /// <param name="seed4">The fourth initial accumulator value.</param>
        /// <param name="accumulator4">The fourth accumulator function to be invoked on each element.</param>
        /// <param name="seed5">The fifth initial accumulator value.</param>
        /// <param name="accumulator5">The fifth accumulator function to be invoked on each element.</param>
        /// <param name="seed6">The sixth initial accumulator value.</param>
        /// <param name="accumulator6">The sixth accumulator function to be invoked on each element.</param>
        /// <param name="seed7">The seventh initial accumulator value.</param>
        /// <param name="accumulator7">The seventh accumulator function to be invoked on each element.</param>
        /// <param name="resultSelector">
        /// A function to transform the final accumulator value into the result value.</param>
        /// <returns>The transformed final accumulator value.</returns>
        /// <remarks>This method uses immediate execution semantics.</remarks>

        public static TResult Aggregate<TSource, TState1, TState2, TState3, TState4, TState5, TState6, TState7, TResult>(
            this IEnumerable<TSource> source,
            TState1 seed1, Func<TState1, TSource, TState1> accumulator1,
            TState2 seed2, Func<TState2, TSource, TState2> accumulator2,
            TState3 seed3, Func<TState3, TSource, TState3> accumulator3,
            TState4 seed4, Func<TState4, TSource, TState4> accumulator4,
            TState5 seed5, Func<TState5, TSource, TState5> accumulator5,
            TState6 seed6, Func<TState6, TSource, TState6> accumulator6,
            TState7 seed7, Func<TState7, TSource, TState7> accumulator7,
            Func<TState1, TState2, TState3, TState4, TState5, TState6, TState7, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (accumulator1 == null) throw new ArgumentNullException(nameof(accumulator1));
            if (accumulator2 == null) throw new ArgumentNullException(nameof(accumulator2));
            if (accumulator3 == null) throw new ArgumentNullException(nameof(accumulator3));
            if (accumulator4 == null) throw new ArgumentNullException(nameof(accumulator4));
            if (accumulator5 == null) throw new ArgumentNullException(nameof(accumulator5));
            if (accumulator6 == null) throw new ArgumentNullException(nameof(accumulator6));
            if (accumulator7 == null) throw new ArgumentNullException(nameof(accumulator7));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                source.Aggregate(
                    (seed1, seed2, seed3, seed4, seed5, seed6, seed7),
                    (s, e) => (accumulator1(s.Item1, e),
                               accumulator2(s.Item2, e),
                               accumulator3(s.Item3, e),
                               accumulator4(s.Item4, e),
                               accumulator5(s.Item5, e),
                               accumulator6(s.Item6, e),
                               accumulator7(s.Item7, e)),
                    s => resultSelector(s.Item1, s.Item2, s.Item3, s.Item4, s.Item5, s.Item6, s.Item7));
        }

        /// <summary>
        /// Applies 8 accumulator functions over a sequence.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TState1">The type of the first accumulator value.</typeparam>
        /// <typeparam name="TState2">The type of the second accumulator value.</typeparam>
        /// <typeparam name="TState3">The type of the third accumulator value.</typeparam>
        /// <typeparam name="TState4">The type of the fourth accumulator value.</typeparam>
        /// <typeparam name="TState5">The type of the fifth accumulator value.</typeparam>
        /// <typeparam name="TState6">The type of the sixth accumulator value.</typeparam>
        /// <typeparam name="TState7">The type of the seventh accumulator value.</typeparam>
        /// <typeparam name="TState8">The type of the eighth accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="source">The sequence to aggregate over.</param>
        /// <param name="seed1">The first initial accumulator value.</param>
        /// <param name="accumulator1">The first accumulator function to be invoked on each element.</param>
        /// <param name="seed2">The second initial accumulator value.</param>
        /// <param name="accumulator2">The second accumulator function to be invoked on each element.</param>
        /// <param name="seed3">The third initial accumulator value.</param>
        /// <param name="accumulator3">The third accumulator function to be invoked on each element.</param>
        /// <param name="seed4">The fourth initial accumulator value.</param>
        /// <param name="accumulator4">The fourth accumulator function to be invoked on each element.</param>
        /// <param name="seed5">The fifth initial accumulator value.</param>
        /// <param name="accumulator5">The fifth accumulator function to be invoked on each element.</param>
        /// <param name="seed6">The sixth initial accumulator value.</param>
        /// <param name="accumulator6">The sixth accumulator function to be invoked on each element.</param>
        /// <param name="seed7">The seventh initial accumulator value.</param>
        /// <param name="accumulator7">The seventh accumulator function to be invoked on each element.</param>
        /// <param name="seed8">The eighth initial accumulator value.</param>
        /// <param name="accumulator8">The eighth accumulator function to be invoked on each element.</param>
        /// <param name="resultSelector">
        /// A function to transform the final accumulator value into the result value.</param>
        /// <returns>The transformed final accumulator value.</returns>
        /// <remarks>This method uses immediate execution semantics.</remarks>

        public static TResult Aggregate<TSource, TState1, TState2, TState3, TState4, TState5, TState6, TState7, TState8, TResult>(
            this IEnumerable<TSource> source,
            TState1 seed1, Func<TState1, TSource, TState1> accumulator1,
            TState2 seed2, Func<TState2, TSource, TState2> accumulator2,
            TState3 seed3, Func<TState3, TSource, TState3> accumulator3,
            TState4 seed4, Func<TState4, TSource, TState4> accumulator4,
            TState5 seed5, Func<TState5, TSource, TState5> accumulator5,
            TState6 seed6, Func<TState6, TSource, TState6> accumulator6,
            TState7 seed7, Func<TState7, TSource, TState7> accumulator7,
            TState8 seed8, Func<TState8, TSource, TState8> accumulator8,
            Func<TState1, TState2, TState3, TState4, TState5, TState6, TState7, TState8, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (accumulator1 == null) throw new ArgumentNullException(nameof(accumulator1));
            if (accumulator2 == null) throw new ArgumentNullException(nameof(accumulator2));
            if (accumulator3 == null) throw new ArgumentNullException(nameof(accumulator3));
            if (accumulator4 == null) throw new ArgumentNullException(nameof(accumulator4));
            if (accumulator5 == null) throw new ArgumentNullException(nameof(accumulator5));
            if (accumulator6 == null) throw new ArgumentNullException(nameof(accumulator6));
            if (accumulator7 == null) throw new ArgumentNullException(nameof(accumulator7));
            if (accumulator8 == null) throw new ArgumentNullException(nameof(accumulator8));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                source.Aggregate(
                    (seed1, seed2, seed3, seed4, seed5, seed6, seed7, seed8),
                    (s, e) => (accumulator1(s.Item1, e),
                               accumulator2(s.Item2, e),
                               accumulator3(s.Item3, e),
                               accumulator4(s.Item4, e),
                               accumulator5(s.Item5, e),
                               accumulator6(s.Item6, e),
                               accumulator7(s.Item7, e),
                               accumulator8(s.Item8, e)),
                    s => resultSelector(s.Item1, s.Item2, s.Item3, s.Item4, s.Item5, s.Item6, s.Item7, s.Item8));
        }

        /// <summary>
        /// Applies 9 accumulator functions over a sequence.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TState1">The type of the first accumulator value.</typeparam>
        /// <typeparam name="TState2">The type of the second accumulator value.</typeparam>
        /// <typeparam name="TState3">The type of the third accumulator value.</typeparam>
        /// <typeparam name="TState4">The type of the fourth accumulator value.</typeparam>
        /// <typeparam name="TState5">The type of the fifth accumulator value.</typeparam>
        /// <typeparam name="TState6">The type of the sixth accumulator value.</typeparam>
        /// <typeparam name="TState7">The type of the seventh accumulator value.</typeparam>
        /// <typeparam name="TState8">The type of the eighth accumulator value.</typeparam>
        /// <typeparam name="TState9">The type of the ninth accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="source">The sequence to aggregate over.</param>
        /// <param name="seed1">The first initial accumulator value.</param>
        /// <param name="accumulator1">The first accumulator function to be invoked on each element.</param>
        /// <param name="seed2">The second initial accumulator value.</param>
        /// <param name="accumulator2">The second accumulator function to be invoked on each element.</param>
        /// <param name="seed3">The third initial accumulator value.</param>
        /// <param name="accumulator3">The third accumulator function to be invoked on each element.</param>
        /// <param name="seed4">The fourth initial accumulator value.</param>
        /// <param name="accumulator4">The fourth accumulator function to be invoked on each element.</param>
        /// <param name="seed5">The fifth initial accumulator value.</param>
        /// <param name="accumulator5">The fifth accumulator function to be invoked on each element.</param>
        /// <param name="seed6">The sixth initial accumulator value.</param>
        /// <param name="accumulator6">The sixth accumulator function to be invoked on each element.</param>
        /// <param name="seed7">The seventh initial accumulator value.</param>
        /// <param name="accumulator7">The seventh accumulator function to be invoked on each element.</param>
        /// <param name="seed8">The eighth initial accumulator value.</param>
        /// <param name="accumulator8">The eighth accumulator function to be invoked on each element.</param>
        /// <param name="seed9">The ninth initial accumulator value.</param>
        /// <param name="accumulator9">The ninth accumulator function to be invoked on each element.</param>
        /// <param name="resultSelector">
        /// A function to transform the final accumulator value into the result value.</param>
        /// <returns>The transformed final accumulator value.</returns>
        /// <remarks>This method uses immediate execution semantics.</remarks>

        public static TResult Aggregate<TSource, TState1, TState2, TState3, TState4, TState5, TState6, TState7, TState8, TState9, TResult>(
            this IEnumerable<TSource> source,
            TState1 seed1, Func<TState1, TSource, TState1> accumulator1,
            TState2 seed2, Func<TState2, TSource, TState2> accumulator2,
            TState3 seed3, Func<TState3, TSource, TState3> accumulator3,
            TState4 seed4, Func<TState4, TSource, TState4> accumulator4,
            TState5 seed5, Func<TState5, TSource, TState5> accumulator5,
            TState6 seed6, Func<TState6, TSource, TState6> accumulator6,
            TState7 seed7, Func<TState7, TSource, TState7> accumulator7,
            TState8 seed8, Func<TState8, TSource, TState8> accumulator8,
            TState9 seed9, Func<TState9, TSource, TState9> accumulator9,
            Func<TState1, TState2, TState3, TState4, TState5, TState6, TState7, TState8, TState9, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (accumulator1 == null) throw new ArgumentNullException(nameof(accumulator1));
            if (accumulator2 == null) throw new ArgumentNullException(nameof(accumulator2));
            if (accumulator3 == null) throw new ArgumentNullException(nameof(accumulator3));
            if (accumulator4 == null) throw new ArgumentNullException(nameof(accumulator4));
            if (accumulator5 == null) throw new ArgumentNullException(nameof(accumulator5));
            if (accumulator6 == null) throw new ArgumentNullException(nameof(accumulator6));
            if (accumulator7 == null) throw new ArgumentNullException(nameof(accumulator7));
            if (accumulator8 == null) throw new ArgumentNullException(nameof(accumulator8));
            if (accumulator9 == null) throw new ArgumentNullException(nameof(accumulator9));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                source.Aggregate(
                    (seed1, seed2, seed3, seed4, seed5, seed6, seed7, seed8, seed9),
                    (s, e) => (accumulator1(s.Item1, e),
                               accumulator2(s.Item2, e),
                               accumulator3(s.Item3, e),
                               accumulator4(s.Item4, e),
                               accumulator5(s.Item5, e),
                               accumulator6(s.Item6, e),
                               accumulator7(s.Item7, e),
                               accumulator8(s.Item8, e),
                               accumulator9(s.Item9, e)),
                    s => resultSelector(s.Item1, s.Item2, s.Item3, s.Item4, s.Item5, s.Item6, s.Item7, s.Item8, s.Item9));
        }

        /// <summary>
        /// Applies 10 accumulator functions over a sequence.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TState1">The type of the first accumulator value.</typeparam>
        /// <typeparam name="TState2">The type of the second accumulator value.</typeparam>
        /// <typeparam name="TState3">The type of the third accumulator value.</typeparam>
        /// <typeparam name="TState4">The type of the fourth accumulator value.</typeparam>
        /// <typeparam name="TState5">The type of the fifth accumulator value.</typeparam>
        /// <typeparam name="TState6">The type of the sixth accumulator value.</typeparam>
        /// <typeparam name="TState7">The type of the seventh accumulator value.</typeparam>
        /// <typeparam name="TState8">The type of the eighth accumulator value.</typeparam>
        /// <typeparam name="TState9">The type of the ninth accumulator value.</typeparam>
        /// <typeparam name="TState10">The type of the tenth accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="source">The sequence to aggregate over.</param>
        /// <param name="seed1">The first initial accumulator value.</param>
        /// <param name="accumulator1">The first accumulator function to be invoked on each element.</param>
        /// <param name="seed2">The second initial accumulator value.</param>
        /// <param name="accumulator2">The second accumulator function to be invoked on each element.</param>
        /// <param name="seed3">The third initial accumulator value.</param>
        /// <param name="accumulator3">The third accumulator function to be invoked on each element.</param>
        /// <param name="seed4">The fourth initial accumulator value.</param>
        /// <param name="accumulator4">The fourth accumulator function to be invoked on each element.</param>
        /// <param name="seed5">The fifth initial accumulator value.</param>
        /// <param name="accumulator5">The fifth accumulator function to be invoked on each element.</param>
        /// <param name="seed6">The sixth initial accumulator value.</param>
        /// <param name="accumulator6">The sixth accumulator function to be invoked on each element.</param>
        /// <param name="seed7">The seventh initial accumulator value.</param>
        /// <param name="accumulator7">The seventh accumulator function to be invoked on each element.</param>
        /// <param name="seed8">The eighth initial accumulator value.</param>
        /// <param name="accumulator8">The eighth accumulator function to be invoked on each element.</param>
        /// <param name="seed9">The ninth initial accumulator value.</param>
        /// <param name="accumulator9">The ninth accumulator function to be invoked on each element.</param>
        /// <param name="seed10">The tenth initial accumulator value.</param>
        /// <param name="accumulator10">The tenth accumulator function to be invoked on each element.</param>
        /// <param name="resultSelector">
        /// A function to transform the final accumulator value into the result value.</param>
        /// <returns>The transformed final accumulator value.</returns>
        /// <remarks>This method uses immediate execution semantics.</remarks>

        public static TResult Aggregate<TSource, TState1, TState2, TState3, TState4, TState5, TState6, TState7, TState8, TState9, TState10, TResult>(
            this IEnumerable<TSource> source,
            TState1 seed1, Func<TState1, TSource, TState1> accumulator1,
            TState2 seed2, Func<TState2, TSource, TState2> accumulator2,
            TState3 seed3, Func<TState3, TSource, TState3> accumulator3,
            TState4 seed4, Func<TState4, TSource, TState4> accumulator4,
            TState5 seed5, Func<TState5, TSource, TState5> accumulator5,
            TState6 seed6, Func<TState6, TSource, TState6> accumulator6,
            TState7 seed7, Func<TState7, TSource, TState7> accumulator7,
            TState8 seed8, Func<TState8, TSource, TState8> accumulator8,
            TState9 seed9, Func<TState9, TSource, TState9> accumulator9,
            TState10 seed10, Func<TState10, TSource, TState10> accumulator10,
            Func<TState1, TState2, TState3, TState4, TState5, TState6, TState7, TState8, TState9, TState10, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (accumulator1 == null) throw new ArgumentNullException(nameof(accumulator1));
            if (accumulator2 == null) throw new ArgumentNullException(nameof(accumulator2));
            if (accumulator3 == null) throw new ArgumentNullException(nameof(accumulator3));
            if (accumulator4 == null) throw new ArgumentNullException(nameof(accumulator4));
            if (accumulator5 == null) throw new ArgumentNullException(nameof(accumulator5));
            if (accumulator6 == null) throw new ArgumentNullException(nameof(accumulator6));
            if (accumulator7 == null) throw new ArgumentNullException(nameof(accumulator7));
            if (accumulator8 == null) throw new ArgumentNullException(nameof(accumulator8));
            if (accumulator9 == null) throw new ArgumentNullException(nameof(accumulator9));
            if (accumulator10 == null) throw new ArgumentNullException(nameof(accumulator10));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                source.Aggregate(
                    (seed1, seed2, seed3, seed4, seed5, seed6, seed7, seed8, seed9, seed10),
                    (s, e) => (accumulator1(s.Item1, e),
                               accumulator2(s.Item2, e),
                               accumulator3(s.Item3, e),
                               accumulator4(s.Item4, e),
                               accumulator5(s.Item5, e),
                               accumulator6(s.Item6, e),
                               accumulator7(s.Item7, e),
                               accumulator8(s.Item8, e),
                               accumulator9(s.Item9, e),
                               accumulator10(s.Item10, e)),
                    s => resultSelector(s.Item1, s.Item2, s.Item3, s.Item4, s.Item5, s.Item6, s.Item7, s.Item8, s.Item9, s.Item10));
        }

        /// <summary>
        /// Applies 11 accumulator functions over a sequence.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TState1">The type of the first accumulator value.</typeparam>
        /// <typeparam name="TState2">The type of the second accumulator value.</typeparam>
        /// <typeparam name="TState3">The type of the third accumulator value.</typeparam>
        /// <typeparam name="TState4">The type of the fourth accumulator value.</typeparam>
        /// <typeparam name="TState5">The type of the fifth accumulator value.</typeparam>
        /// <typeparam name="TState6">The type of the sixth accumulator value.</typeparam>
        /// <typeparam name="TState7">The type of the seventh accumulator value.</typeparam>
        /// <typeparam name="TState8">The type of the eighth accumulator value.</typeparam>
        /// <typeparam name="TState9">The type of the ninth accumulator value.</typeparam>
        /// <typeparam name="TState10">The type of the tenth accumulator value.</typeparam>
        /// <typeparam name="TState11">The type of the eleventh accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="source">The sequence to aggregate over.</param>
        /// <param name="seed1">The first initial accumulator value.</param>
        /// <param name="accumulator1">The first accumulator function to be invoked on each element.</param>
        /// <param name="seed2">The second initial accumulator value.</param>
        /// <param name="accumulator2">The second accumulator function to be invoked on each element.</param>
        /// <param name="seed3">The third initial accumulator value.</param>
        /// <param name="accumulator3">The third accumulator function to be invoked on each element.</param>
        /// <param name="seed4">The fourth initial accumulator value.</param>
        /// <param name="accumulator4">The fourth accumulator function to be invoked on each element.</param>
        /// <param name="seed5">The fifth initial accumulator value.</param>
        /// <param name="accumulator5">The fifth accumulator function to be invoked on each element.</param>
        /// <param name="seed6">The sixth initial accumulator value.</param>
        /// <param name="accumulator6">The sixth accumulator function to be invoked on each element.</param>
        /// <param name="seed7">The seventh initial accumulator value.</param>
        /// <param name="accumulator7">The seventh accumulator function to be invoked on each element.</param>
        /// <param name="seed8">The eighth initial accumulator value.</param>
        /// <param name="accumulator8">The eighth accumulator function to be invoked on each element.</param>
        /// <param name="seed9">The ninth initial accumulator value.</param>
        /// <param name="accumulator9">The ninth accumulator function to be invoked on each element.</param>
        /// <param name="seed10">The tenth initial accumulator value.</param>
        /// <param name="accumulator10">The tenth accumulator function to be invoked on each element.</param>
        /// <param name="seed11">The eleventh initial accumulator value.</param>
        /// <param name="accumulator11">The eleventh accumulator function to be invoked on each element.</param>
        /// <param name="resultSelector">
        /// A function to transform the final accumulator value into the result value.</param>
        /// <returns>The transformed final accumulator value.</returns>
        /// <remarks>This method uses immediate execution semantics.</remarks>

        public static TResult Aggregate<TSource, TState1, TState2, TState3, TState4, TState5, TState6, TState7, TState8, TState9, TState10, TState11, TResult>(
            this IEnumerable<TSource> source,
            TState1 seed1, Func<TState1, TSource, TState1> accumulator1,
            TState2 seed2, Func<TState2, TSource, TState2> accumulator2,
            TState3 seed3, Func<TState3, TSource, TState3> accumulator3,
            TState4 seed4, Func<TState4, TSource, TState4> accumulator4,
            TState5 seed5, Func<TState5, TSource, TState5> accumulator5,
            TState6 seed6, Func<TState6, TSource, TState6> accumulator6,
            TState7 seed7, Func<TState7, TSource, TState7> accumulator7,
            TState8 seed8, Func<TState8, TSource, TState8> accumulator8,
            TState9 seed9, Func<TState9, TSource, TState9> accumulator9,
            TState10 seed10, Func<TState10, TSource, TState10> accumulator10,
            TState11 seed11, Func<TState11, TSource, TState11> accumulator11,
            Func<TState1, TState2, TState3, TState4, TState5, TState6, TState7, TState8, TState9, TState10, TState11, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (accumulator1 == null) throw new ArgumentNullException(nameof(accumulator1));
            if (accumulator2 == null) throw new ArgumentNullException(nameof(accumulator2));
            if (accumulator3 == null) throw new ArgumentNullException(nameof(accumulator3));
            if (accumulator4 == null) throw new ArgumentNullException(nameof(accumulator4));
            if (accumulator5 == null) throw new ArgumentNullException(nameof(accumulator5));
            if (accumulator6 == null) throw new ArgumentNullException(nameof(accumulator6));
            if (accumulator7 == null) throw new ArgumentNullException(nameof(accumulator7));
            if (accumulator8 == null) throw new ArgumentNullException(nameof(accumulator8));
            if (accumulator9 == null) throw new ArgumentNullException(nameof(accumulator9));
            if (accumulator10 == null) throw new ArgumentNullException(nameof(accumulator10));
            if (accumulator11 == null) throw new ArgumentNullException(nameof(accumulator11));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                source.Aggregate(
                    (seed1, seed2, seed3, seed4, seed5, seed6, seed7, seed8, seed9, seed10, seed11),
                    (s, e) => (accumulator1(s.Item1, e),
                               accumulator2(s.Item2, e),
                               accumulator3(s.Item3, e),
                               accumulator4(s.Item4, e),
                               accumulator5(s.Item5, e),
                               accumulator6(s.Item6, e),
                               accumulator7(s.Item7, e),
                               accumulator8(s.Item8, e),
                               accumulator9(s.Item9, e),
                               accumulator10(s.Item10, e),
                               accumulator11(s.Item11, e)),
                    s => resultSelector(s.Item1, s.Item2, s.Item3, s.Item4, s.Item5, s.Item6, s.Item7, s.Item8, s.Item9, s.Item10, s.Item11));
        }

        /// <summary>
        /// Applies 12 accumulator functions over a sequence.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TState1">The type of the first accumulator value.</typeparam>
        /// <typeparam name="TState2">The type of the second accumulator value.</typeparam>
        /// <typeparam name="TState3">The type of the third accumulator value.</typeparam>
        /// <typeparam name="TState4">The type of the fourth accumulator value.</typeparam>
        /// <typeparam name="TState5">The type of the fifth accumulator value.</typeparam>
        /// <typeparam name="TState6">The type of the sixth accumulator value.</typeparam>
        /// <typeparam name="TState7">The type of the seventh accumulator value.</typeparam>
        /// <typeparam name="TState8">The type of the eighth accumulator value.</typeparam>
        /// <typeparam name="TState9">The type of the ninth accumulator value.</typeparam>
        /// <typeparam name="TState10">The type of the tenth accumulator value.</typeparam>
        /// <typeparam name="TState11">The type of the eleventh accumulator value.</typeparam>
        /// <typeparam name="TState12">The type of the twelfth accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="source">The sequence to aggregate over.</param>
        /// <param name="seed1">The first initial accumulator value.</param>
        /// <param name="accumulator1">The first accumulator function to be invoked on each element.</param>
        /// <param name="seed2">The second initial accumulator value.</param>
        /// <param name="accumulator2">The second accumulator function to be invoked on each element.</param>
        /// <param name="seed3">The third initial accumulator value.</param>
        /// <param name="accumulator3">The third accumulator function to be invoked on each element.</param>
        /// <param name="seed4">The fourth initial accumulator value.</param>
        /// <param name="accumulator4">The fourth accumulator function to be invoked on each element.</param>
        /// <param name="seed5">The fifth initial accumulator value.</param>
        /// <param name="accumulator5">The fifth accumulator function to be invoked on each element.</param>
        /// <param name="seed6">The sixth initial accumulator value.</param>
        /// <param name="accumulator6">The sixth accumulator function to be invoked on each element.</param>
        /// <param name="seed7">The seventh initial accumulator value.</param>
        /// <param name="accumulator7">The seventh accumulator function to be invoked on each element.</param>
        /// <param name="seed8">The eighth initial accumulator value.</param>
        /// <param name="accumulator8">The eighth accumulator function to be invoked on each element.</param>
        /// <param name="seed9">The ninth initial accumulator value.</param>
        /// <param name="accumulator9">The ninth accumulator function to be invoked on each element.</param>
        /// <param name="seed10">The tenth initial accumulator value.</param>
        /// <param name="accumulator10">The tenth accumulator function to be invoked on each element.</param>
        /// <param name="seed11">The eleventh initial accumulator value.</param>
        /// <param name="accumulator11">The eleventh accumulator function to be invoked on each element.</param>
        /// <param name="seed12">The twelfth initial accumulator value.</param>
        /// <param name="accumulator12">The twelfth accumulator function to be invoked on each element.</param>
        /// <param name="resultSelector">
        /// A function to transform the final accumulator value into the result value.</param>
        /// <returns>The transformed final accumulator value.</returns>
        /// <remarks>This method uses immediate execution semantics.</remarks>

        public static TResult Aggregate<TSource, TState1, TState2, TState3, TState4, TState5, TState6, TState7, TState8, TState9, TState10, TState11, TState12, TResult>(
            this IEnumerable<TSource> source,
            TState1 seed1, Func<TState1, TSource, TState1> accumulator1,
            TState2 seed2, Func<TState2, TSource, TState2> accumulator2,
            TState3 seed3, Func<TState3, TSource, TState3> accumulator3,
            TState4 seed4, Func<TState4, TSource, TState4> accumulator4,
            TState5 seed5, Func<TState5, TSource, TState5> accumulator5,
            TState6 seed6, Func<TState6, TSource, TState6> accumulator6,
            TState7 seed7, Func<TState7, TSource, TState7> accumulator7,
            TState8 seed8, Func<TState8, TSource, TState8> accumulator8,
            TState9 seed9, Func<TState9, TSource, TState9> accumulator9,
            TState10 seed10, Func<TState10, TSource, TState10> accumulator10,
            TState11 seed11, Func<TState11, TSource, TState11> accumulator11,
            TState12 seed12, Func<TState12, TSource, TState12> accumulator12,
            Func<TState1, TState2, TState3, TState4, TState5, TState6, TState7, TState8, TState9, TState10, TState11, TState12, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (accumulator1 == null) throw new ArgumentNullException(nameof(accumulator1));
            if (accumulator2 == null) throw new ArgumentNullException(nameof(accumulator2));
            if (accumulator3 == null) throw new ArgumentNullException(nameof(accumulator3));
            if (accumulator4 == null) throw new ArgumentNullException(nameof(accumulator4));
            if (accumulator5 == null) throw new ArgumentNullException(nameof(accumulator5));
            if (accumulator6 == null) throw new ArgumentNullException(nameof(accumulator6));
            if (accumulator7 == null) throw new ArgumentNullException(nameof(accumulator7));
            if (accumulator8 == null) throw new ArgumentNullException(nameof(accumulator8));
            if (accumulator9 == null) throw new ArgumentNullException(nameof(accumulator9));
            if (accumulator10 == null) throw new ArgumentNullException(nameof(accumulator10));
            if (accumulator11 == null) throw new ArgumentNullException(nameof(accumulator11));
            if (accumulator12 == null) throw new ArgumentNullException(nameof(accumulator12));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                source.Aggregate(
                    (seed1, seed2, seed3, seed4, seed5, seed6, seed7, seed8, seed9, seed10, seed11, seed12),
                    (s, e) => (accumulator1(s.Item1, e),
                               accumulator2(s.Item2, e),
                               accumulator3(s.Item3, e),
                               accumulator4(s.Item4, e),
                               accumulator5(s.Item5, e),
                               accumulator6(s.Item6, e),
                               accumulator7(s.Item7, e),
                               accumulator8(s.Item8, e),
                               accumulator9(s.Item9, e),
                               accumulator10(s.Item10, e),
                               accumulator11(s.Item11, e),
                               accumulator12(s.Item12, e)),
                    s => resultSelector(s.Item1, s.Item2, s.Item3, s.Item4, s.Item5, s.Item6, s.Item7, s.Item8, s.Item9, s.Item10, s.Item11, s.Item12));
        }

        /// <summary>
        /// Applies 13 accumulator functions over a sequence.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TState1">The type of the first accumulator value.</typeparam>
        /// <typeparam name="TState2">The type of the second accumulator value.</typeparam>
        /// <typeparam name="TState3">The type of the third accumulator value.</typeparam>
        /// <typeparam name="TState4">The type of the fourth accumulator value.</typeparam>
        /// <typeparam name="TState5">The type of the fifth accumulator value.</typeparam>
        /// <typeparam name="TState6">The type of the sixth accumulator value.</typeparam>
        /// <typeparam name="TState7">The type of the seventh accumulator value.</typeparam>
        /// <typeparam name="TState8">The type of the eighth accumulator value.</typeparam>
        /// <typeparam name="TState9">The type of the ninth accumulator value.</typeparam>
        /// <typeparam name="TState10">The type of the tenth accumulator value.</typeparam>
        /// <typeparam name="TState11">The type of the eleventh accumulator value.</typeparam>
        /// <typeparam name="TState12">The type of the twelfth accumulator value.</typeparam>
        /// <typeparam name="TState13">The type of the thirteenth accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="source">The sequence to aggregate over.</param>
        /// <param name="seed1">The first initial accumulator value.</param>
        /// <param name="accumulator1">The first accumulator function to be invoked on each element.</param>
        /// <param name="seed2">The second initial accumulator value.</param>
        /// <param name="accumulator2">The second accumulator function to be invoked on each element.</param>
        /// <param name="seed3">The third initial accumulator value.</param>
        /// <param name="accumulator3">The third accumulator function to be invoked on each element.</param>
        /// <param name="seed4">The fourth initial accumulator value.</param>
        /// <param name="accumulator4">The fourth accumulator function to be invoked on each element.</param>
        /// <param name="seed5">The fifth initial accumulator value.</param>
        /// <param name="accumulator5">The fifth accumulator function to be invoked on each element.</param>
        /// <param name="seed6">The sixth initial accumulator value.</param>
        /// <param name="accumulator6">The sixth accumulator function to be invoked on each element.</param>
        /// <param name="seed7">The seventh initial accumulator value.</param>
        /// <param name="accumulator7">The seventh accumulator function to be invoked on each element.</param>
        /// <param name="seed8">The eighth initial accumulator value.</param>
        /// <param name="accumulator8">The eighth accumulator function to be invoked on each element.</param>
        /// <param name="seed9">The ninth initial accumulator value.</param>
        /// <param name="accumulator9">The ninth accumulator function to be invoked on each element.</param>
        /// <param name="seed10">The tenth initial accumulator value.</param>
        /// <param name="accumulator10">The tenth accumulator function to be invoked on each element.</param>
        /// <param name="seed11">The eleventh initial accumulator value.</param>
        /// <param name="accumulator11">The eleventh accumulator function to be invoked on each element.</param>
        /// <param name="seed12">The twelfth initial accumulator value.</param>
        /// <param name="accumulator12">The twelfth accumulator function to be invoked on each element.</param>
        /// <param name="seed13">The thirteenth initial accumulator value.</param>
        /// <param name="accumulator13">The thirteenth accumulator function to be invoked on each element.</param>
        /// <param name="resultSelector">
        /// A function to transform the final accumulator value into the result value.</param>
        /// <returns>The transformed final accumulator value.</returns>
        /// <remarks>This method uses immediate execution semantics.</remarks>

        public static TResult Aggregate<TSource, TState1, TState2, TState3, TState4, TState5, TState6, TState7, TState8, TState9, TState10, TState11, TState12, TState13, TResult>(
            this IEnumerable<TSource> source,
            TState1 seed1, Func<TState1, TSource, TState1> accumulator1,
            TState2 seed2, Func<TState2, TSource, TState2> accumulator2,
            TState3 seed3, Func<TState3, TSource, TState3> accumulator3,
            TState4 seed4, Func<TState4, TSource, TState4> accumulator4,
            TState5 seed5, Func<TState5, TSource, TState5> accumulator5,
            TState6 seed6, Func<TState6, TSource, TState6> accumulator6,
            TState7 seed7, Func<TState7, TSource, TState7> accumulator7,
            TState8 seed8, Func<TState8, TSource, TState8> accumulator8,
            TState9 seed9, Func<TState9, TSource, TState9> accumulator9,
            TState10 seed10, Func<TState10, TSource, TState10> accumulator10,
            TState11 seed11, Func<TState11, TSource, TState11> accumulator11,
            TState12 seed12, Func<TState12, TSource, TState12> accumulator12,
            TState13 seed13, Func<TState13, TSource, TState13> accumulator13,
            Func<TState1, TState2, TState3, TState4, TState5, TState6, TState7, TState8, TState9, TState10, TState11, TState12, TState13, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (accumulator1 == null) throw new ArgumentNullException(nameof(accumulator1));
            if (accumulator2 == null) throw new ArgumentNullException(nameof(accumulator2));
            if (accumulator3 == null) throw new ArgumentNullException(nameof(accumulator3));
            if (accumulator4 == null) throw new ArgumentNullException(nameof(accumulator4));
            if (accumulator5 == null) throw new ArgumentNullException(nameof(accumulator5));
            if (accumulator6 == null) throw new ArgumentNullException(nameof(accumulator6));
            if (accumulator7 == null) throw new ArgumentNullException(nameof(accumulator7));
            if (accumulator8 == null) throw new ArgumentNullException(nameof(accumulator8));
            if (accumulator9 == null) throw new ArgumentNullException(nameof(accumulator9));
            if (accumulator10 == null) throw new ArgumentNullException(nameof(accumulator10));
            if (accumulator11 == null) throw new ArgumentNullException(nameof(accumulator11));
            if (accumulator12 == null) throw new ArgumentNullException(nameof(accumulator12));
            if (accumulator13 == null) throw new ArgumentNullException(nameof(accumulator13));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                source.Aggregate(
                    (seed1, seed2, seed3, seed4, seed5, seed6, seed7, seed8, seed9, seed10, seed11, seed12, seed13),
                    (s, e) => (accumulator1(s.Item1, e),
                               accumulator2(s.Item2, e),
                               accumulator3(s.Item3, e),
                               accumulator4(s.Item4, e),
                               accumulator5(s.Item5, e),
                               accumulator6(s.Item6, e),
                               accumulator7(s.Item7, e),
                               accumulator8(s.Item8, e),
                               accumulator9(s.Item9, e),
                               accumulator10(s.Item10, e),
                               accumulator11(s.Item11, e),
                               accumulator12(s.Item12, e),
                               accumulator13(s.Item13, e)),
                    s => resultSelector(s.Item1, s.Item2, s.Item3, s.Item4, s.Item5, s.Item6, s.Item7, s.Item8, s.Item9, s.Item10, s.Item11, s.Item12, s.Item13));
        }

        /// <summary>
        /// Applies 14 accumulator functions over a sequence.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TState1">The type of the first accumulator value.</typeparam>
        /// <typeparam name="TState2">The type of the second accumulator value.</typeparam>
        /// <typeparam name="TState3">The type of the third accumulator value.</typeparam>
        /// <typeparam name="TState4">The type of the fourth accumulator value.</typeparam>
        /// <typeparam name="TState5">The type of the fifth accumulator value.</typeparam>
        /// <typeparam name="TState6">The type of the sixth accumulator value.</typeparam>
        /// <typeparam name="TState7">The type of the seventh accumulator value.</typeparam>
        /// <typeparam name="TState8">The type of the eighth accumulator value.</typeparam>
        /// <typeparam name="TState9">The type of the ninth accumulator value.</typeparam>
        /// <typeparam name="TState10">The type of the tenth accumulator value.</typeparam>
        /// <typeparam name="TState11">The type of the eleventh accumulator value.</typeparam>
        /// <typeparam name="TState12">The type of the twelfth accumulator value.</typeparam>
        /// <typeparam name="TState13">The type of the thirteenth accumulator value.</typeparam>
        /// <typeparam name="TState14">The type of the fourteenth accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="source">The sequence to aggregate over.</param>
        /// <param name="seed1">The first initial accumulator value.</param>
        /// <param name="accumulator1">The first accumulator function to be invoked on each element.</param>
        /// <param name="seed2">The second initial accumulator value.</param>
        /// <param name="accumulator2">The second accumulator function to be invoked on each element.</param>
        /// <param name="seed3">The third initial accumulator value.</param>
        /// <param name="accumulator3">The third accumulator function to be invoked on each element.</param>
        /// <param name="seed4">The fourth initial accumulator value.</param>
        /// <param name="accumulator4">The fourth accumulator function to be invoked on each element.</param>
        /// <param name="seed5">The fifth initial accumulator value.</param>
        /// <param name="accumulator5">The fifth accumulator function to be invoked on each element.</param>
        /// <param name="seed6">The sixth initial accumulator value.</param>
        /// <param name="accumulator6">The sixth accumulator function to be invoked on each element.</param>
        /// <param name="seed7">The seventh initial accumulator value.</param>
        /// <param name="accumulator7">The seventh accumulator function to be invoked on each element.</param>
        /// <param name="seed8">The eighth initial accumulator value.</param>
        /// <param name="accumulator8">The eighth accumulator function to be invoked on each element.</param>
        /// <param name="seed9">The ninth initial accumulator value.</param>
        /// <param name="accumulator9">The ninth accumulator function to be invoked on each element.</param>
        /// <param name="seed10">The tenth initial accumulator value.</param>
        /// <param name="accumulator10">The tenth accumulator function to be invoked on each element.</param>
        /// <param name="seed11">The eleventh initial accumulator value.</param>
        /// <param name="accumulator11">The eleventh accumulator function to be invoked on each element.</param>
        /// <param name="seed12">The twelfth initial accumulator value.</param>
        /// <param name="accumulator12">The twelfth accumulator function to be invoked on each element.</param>
        /// <param name="seed13">The thirteenth initial accumulator value.</param>
        /// <param name="accumulator13">The thirteenth accumulator function to be invoked on each element.</param>
        /// <param name="seed14">The fourteenth initial accumulator value.</param>
        /// <param name="accumulator14">The fourteenth accumulator function to be invoked on each element.</param>
        /// <param name="resultSelector">
        /// A function to transform the final accumulator value into the result value.</param>
        /// <returns>The transformed final accumulator value.</returns>
        /// <remarks>This method uses immediate execution semantics.</remarks>

        public static TResult Aggregate<TSource, TState1, TState2, TState3, TState4, TState5, TState6, TState7, TState8, TState9, TState10, TState11, TState12, TState13, TState14, TResult>(
            this IEnumerable<TSource> source,
            TState1 seed1, Func<TState1, TSource, TState1> accumulator1,
            TState2 seed2, Func<TState2, TSource, TState2> accumulator2,
            TState3 seed3, Func<TState3, TSource, TState3> accumulator3,
            TState4 seed4, Func<TState4, TSource, TState4> accumulator4,
            TState5 seed5, Func<TState5, TSource, TState5> accumulator5,
            TState6 seed6, Func<TState6, TSource, TState6> accumulator6,
            TState7 seed7, Func<TState7, TSource, TState7> accumulator7,
            TState8 seed8, Func<TState8, TSource, TState8> accumulator8,
            TState9 seed9, Func<TState9, TSource, TState9> accumulator9,
            TState10 seed10, Func<TState10, TSource, TState10> accumulator10,
            TState11 seed11, Func<TState11, TSource, TState11> accumulator11,
            TState12 seed12, Func<TState12, TSource, TState12> accumulator12,
            TState13 seed13, Func<TState13, TSource, TState13> accumulator13,
            TState14 seed14, Func<TState14, TSource, TState14> accumulator14,
            Func<TState1, TState2, TState3, TState4, TState5, TState6, TState7, TState8, TState9, TState10, TState11, TState12, TState13, TState14, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (accumulator1 == null) throw new ArgumentNullException(nameof(accumulator1));
            if (accumulator2 == null) throw new ArgumentNullException(nameof(accumulator2));
            if (accumulator3 == null) throw new ArgumentNullException(nameof(accumulator3));
            if (accumulator4 == null) throw new ArgumentNullException(nameof(accumulator4));
            if (accumulator5 == null) throw new ArgumentNullException(nameof(accumulator5));
            if (accumulator6 == null) throw new ArgumentNullException(nameof(accumulator6));
            if (accumulator7 == null) throw new ArgumentNullException(nameof(accumulator7));
            if (accumulator8 == null) throw new ArgumentNullException(nameof(accumulator8));
            if (accumulator9 == null) throw new ArgumentNullException(nameof(accumulator9));
            if (accumulator10 == null) throw new ArgumentNullException(nameof(accumulator10));
            if (accumulator11 == null) throw new ArgumentNullException(nameof(accumulator11));
            if (accumulator12 == null) throw new ArgumentNullException(nameof(accumulator12));
            if (accumulator13 == null) throw new ArgumentNullException(nameof(accumulator13));
            if (accumulator14 == null) throw new ArgumentNullException(nameof(accumulator14));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                source.Aggregate(
                    (seed1, seed2, seed3, seed4, seed5, seed6, seed7, seed8, seed9, seed10, seed11, seed12, seed13, seed14),
                    (s, e) => (accumulator1(s.Item1, e),
                               accumulator2(s.Item2, e),
                               accumulator3(s.Item3, e),
                               accumulator4(s.Item4, e),
                               accumulator5(s.Item5, e),
                               accumulator6(s.Item6, e),
                               accumulator7(s.Item7, e),
                               accumulator8(s.Item8, e),
                               accumulator9(s.Item9, e),
                               accumulator10(s.Item10, e),
                               accumulator11(s.Item11, e),
                               accumulator12(s.Item12, e),
                               accumulator13(s.Item13, e),
                               accumulator14(s.Item14, e)),
                    s => resultSelector(s.Item1, s.Item2, s.Item3, s.Item4, s.Item5, s.Item6, s.Item7, s.Item8, s.Item9, s.Item10, s.Item11, s.Item12, s.Item13, s.Item14));
        }

        /// <summary>
        /// Applies 15 accumulator functions over a sequence.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TState1">The type of the first accumulator value.</typeparam>
        /// <typeparam name="TState2">The type of the second accumulator value.</typeparam>
        /// <typeparam name="TState3">The type of the third accumulator value.</typeparam>
        /// <typeparam name="TState4">The type of the fourth accumulator value.</typeparam>
        /// <typeparam name="TState5">The type of the fifth accumulator value.</typeparam>
        /// <typeparam name="TState6">The type of the sixth accumulator value.</typeparam>
        /// <typeparam name="TState7">The type of the seventh accumulator value.</typeparam>
        /// <typeparam name="TState8">The type of the eighth accumulator value.</typeparam>
        /// <typeparam name="TState9">The type of the ninth accumulator value.</typeparam>
        /// <typeparam name="TState10">The type of the tenth accumulator value.</typeparam>
        /// <typeparam name="TState11">The type of the eleventh accumulator value.</typeparam>
        /// <typeparam name="TState12">The type of the twelfth accumulator value.</typeparam>
        /// <typeparam name="TState13">The type of the thirteenth accumulator value.</typeparam>
        /// <typeparam name="TState14">The type of the fourteenth accumulator value.</typeparam>
        /// <typeparam name="TState15">The type of the fifteenth accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="source">The sequence to aggregate over.</param>
        /// <param name="seed1">The first initial accumulator value.</param>
        /// <param name="accumulator1">The first accumulator function to be invoked on each element.</param>
        /// <param name="seed2">The second initial accumulator value.</param>
        /// <param name="accumulator2">The second accumulator function to be invoked on each element.</param>
        /// <param name="seed3">The third initial accumulator value.</param>
        /// <param name="accumulator3">The third accumulator function to be invoked on each element.</param>
        /// <param name="seed4">The fourth initial accumulator value.</param>
        /// <param name="accumulator4">The fourth accumulator function to be invoked on each element.</param>
        /// <param name="seed5">The fifth initial accumulator value.</param>
        /// <param name="accumulator5">The fifth accumulator function to be invoked on each element.</param>
        /// <param name="seed6">The sixth initial accumulator value.</param>
        /// <param name="accumulator6">The sixth accumulator function to be invoked on each element.</param>
        /// <param name="seed7">The seventh initial accumulator value.</param>
        /// <param name="accumulator7">The seventh accumulator function to be invoked on each element.</param>
        /// <param name="seed8">The eighth initial accumulator value.</param>
        /// <param name="accumulator8">The eighth accumulator function to be invoked on each element.</param>
        /// <param name="seed9">The ninth initial accumulator value.</param>
        /// <param name="accumulator9">The ninth accumulator function to be invoked on each element.</param>
        /// <param name="seed10">The tenth initial accumulator value.</param>
        /// <param name="accumulator10">The tenth accumulator function to be invoked on each element.</param>
        /// <param name="seed11">The eleventh initial accumulator value.</param>
        /// <param name="accumulator11">The eleventh accumulator function to be invoked on each element.</param>
        /// <param name="seed12">The twelfth initial accumulator value.</param>
        /// <param name="accumulator12">The twelfth accumulator function to be invoked on each element.</param>
        /// <param name="seed13">The thirteenth initial accumulator value.</param>
        /// <param name="accumulator13">The thirteenth accumulator function to be invoked on each element.</param>
        /// <param name="seed14">The fourteenth initial accumulator value.</param>
        /// <param name="accumulator14">The fourteenth accumulator function to be invoked on each element.</param>
        /// <param name="seed15">The fifteenth initial accumulator value.</param>
        /// <param name="accumulator15">The fifteenth accumulator function to be invoked on each element.</param>
        /// <param name="resultSelector">
        /// A function to transform the final accumulator value into the result value.</param>
        /// <returns>The transformed final accumulator value.</returns>
        /// <remarks>This method uses immediate execution semantics.</remarks>

        public static TResult Aggregate<TSource, TState1, TState2, TState3, TState4, TState5, TState6, TState7, TState8, TState9, TState10, TState11, TState12, TState13, TState14, TState15, TResult>(
            this IEnumerable<TSource> source,
            TState1 seed1, Func<TState1, TSource, TState1> accumulator1,
            TState2 seed2, Func<TState2, TSource, TState2> accumulator2,
            TState3 seed3, Func<TState3, TSource, TState3> accumulator3,
            TState4 seed4, Func<TState4, TSource, TState4> accumulator4,
            TState5 seed5, Func<TState5, TSource, TState5> accumulator5,
            TState6 seed6, Func<TState6, TSource, TState6> accumulator6,
            TState7 seed7, Func<TState7, TSource, TState7> accumulator7,
            TState8 seed8, Func<TState8, TSource, TState8> accumulator8,
            TState9 seed9, Func<TState9, TSource, TState9> accumulator9,
            TState10 seed10, Func<TState10, TSource, TState10> accumulator10,
            TState11 seed11, Func<TState11, TSource, TState11> accumulator11,
            TState12 seed12, Func<TState12, TSource, TState12> accumulator12,
            TState13 seed13, Func<TState13, TSource, TState13> accumulator13,
            TState14 seed14, Func<TState14, TSource, TState14> accumulator14,
            TState15 seed15, Func<TState15, TSource, TState15> accumulator15,
            Func<TState1, TState2, TState3, TState4, TState5, TState6, TState7, TState8, TState9, TState10, TState11, TState12, TState13, TState14, TState15, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (accumulator1 == null) throw new ArgumentNullException(nameof(accumulator1));
            if (accumulator2 == null) throw new ArgumentNullException(nameof(accumulator2));
            if (accumulator3 == null) throw new ArgumentNullException(nameof(accumulator3));
            if (accumulator4 == null) throw new ArgumentNullException(nameof(accumulator4));
            if (accumulator5 == null) throw new ArgumentNullException(nameof(accumulator5));
            if (accumulator6 == null) throw new ArgumentNullException(nameof(accumulator6));
            if (accumulator7 == null) throw new ArgumentNullException(nameof(accumulator7));
            if (accumulator8 == null) throw new ArgumentNullException(nameof(accumulator8));
            if (accumulator9 == null) throw new ArgumentNullException(nameof(accumulator9));
            if (accumulator10 == null) throw new ArgumentNullException(nameof(accumulator10));
            if (accumulator11 == null) throw new ArgumentNullException(nameof(accumulator11));
            if (accumulator12 == null) throw new ArgumentNullException(nameof(accumulator12));
            if (accumulator13 == null) throw new ArgumentNullException(nameof(accumulator13));
            if (accumulator14 == null) throw new ArgumentNullException(nameof(accumulator14));
            if (accumulator15 == null) throw new ArgumentNullException(nameof(accumulator15));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return
                source.Aggregate(
                    (seed1, seed2, seed3, seed4, seed5, seed6, seed7, seed8, seed9, seed10, seed11, seed12, seed13, seed14, seed15),
                    (s, e) => (accumulator1(s.Item1, e),
                               accumulator2(s.Item2, e),
                               accumulator3(s.Item3, e),
                               accumulator4(s.Item4, e),
                               accumulator5(s.Item5, e),
                               accumulator6(s.Item6, e),
                               accumulator7(s.Item7, e),
                               accumulator8(s.Item8, e),
                               accumulator9(s.Item9, e),
                               accumulator10(s.Item10, e),
                               accumulator11(s.Item11, e),
                               accumulator12(s.Item12, e),
                               accumulator13(s.Item13, e),
                               accumulator14(s.Item14, e),
                               accumulator15(s.Item15, e)),
                    s => resultSelector(s.Item1, s.Item2, s.Item3, s.Item4, s.Item5, s.Item6, s.Item7, s.Item8, s.Item9, s.Item10, s.Item11, s.Item12, s.Item13, s.Item14, s.Item15));
        }

    }
}

#endif // !NO_VALUE_TUPLES
