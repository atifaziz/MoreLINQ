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

#if !NO_OBSERVABLES

namespace MoreLinq
{
    using System;
    using System.Collections.Generic;

    partial class MoreEnumerable
    {
        /// <summary>
        /// Applies 2 accumulators over a sequence.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TResult1">The type of the first accumulator value.</typeparam>
        /// <typeparam name="TResult2">The type of the second accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="source">The sequence to aggregate over.</param>
        /// <param name="aggregatorConnector1">
        /// The first function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector2">
        /// The second function that connects an accumulator to the source.</param>
        /// <param name="resultSelector">
        /// A function to transform the final accumulator value into the result value.</param>
        /// <returns>The transformed final accumulator value.</returns>
        /// <remarks>This method uses immediate execution semantics.</remarks>

        public static TResult Aggregate<TSource, TResult1, TResult2, TResult>(
            this IEnumerable<TSource> source,
            Func<IObservable<TSource>, Func<TResult1>> aggregatorConnector1,
            Func<IObservable<TSource>, Func<TResult2>> aggregatorConnector2,
            Func<TResult1, TResult2, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (aggregatorConnector1 == null) throw new ArgumentNullException(nameof(aggregatorConnector1));
            if (aggregatorConnector2 == null) throw new ArgumentNullException(nameof(aggregatorConnector2));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            var s1 = new Subject<TSource>(); var c1 = aggregatorConnector1(s1);
            var s2 = new Subject<TSource>(); var c2 = aggregatorConnector2(s2);

            // TODO OnError

            foreach (var item in source)
            {
                s1.OnNext(item);
                s2.OnNext(item);
            }

            s1.OnCompleted();
            s2.OnCompleted();

            return resultSelector(c1(), c2());
        }

        /// <summary>
        /// Applies 3 accumulators over a sequence.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TResult1">The type of the first accumulator value.</typeparam>
        /// <typeparam name="TResult2">The type of the second accumulator value.</typeparam>
        /// <typeparam name="TResult3">The type of the third accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="source">The sequence to aggregate over.</param>
        /// <param name="aggregatorConnector1">
        /// The first function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector2">
        /// The second function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector3">
        /// The third function that connects an accumulator to the source.</param>
        /// <param name="resultSelector">
        /// A function to transform the final accumulator value into the result value.</param>
        /// <returns>The transformed final accumulator value.</returns>
        /// <remarks>This method uses immediate execution semantics.</remarks>

        public static TResult Aggregate<TSource, TResult1, TResult2, TResult3, TResult>(
            this IEnumerable<TSource> source,
            Func<IObservable<TSource>, Func<TResult1>> aggregatorConnector1,
            Func<IObservable<TSource>, Func<TResult2>> aggregatorConnector2,
            Func<IObservable<TSource>, Func<TResult3>> aggregatorConnector3,
            Func<TResult1, TResult2, TResult3, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (aggregatorConnector1 == null) throw new ArgumentNullException(nameof(aggregatorConnector1));
            if (aggregatorConnector2 == null) throw new ArgumentNullException(nameof(aggregatorConnector2));
            if (aggregatorConnector3 == null) throw new ArgumentNullException(nameof(aggregatorConnector3));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            var s1 = new Subject<TSource>(); var c1 = aggregatorConnector1(s1);
            var s2 = new Subject<TSource>(); var c2 = aggregatorConnector2(s2);
            var s3 = new Subject<TSource>(); var c3 = aggregatorConnector3(s3);

            // TODO OnError

            foreach (var item in source)
            {
                s1.OnNext(item);
                s2.OnNext(item);
                s3.OnNext(item);
            }

            s1.OnCompleted();
            s2.OnCompleted();
            s3.OnCompleted();

            return resultSelector(c1(), c2(), c3());
        }

        /// <summary>
        /// Applies 4 accumulators over a sequence.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TResult1">The type of the first accumulator value.</typeparam>
        /// <typeparam name="TResult2">The type of the second accumulator value.</typeparam>
        /// <typeparam name="TResult3">The type of the third accumulator value.</typeparam>
        /// <typeparam name="TResult4">The type of the fourth accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="source">The sequence to aggregate over.</param>
        /// <param name="aggregatorConnector1">
        /// The first function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector2">
        /// The second function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector3">
        /// The third function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector4">
        /// The fourth function that connects an accumulator to the source.</param>
        /// <param name="resultSelector">
        /// A function to transform the final accumulator value into the result value.</param>
        /// <returns>The transformed final accumulator value.</returns>
        /// <remarks>This method uses immediate execution semantics.</remarks>

        public static TResult Aggregate<TSource, TResult1, TResult2, TResult3, TResult4, TResult>(
            this IEnumerable<TSource> source,
            Func<IObservable<TSource>, Func<TResult1>> aggregatorConnector1,
            Func<IObservable<TSource>, Func<TResult2>> aggregatorConnector2,
            Func<IObservable<TSource>, Func<TResult3>> aggregatorConnector3,
            Func<IObservable<TSource>, Func<TResult4>> aggregatorConnector4,
            Func<TResult1, TResult2, TResult3, TResult4, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (aggregatorConnector1 == null) throw new ArgumentNullException(nameof(aggregatorConnector1));
            if (aggregatorConnector2 == null) throw new ArgumentNullException(nameof(aggregatorConnector2));
            if (aggregatorConnector3 == null) throw new ArgumentNullException(nameof(aggregatorConnector3));
            if (aggregatorConnector4 == null) throw new ArgumentNullException(nameof(aggregatorConnector4));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            var s1 = new Subject<TSource>(); var c1 = aggregatorConnector1(s1);
            var s2 = new Subject<TSource>(); var c2 = aggregatorConnector2(s2);
            var s3 = new Subject<TSource>(); var c3 = aggregatorConnector3(s3);
            var s4 = new Subject<TSource>(); var c4 = aggregatorConnector4(s4);

            // TODO OnError

            foreach (var item in source)
            {
                s1.OnNext(item);
                s2.OnNext(item);
                s3.OnNext(item);
                s4.OnNext(item);
            }

            s1.OnCompleted();
            s2.OnCompleted();
            s3.OnCompleted();
            s4.OnCompleted();

            return resultSelector(c1(), c2(), c3(), c4());
        }

        /// <summary>
        /// Applies 5 accumulators over a sequence.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TResult1">The type of the first accumulator value.</typeparam>
        /// <typeparam name="TResult2">The type of the second accumulator value.</typeparam>
        /// <typeparam name="TResult3">The type of the third accumulator value.</typeparam>
        /// <typeparam name="TResult4">The type of the fourth accumulator value.</typeparam>
        /// <typeparam name="TResult5">The type of the fifth accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="source">The sequence to aggregate over.</param>
        /// <param name="aggregatorConnector1">
        /// The first function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector2">
        /// The second function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector3">
        /// The third function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector4">
        /// The fourth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector5">
        /// The fifth function that connects an accumulator to the source.</param>
        /// <param name="resultSelector">
        /// A function to transform the final accumulator value into the result value.</param>
        /// <returns>The transformed final accumulator value.</returns>
        /// <remarks>This method uses immediate execution semantics.</remarks>

        public static TResult Aggregate<TSource, TResult1, TResult2, TResult3, TResult4, TResult5, TResult>(
            this IEnumerable<TSource> source,
            Func<IObservable<TSource>, Func<TResult1>> aggregatorConnector1,
            Func<IObservable<TSource>, Func<TResult2>> aggregatorConnector2,
            Func<IObservable<TSource>, Func<TResult3>> aggregatorConnector3,
            Func<IObservable<TSource>, Func<TResult4>> aggregatorConnector4,
            Func<IObservable<TSource>, Func<TResult5>> aggregatorConnector5,
            Func<TResult1, TResult2, TResult3, TResult4, TResult5, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (aggregatorConnector1 == null) throw new ArgumentNullException(nameof(aggregatorConnector1));
            if (aggregatorConnector2 == null) throw new ArgumentNullException(nameof(aggregatorConnector2));
            if (aggregatorConnector3 == null) throw new ArgumentNullException(nameof(aggregatorConnector3));
            if (aggregatorConnector4 == null) throw new ArgumentNullException(nameof(aggregatorConnector4));
            if (aggregatorConnector5 == null) throw new ArgumentNullException(nameof(aggregatorConnector5));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            var s1 = new Subject<TSource>(); var c1 = aggregatorConnector1(s1);
            var s2 = new Subject<TSource>(); var c2 = aggregatorConnector2(s2);
            var s3 = new Subject<TSource>(); var c3 = aggregatorConnector3(s3);
            var s4 = new Subject<TSource>(); var c4 = aggregatorConnector4(s4);
            var s5 = new Subject<TSource>(); var c5 = aggregatorConnector5(s5);

            // TODO OnError

            foreach (var item in source)
            {
                s1.OnNext(item);
                s2.OnNext(item);
                s3.OnNext(item);
                s4.OnNext(item);
                s5.OnNext(item);
            }

            s1.OnCompleted();
            s2.OnCompleted();
            s3.OnCompleted();
            s4.OnCompleted();
            s5.OnCompleted();

            return resultSelector(c1(), c2(), c3(), c4(), c5());
        }

        /// <summary>
        /// Applies 6 accumulators over a sequence.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TResult1">The type of the first accumulator value.</typeparam>
        /// <typeparam name="TResult2">The type of the second accumulator value.</typeparam>
        /// <typeparam name="TResult3">The type of the third accumulator value.</typeparam>
        /// <typeparam name="TResult4">The type of the fourth accumulator value.</typeparam>
        /// <typeparam name="TResult5">The type of the fifth accumulator value.</typeparam>
        /// <typeparam name="TResult6">The type of the sixth accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="source">The sequence to aggregate over.</param>
        /// <param name="aggregatorConnector1">
        /// The first function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector2">
        /// The second function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector3">
        /// The third function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector4">
        /// The fourth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector5">
        /// The fifth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector6">
        /// The sixth function that connects an accumulator to the source.</param>
        /// <param name="resultSelector">
        /// A function to transform the final accumulator value into the result value.</param>
        /// <returns>The transformed final accumulator value.</returns>
        /// <remarks>This method uses immediate execution semantics.</remarks>

        public static TResult Aggregate<TSource, TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult>(
            this IEnumerable<TSource> source,
            Func<IObservable<TSource>, Func<TResult1>> aggregatorConnector1,
            Func<IObservable<TSource>, Func<TResult2>> aggregatorConnector2,
            Func<IObservable<TSource>, Func<TResult3>> aggregatorConnector3,
            Func<IObservable<TSource>, Func<TResult4>> aggregatorConnector4,
            Func<IObservable<TSource>, Func<TResult5>> aggregatorConnector5,
            Func<IObservable<TSource>, Func<TResult6>> aggregatorConnector6,
            Func<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (aggregatorConnector1 == null) throw new ArgumentNullException(nameof(aggregatorConnector1));
            if (aggregatorConnector2 == null) throw new ArgumentNullException(nameof(aggregatorConnector2));
            if (aggregatorConnector3 == null) throw new ArgumentNullException(nameof(aggregatorConnector3));
            if (aggregatorConnector4 == null) throw new ArgumentNullException(nameof(aggregatorConnector4));
            if (aggregatorConnector5 == null) throw new ArgumentNullException(nameof(aggregatorConnector5));
            if (aggregatorConnector6 == null) throw new ArgumentNullException(nameof(aggregatorConnector6));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            var s1 = new Subject<TSource>(); var c1 = aggregatorConnector1(s1);
            var s2 = new Subject<TSource>(); var c2 = aggregatorConnector2(s2);
            var s3 = new Subject<TSource>(); var c3 = aggregatorConnector3(s3);
            var s4 = new Subject<TSource>(); var c4 = aggregatorConnector4(s4);
            var s5 = new Subject<TSource>(); var c5 = aggregatorConnector5(s5);
            var s6 = new Subject<TSource>(); var c6 = aggregatorConnector6(s6);

            // TODO OnError

            foreach (var item in source)
            {
                s1.OnNext(item);
                s2.OnNext(item);
                s3.OnNext(item);
                s4.OnNext(item);
                s5.OnNext(item);
                s6.OnNext(item);
            }

            s1.OnCompleted();
            s2.OnCompleted();
            s3.OnCompleted();
            s4.OnCompleted();
            s5.OnCompleted();
            s6.OnCompleted();

            return resultSelector(c1(), c2(), c3(), c4(), c5(), c6());
        }

        /// <summary>
        /// Applies 7 accumulators over a sequence.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TResult1">The type of the first accumulator value.</typeparam>
        /// <typeparam name="TResult2">The type of the second accumulator value.</typeparam>
        /// <typeparam name="TResult3">The type of the third accumulator value.</typeparam>
        /// <typeparam name="TResult4">The type of the fourth accumulator value.</typeparam>
        /// <typeparam name="TResult5">The type of the fifth accumulator value.</typeparam>
        /// <typeparam name="TResult6">The type of the sixth accumulator value.</typeparam>
        /// <typeparam name="TResult7">The type of the seventh accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="source">The sequence to aggregate over.</param>
        /// <param name="aggregatorConnector1">
        /// The first function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector2">
        /// The second function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector3">
        /// The third function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector4">
        /// The fourth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector5">
        /// The fifth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector6">
        /// The sixth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector7">
        /// The seventh function that connects an accumulator to the source.</param>
        /// <param name="resultSelector">
        /// A function to transform the final accumulator value into the result value.</param>
        /// <returns>The transformed final accumulator value.</returns>
        /// <remarks>This method uses immediate execution semantics.</remarks>

        public static TResult Aggregate<TSource, TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult>(
            this IEnumerable<TSource> source,
            Func<IObservable<TSource>, Func<TResult1>> aggregatorConnector1,
            Func<IObservable<TSource>, Func<TResult2>> aggregatorConnector2,
            Func<IObservable<TSource>, Func<TResult3>> aggregatorConnector3,
            Func<IObservable<TSource>, Func<TResult4>> aggregatorConnector4,
            Func<IObservable<TSource>, Func<TResult5>> aggregatorConnector5,
            Func<IObservable<TSource>, Func<TResult6>> aggregatorConnector6,
            Func<IObservable<TSource>, Func<TResult7>> aggregatorConnector7,
            Func<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (aggregatorConnector1 == null) throw new ArgumentNullException(nameof(aggregatorConnector1));
            if (aggregatorConnector2 == null) throw new ArgumentNullException(nameof(aggregatorConnector2));
            if (aggregatorConnector3 == null) throw new ArgumentNullException(nameof(aggregatorConnector3));
            if (aggregatorConnector4 == null) throw new ArgumentNullException(nameof(aggregatorConnector4));
            if (aggregatorConnector5 == null) throw new ArgumentNullException(nameof(aggregatorConnector5));
            if (aggregatorConnector6 == null) throw new ArgumentNullException(nameof(aggregatorConnector6));
            if (aggregatorConnector7 == null) throw new ArgumentNullException(nameof(aggregatorConnector7));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            var s1 = new Subject<TSource>(); var c1 = aggregatorConnector1(s1);
            var s2 = new Subject<TSource>(); var c2 = aggregatorConnector2(s2);
            var s3 = new Subject<TSource>(); var c3 = aggregatorConnector3(s3);
            var s4 = new Subject<TSource>(); var c4 = aggregatorConnector4(s4);
            var s5 = new Subject<TSource>(); var c5 = aggregatorConnector5(s5);
            var s6 = new Subject<TSource>(); var c6 = aggregatorConnector6(s6);
            var s7 = new Subject<TSource>(); var c7 = aggregatorConnector7(s7);

            // TODO OnError

            foreach (var item in source)
            {
                s1.OnNext(item);
                s2.OnNext(item);
                s3.OnNext(item);
                s4.OnNext(item);
                s5.OnNext(item);
                s6.OnNext(item);
                s7.OnNext(item);
            }

            s1.OnCompleted();
            s2.OnCompleted();
            s3.OnCompleted();
            s4.OnCompleted();
            s5.OnCompleted();
            s6.OnCompleted();
            s7.OnCompleted();

            return resultSelector(c1(), c2(), c3(), c4(), c5(), c6(), c7());
        }

        /// <summary>
        /// Applies 8 accumulators over a sequence.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TResult1">The type of the first accumulator value.</typeparam>
        /// <typeparam name="TResult2">The type of the second accumulator value.</typeparam>
        /// <typeparam name="TResult3">The type of the third accumulator value.</typeparam>
        /// <typeparam name="TResult4">The type of the fourth accumulator value.</typeparam>
        /// <typeparam name="TResult5">The type of the fifth accumulator value.</typeparam>
        /// <typeparam name="TResult6">The type of the sixth accumulator value.</typeparam>
        /// <typeparam name="TResult7">The type of the seventh accumulator value.</typeparam>
        /// <typeparam name="TResult8">The type of the eighth accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="source">The sequence to aggregate over.</param>
        /// <param name="aggregatorConnector1">
        /// The first function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector2">
        /// The second function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector3">
        /// The third function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector4">
        /// The fourth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector5">
        /// The fifth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector6">
        /// The sixth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector7">
        /// The seventh function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector8">
        /// The eighth function that connects an accumulator to the source.</param>
        /// <param name="resultSelector">
        /// A function to transform the final accumulator value into the result value.</param>
        /// <returns>The transformed final accumulator value.</returns>
        /// <remarks>This method uses immediate execution semantics.</remarks>

        public static TResult Aggregate<TSource, TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult>(
            this IEnumerable<TSource> source,
            Func<IObservable<TSource>, Func<TResult1>> aggregatorConnector1,
            Func<IObservable<TSource>, Func<TResult2>> aggregatorConnector2,
            Func<IObservable<TSource>, Func<TResult3>> aggregatorConnector3,
            Func<IObservable<TSource>, Func<TResult4>> aggregatorConnector4,
            Func<IObservable<TSource>, Func<TResult5>> aggregatorConnector5,
            Func<IObservable<TSource>, Func<TResult6>> aggregatorConnector6,
            Func<IObservable<TSource>, Func<TResult7>> aggregatorConnector7,
            Func<IObservable<TSource>, Func<TResult8>> aggregatorConnector8,
            Func<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (aggregatorConnector1 == null) throw new ArgumentNullException(nameof(aggregatorConnector1));
            if (aggregatorConnector2 == null) throw new ArgumentNullException(nameof(aggregatorConnector2));
            if (aggregatorConnector3 == null) throw new ArgumentNullException(nameof(aggregatorConnector3));
            if (aggregatorConnector4 == null) throw new ArgumentNullException(nameof(aggregatorConnector4));
            if (aggregatorConnector5 == null) throw new ArgumentNullException(nameof(aggregatorConnector5));
            if (aggregatorConnector6 == null) throw new ArgumentNullException(nameof(aggregatorConnector6));
            if (aggregatorConnector7 == null) throw new ArgumentNullException(nameof(aggregatorConnector7));
            if (aggregatorConnector8 == null) throw new ArgumentNullException(nameof(aggregatorConnector8));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            var s1 = new Subject<TSource>(); var c1 = aggregatorConnector1(s1);
            var s2 = new Subject<TSource>(); var c2 = aggregatorConnector2(s2);
            var s3 = new Subject<TSource>(); var c3 = aggregatorConnector3(s3);
            var s4 = new Subject<TSource>(); var c4 = aggregatorConnector4(s4);
            var s5 = new Subject<TSource>(); var c5 = aggregatorConnector5(s5);
            var s6 = new Subject<TSource>(); var c6 = aggregatorConnector6(s6);
            var s7 = new Subject<TSource>(); var c7 = aggregatorConnector7(s7);
            var s8 = new Subject<TSource>(); var c8 = aggregatorConnector8(s8);

            // TODO OnError

            foreach (var item in source)
            {
                s1.OnNext(item);
                s2.OnNext(item);
                s3.OnNext(item);
                s4.OnNext(item);
                s5.OnNext(item);
                s6.OnNext(item);
                s7.OnNext(item);
                s8.OnNext(item);
            }

            s1.OnCompleted();
            s2.OnCompleted();
            s3.OnCompleted();
            s4.OnCompleted();
            s5.OnCompleted();
            s6.OnCompleted();
            s7.OnCompleted();
            s8.OnCompleted();

            return resultSelector(c1(), c2(), c3(), c4(), c5(), c6(), c7(), c8());
        }

        /// <summary>
        /// Applies 9 accumulators over a sequence.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TResult1">The type of the first accumulator value.</typeparam>
        /// <typeparam name="TResult2">The type of the second accumulator value.</typeparam>
        /// <typeparam name="TResult3">The type of the third accumulator value.</typeparam>
        /// <typeparam name="TResult4">The type of the fourth accumulator value.</typeparam>
        /// <typeparam name="TResult5">The type of the fifth accumulator value.</typeparam>
        /// <typeparam name="TResult6">The type of the sixth accumulator value.</typeparam>
        /// <typeparam name="TResult7">The type of the seventh accumulator value.</typeparam>
        /// <typeparam name="TResult8">The type of the eighth accumulator value.</typeparam>
        /// <typeparam name="TResult9">The type of the ninth accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="source">The sequence to aggregate over.</param>
        /// <param name="aggregatorConnector1">
        /// The first function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector2">
        /// The second function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector3">
        /// The third function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector4">
        /// The fourth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector5">
        /// The fifth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector6">
        /// The sixth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector7">
        /// The seventh function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector8">
        /// The eighth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector9">
        /// The ninth function that connects an accumulator to the source.</param>
        /// <param name="resultSelector">
        /// A function to transform the final accumulator value into the result value.</param>
        /// <returns>The transformed final accumulator value.</returns>
        /// <remarks>This method uses immediate execution semantics.</remarks>

        public static TResult Aggregate<TSource, TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9, TResult>(
            this IEnumerable<TSource> source,
            Func<IObservable<TSource>, Func<TResult1>> aggregatorConnector1,
            Func<IObservable<TSource>, Func<TResult2>> aggregatorConnector2,
            Func<IObservable<TSource>, Func<TResult3>> aggregatorConnector3,
            Func<IObservable<TSource>, Func<TResult4>> aggregatorConnector4,
            Func<IObservable<TSource>, Func<TResult5>> aggregatorConnector5,
            Func<IObservable<TSource>, Func<TResult6>> aggregatorConnector6,
            Func<IObservable<TSource>, Func<TResult7>> aggregatorConnector7,
            Func<IObservable<TSource>, Func<TResult8>> aggregatorConnector8,
            Func<IObservable<TSource>, Func<TResult9>> aggregatorConnector9,
            Func<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (aggregatorConnector1 == null) throw new ArgumentNullException(nameof(aggregatorConnector1));
            if (aggregatorConnector2 == null) throw new ArgumentNullException(nameof(aggregatorConnector2));
            if (aggregatorConnector3 == null) throw new ArgumentNullException(nameof(aggregatorConnector3));
            if (aggregatorConnector4 == null) throw new ArgumentNullException(nameof(aggregatorConnector4));
            if (aggregatorConnector5 == null) throw new ArgumentNullException(nameof(aggregatorConnector5));
            if (aggregatorConnector6 == null) throw new ArgumentNullException(nameof(aggregatorConnector6));
            if (aggregatorConnector7 == null) throw new ArgumentNullException(nameof(aggregatorConnector7));
            if (aggregatorConnector8 == null) throw new ArgumentNullException(nameof(aggregatorConnector8));
            if (aggregatorConnector9 == null) throw new ArgumentNullException(nameof(aggregatorConnector9));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            var s1 = new Subject<TSource>(); var c1 = aggregatorConnector1(s1);
            var s2 = new Subject<TSource>(); var c2 = aggregatorConnector2(s2);
            var s3 = new Subject<TSource>(); var c3 = aggregatorConnector3(s3);
            var s4 = new Subject<TSource>(); var c4 = aggregatorConnector4(s4);
            var s5 = new Subject<TSource>(); var c5 = aggregatorConnector5(s5);
            var s6 = new Subject<TSource>(); var c6 = aggregatorConnector6(s6);
            var s7 = new Subject<TSource>(); var c7 = aggregatorConnector7(s7);
            var s8 = new Subject<TSource>(); var c8 = aggregatorConnector8(s8);
            var s9 = new Subject<TSource>(); var c9 = aggregatorConnector9(s9);

            // TODO OnError

            foreach (var item in source)
            {
                s1.OnNext(item);
                s2.OnNext(item);
                s3.OnNext(item);
                s4.OnNext(item);
                s5.OnNext(item);
                s6.OnNext(item);
                s7.OnNext(item);
                s8.OnNext(item);
                s9.OnNext(item);
            }

            s1.OnCompleted();
            s2.OnCompleted();
            s3.OnCompleted();
            s4.OnCompleted();
            s5.OnCompleted();
            s6.OnCompleted();
            s7.OnCompleted();
            s8.OnCompleted();
            s9.OnCompleted();

            return resultSelector(c1(), c2(), c3(), c4(), c5(), c6(), c7(), c8(), c9());
        }

        /// <summary>
        /// Applies 10 accumulators over a sequence.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TResult1">The type of the first accumulator value.</typeparam>
        /// <typeparam name="TResult2">The type of the second accumulator value.</typeparam>
        /// <typeparam name="TResult3">The type of the third accumulator value.</typeparam>
        /// <typeparam name="TResult4">The type of the fourth accumulator value.</typeparam>
        /// <typeparam name="TResult5">The type of the fifth accumulator value.</typeparam>
        /// <typeparam name="TResult6">The type of the sixth accumulator value.</typeparam>
        /// <typeparam name="TResult7">The type of the seventh accumulator value.</typeparam>
        /// <typeparam name="TResult8">The type of the eighth accumulator value.</typeparam>
        /// <typeparam name="TResult9">The type of the ninth accumulator value.</typeparam>
        /// <typeparam name="TResult10">The type of the tenth accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="source">The sequence to aggregate over.</param>
        /// <param name="aggregatorConnector1">
        /// The first function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector2">
        /// The second function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector3">
        /// The third function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector4">
        /// The fourth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector5">
        /// The fifth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector6">
        /// The sixth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector7">
        /// The seventh function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector8">
        /// The eighth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector9">
        /// The ninth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector10">
        /// The tenth function that connects an accumulator to the source.</param>
        /// <param name="resultSelector">
        /// A function to transform the final accumulator value into the result value.</param>
        /// <returns>The transformed final accumulator value.</returns>
        /// <remarks>This method uses immediate execution semantics.</remarks>

        public static TResult Aggregate<TSource, TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9, TResult10, TResult>(
            this IEnumerable<TSource> source,
            Func<IObservable<TSource>, Func<TResult1>> aggregatorConnector1,
            Func<IObservable<TSource>, Func<TResult2>> aggregatorConnector2,
            Func<IObservable<TSource>, Func<TResult3>> aggregatorConnector3,
            Func<IObservable<TSource>, Func<TResult4>> aggregatorConnector4,
            Func<IObservable<TSource>, Func<TResult5>> aggregatorConnector5,
            Func<IObservable<TSource>, Func<TResult6>> aggregatorConnector6,
            Func<IObservable<TSource>, Func<TResult7>> aggregatorConnector7,
            Func<IObservable<TSource>, Func<TResult8>> aggregatorConnector8,
            Func<IObservable<TSource>, Func<TResult9>> aggregatorConnector9,
            Func<IObservable<TSource>, Func<TResult10>> aggregatorConnector10,
            Func<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9, TResult10, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (aggregatorConnector1 == null) throw new ArgumentNullException(nameof(aggregatorConnector1));
            if (aggregatorConnector2 == null) throw new ArgumentNullException(nameof(aggregatorConnector2));
            if (aggregatorConnector3 == null) throw new ArgumentNullException(nameof(aggregatorConnector3));
            if (aggregatorConnector4 == null) throw new ArgumentNullException(nameof(aggregatorConnector4));
            if (aggregatorConnector5 == null) throw new ArgumentNullException(nameof(aggregatorConnector5));
            if (aggregatorConnector6 == null) throw new ArgumentNullException(nameof(aggregatorConnector6));
            if (aggregatorConnector7 == null) throw new ArgumentNullException(nameof(aggregatorConnector7));
            if (aggregatorConnector8 == null) throw new ArgumentNullException(nameof(aggregatorConnector8));
            if (aggregatorConnector9 == null) throw new ArgumentNullException(nameof(aggregatorConnector9));
            if (aggregatorConnector10 == null) throw new ArgumentNullException(nameof(aggregatorConnector10));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            var s1 = new Subject<TSource>(); var c1 = aggregatorConnector1(s1);
            var s2 = new Subject<TSource>(); var c2 = aggregatorConnector2(s2);
            var s3 = new Subject<TSource>(); var c3 = aggregatorConnector3(s3);
            var s4 = new Subject<TSource>(); var c4 = aggregatorConnector4(s4);
            var s5 = new Subject<TSource>(); var c5 = aggregatorConnector5(s5);
            var s6 = new Subject<TSource>(); var c6 = aggregatorConnector6(s6);
            var s7 = new Subject<TSource>(); var c7 = aggregatorConnector7(s7);
            var s8 = new Subject<TSource>(); var c8 = aggregatorConnector8(s8);
            var s9 = new Subject<TSource>(); var c9 = aggregatorConnector9(s9);
            var s10 = new Subject<TSource>(); var c10 = aggregatorConnector10(s10);

            // TODO OnError

            foreach (var item in source)
            {
                s1.OnNext(item);
                s2.OnNext(item);
                s3.OnNext(item);
                s4.OnNext(item);
                s5.OnNext(item);
                s6.OnNext(item);
                s7.OnNext(item);
                s8.OnNext(item);
                s9.OnNext(item);
                s10.OnNext(item);
            }

            s1.OnCompleted();
            s2.OnCompleted();
            s3.OnCompleted();
            s4.OnCompleted();
            s5.OnCompleted();
            s6.OnCompleted();
            s7.OnCompleted();
            s8.OnCompleted();
            s9.OnCompleted();
            s10.OnCompleted();

            return resultSelector(c1(), c2(), c3(), c4(), c5(), c6(), c7(), c8(), c9(), c10());
        }

        /// <summary>
        /// Applies 11 accumulators over a sequence.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TResult1">The type of the first accumulator value.</typeparam>
        /// <typeparam name="TResult2">The type of the second accumulator value.</typeparam>
        /// <typeparam name="TResult3">The type of the third accumulator value.</typeparam>
        /// <typeparam name="TResult4">The type of the fourth accumulator value.</typeparam>
        /// <typeparam name="TResult5">The type of the fifth accumulator value.</typeparam>
        /// <typeparam name="TResult6">The type of the sixth accumulator value.</typeparam>
        /// <typeparam name="TResult7">The type of the seventh accumulator value.</typeparam>
        /// <typeparam name="TResult8">The type of the eighth accumulator value.</typeparam>
        /// <typeparam name="TResult9">The type of the ninth accumulator value.</typeparam>
        /// <typeparam name="TResult10">The type of the tenth accumulator value.</typeparam>
        /// <typeparam name="TResult11">The type of the eleventh accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="source">The sequence to aggregate over.</param>
        /// <param name="aggregatorConnector1">
        /// The first function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector2">
        /// The second function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector3">
        /// The third function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector4">
        /// The fourth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector5">
        /// The fifth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector6">
        /// The sixth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector7">
        /// The seventh function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector8">
        /// The eighth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector9">
        /// The ninth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector10">
        /// The tenth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector11">
        /// The eleventh function that connects an accumulator to the source.</param>
        /// <param name="resultSelector">
        /// A function to transform the final accumulator value into the result value.</param>
        /// <returns>The transformed final accumulator value.</returns>
        /// <remarks>This method uses immediate execution semantics.</remarks>

        public static TResult Aggregate<TSource, TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9, TResult10, TResult11, TResult>(
            this IEnumerable<TSource> source,
            Func<IObservable<TSource>, Func<TResult1>> aggregatorConnector1,
            Func<IObservable<TSource>, Func<TResult2>> aggregatorConnector2,
            Func<IObservable<TSource>, Func<TResult3>> aggregatorConnector3,
            Func<IObservable<TSource>, Func<TResult4>> aggregatorConnector4,
            Func<IObservable<TSource>, Func<TResult5>> aggregatorConnector5,
            Func<IObservable<TSource>, Func<TResult6>> aggregatorConnector6,
            Func<IObservable<TSource>, Func<TResult7>> aggregatorConnector7,
            Func<IObservable<TSource>, Func<TResult8>> aggregatorConnector8,
            Func<IObservable<TSource>, Func<TResult9>> aggregatorConnector9,
            Func<IObservable<TSource>, Func<TResult10>> aggregatorConnector10,
            Func<IObservable<TSource>, Func<TResult11>> aggregatorConnector11,
            Func<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9, TResult10, TResult11, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (aggregatorConnector1 == null) throw new ArgumentNullException(nameof(aggregatorConnector1));
            if (aggregatorConnector2 == null) throw new ArgumentNullException(nameof(aggregatorConnector2));
            if (aggregatorConnector3 == null) throw new ArgumentNullException(nameof(aggregatorConnector3));
            if (aggregatorConnector4 == null) throw new ArgumentNullException(nameof(aggregatorConnector4));
            if (aggregatorConnector5 == null) throw new ArgumentNullException(nameof(aggregatorConnector5));
            if (aggregatorConnector6 == null) throw new ArgumentNullException(nameof(aggregatorConnector6));
            if (aggregatorConnector7 == null) throw new ArgumentNullException(nameof(aggregatorConnector7));
            if (aggregatorConnector8 == null) throw new ArgumentNullException(nameof(aggregatorConnector8));
            if (aggregatorConnector9 == null) throw new ArgumentNullException(nameof(aggregatorConnector9));
            if (aggregatorConnector10 == null) throw new ArgumentNullException(nameof(aggregatorConnector10));
            if (aggregatorConnector11 == null) throw new ArgumentNullException(nameof(aggregatorConnector11));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            var s1 = new Subject<TSource>(); var c1 = aggregatorConnector1(s1);
            var s2 = new Subject<TSource>(); var c2 = aggregatorConnector2(s2);
            var s3 = new Subject<TSource>(); var c3 = aggregatorConnector3(s3);
            var s4 = new Subject<TSource>(); var c4 = aggregatorConnector4(s4);
            var s5 = new Subject<TSource>(); var c5 = aggregatorConnector5(s5);
            var s6 = new Subject<TSource>(); var c6 = aggregatorConnector6(s6);
            var s7 = new Subject<TSource>(); var c7 = aggregatorConnector7(s7);
            var s8 = new Subject<TSource>(); var c8 = aggregatorConnector8(s8);
            var s9 = new Subject<TSource>(); var c9 = aggregatorConnector9(s9);
            var s10 = new Subject<TSource>(); var c10 = aggregatorConnector10(s10);
            var s11 = new Subject<TSource>(); var c11 = aggregatorConnector11(s11);

            // TODO OnError

            foreach (var item in source)
            {
                s1.OnNext(item);
                s2.OnNext(item);
                s3.OnNext(item);
                s4.OnNext(item);
                s5.OnNext(item);
                s6.OnNext(item);
                s7.OnNext(item);
                s8.OnNext(item);
                s9.OnNext(item);
                s10.OnNext(item);
                s11.OnNext(item);
            }

            s1.OnCompleted();
            s2.OnCompleted();
            s3.OnCompleted();
            s4.OnCompleted();
            s5.OnCompleted();
            s6.OnCompleted();
            s7.OnCompleted();
            s8.OnCompleted();
            s9.OnCompleted();
            s10.OnCompleted();
            s11.OnCompleted();

            return resultSelector(c1(), c2(), c3(), c4(), c5(), c6(), c7(), c8(), c9(), c10(), c11());
        }

        /// <summary>
        /// Applies 12 accumulators over a sequence.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TResult1">The type of the first accumulator value.</typeparam>
        /// <typeparam name="TResult2">The type of the second accumulator value.</typeparam>
        /// <typeparam name="TResult3">The type of the third accumulator value.</typeparam>
        /// <typeparam name="TResult4">The type of the fourth accumulator value.</typeparam>
        /// <typeparam name="TResult5">The type of the fifth accumulator value.</typeparam>
        /// <typeparam name="TResult6">The type of the sixth accumulator value.</typeparam>
        /// <typeparam name="TResult7">The type of the seventh accumulator value.</typeparam>
        /// <typeparam name="TResult8">The type of the eighth accumulator value.</typeparam>
        /// <typeparam name="TResult9">The type of the ninth accumulator value.</typeparam>
        /// <typeparam name="TResult10">The type of the tenth accumulator value.</typeparam>
        /// <typeparam name="TResult11">The type of the eleventh accumulator value.</typeparam>
        /// <typeparam name="TResult12">The type of the twelfth accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="source">The sequence to aggregate over.</param>
        /// <param name="aggregatorConnector1">
        /// The first function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector2">
        /// The second function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector3">
        /// The third function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector4">
        /// The fourth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector5">
        /// The fifth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector6">
        /// The sixth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector7">
        /// The seventh function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector8">
        /// The eighth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector9">
        /// The ninth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector10">
        /// The tenth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector11">
        /// The eleventh function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector12">
        /// The twelfth function that connects an accumulator to the source.</param>
        /// <param name="resultSelector">
        /// A function to transform the final accumulator value into the result value.</param>
        /// <returns>The transformed final accumulator value.</returns>
        /// <remarks>This method uses immediate execution semantics.</remarks>

        public static TResult Aggregate<TSource, TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9, TResult10, TResult11, TResult12, TResult>(
            this IEnumerable<TSource> source,
            Func<IObservable<TSource>, Func<TResult1>> aggregatorConnector1,
            Func<IObservable<TSource>, Func<TResult2>> aggregatorConnector2,
            Func<IObservable<TSource>, Func<TResult3>> aggregatorConnector3,
            Func<IObservable<TSource>, Func<TResult4>> aggregatorConnector4,
            Func<IObservable<TSource>, Func<TResult5>> aggregatorConnector5,
            Func<IObservable<TSource>, Func<TResult6>> aggregatorConnector6,
            Func<IObservable<TSource>, Func<TResult7>> aggregatorConnector7,
            Func<IObservable<TSource>, Func<TResult8>> aggregatorConnector8,
            Func<IObservable<TSource>, Func<TResult9>> aggregatorConnector9,
            Func<IObservable<TSource>, Func<TResult10>> aggregatorConnector10,
            Func<IObservable<TSource>, Func<TResult11>> aggregatorConnector11,
            Func<IObservable<TSource>, Func<TResult12>> aggregatorConnector12,
            Func<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9, TResult10, TResult11, TResult12, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (aggregatorConnector1 == null) throw new ArgumentNullException(nameof(aggregatorConnector1));
            if (aggregatorConnector2 == null) throw new ArgumentNullException(nameof(aggregatorConnector2));
            if (aggregatorConnector3 == null) throw new ArgumentNullException(nameof(aggregatorConnector3));
            if (aggregatorConnector4 == null) throw new ArgumentNullException(nameof(aggregatorConnector4));
            if (aggregatorConnector5 == null) throw new ArgumentNullException(nameof(aggregatorConnector5));
            if (aggregatorConnector6 == null) throw new ArgumentNullException(nameof(aggregatorConnector6));
            if (aggregatorConnector7 == null) throw new ArgumentNullException(nameof(aggregatorConnector7));
            if (aggregatorConnector8 == null) throw new ArgumentNullException(nameof(aggregatorConnector8));
            if (aggregatorConnector9 == null) throw new ArgumentNullException(nameof(aggregatorConnector9));
            if (aggregatorConnector10 == null) throw new ArgumentNullException(nameof(aggregatorConnector10));
            if (aggregatorConnector11 == null) throw new ArgumentNullException(nameof(aggregatorConnector11));
            if (aggregatorConnector12 == null) throw new ArgumentNullException(nameof(aggregatorConnector12));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            var s1 = new Subject<TSource>(); var c1 = aggregatorConnector1(s1);
            var s2 = new Subject<TSource>(); var c2 = aggregatorConnector2(s2);
            var s3 = new Subject<TSource>(); var c3 = aggregatorConnector3(s3);
            var s4 = new Subject<TSource>(); var c4 = aggregatorConnector4(s4);
            var s5 = new Subject<TSource>(); var c5 = aggregatorConnector5(s5);
            var s6 = new Subject<TSource>(); var c6 = aggregatorConnector6(s6);
            var s7 = new Subject<TSource>(); var c7 = aggregatorConnector7(s7);
            var s8 = new Subject<TSource>(); var c8 = aggregatorConnector8(s8);
            var s9 = new Subject<TSource>(); var c9 = aggregatorConnector9(s9);
            var s10 = new Subject<TSource>(); var c10 = aggregatorConnector10(s10);
            var s11 = new Subject<TSource>(); var c11 = aggregatorConnector11(s11);
            var s12 = new Subject<TSource>(); var c12 = aggregatorConnector12(s12);

            // TODO OnError

            foreach (var item in source)
            {
                s1.OnNext(item);
                s2.OnNext(item);
                s3.OnNext(item);
                s4.OnNext(item);
                s5.OnNext(item);
                s6.OnNext(item);
                s7.OnNext(item);
                s8.OnNext(item);
                s9.OnNext(item);
                s10.OnNext(item);
                s11.OnNext(item);
                s12.OnNext(item);
            }

            s1.OnCompleted();
            s2.OnCompleted();
            s3.OnCompleted();
            s4.OnCompleted();
            s5.OnCompleted();
            s6.OnCompleted();
            s7.OnCompleted();
            s8.OnCompleted();
            s9.OnCompleted();
            s10.OnCompleted();
            s11.OnCompleted();
            s12.OnCompleted();

            return resultSelector(c1(), c2(), c3(), c4(), c5(), c6(), c7(), c8(), c9(), c10(), c11(), c12());
        }

        /// <summary>
        /// Applies 13 accumulators over a sequence.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TResult1">The type of the first accumulator value.</typeparam>
        /// <typeparam name="TResult2">The type of the second accumulator value.</typeparam>
        /// <typeparam name="TResult3">The type of the third accumulator value.</typeparam>
        /// <typeparam name="TResult4">The type of the fourth accumulator value.</typeparam>
        /// <typeparam name="TResult5">The type of the fifth accumulator value.</typeparam>
        /// <typeparam name="TResult6">The type of the sixth accumulator value.</typeparam>
        /// <typeparam name="TResult7">The type of the seventh accumulator value.</typeparam>
        /// <typeparam name="TResult8">The type of the eighth accumulator value.</typeparam>
        /// <typeparam name="TResult9">The type of the ninth accumulator value.</typeparam>
        /// <typeparam name="TResult10">The type of the tenth accumulator value.</typeparam>
        /// <typeparam name="TResult11">The type of the eleventh accumulator value.</typeparam>
        /// <typeparam name="TResult12">The type of the twelfth accumulator value.</typeparam>
        /// <typeparam name="TResult13">The type of the thirteenth accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="source">The sequence to aggregate over.</param>
        /// <param name="aggregatorConnector1">
        /// The first function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector2">
        /// The second function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector3">
        /// The third function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector4">
        /// The fourth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector5">
        /// The fifth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector6">
        /// The sixth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector7">
        /// The seventh function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector8">
        /// The eighth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector9">
        /// The ninth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector10">
        /// The tenth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector11">
        /// The eleventh function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector12">
        /// The twelfth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector13">
        /// The thirteenth function that connects an accumulator to the source.</param>
        /// <param name="resultSelector">
        /// A function to transform the final accumulator value into the result value.</param>
        /// <returns>The transformed final accumulator value.</returns>
        /// <remarks>This method uses immediate execution semantics.</remarks>

        public static TResult Aggregate<TSource, TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9, TResult10, TResult11, TResult12, TResult13, TResult>(
            this IEnumerable<TSource> source,
            Func<IObservable<TSource>, Func<TResult1>> aggregatorConnector1,
            Func<IObservable<TSource>, Func<TResult2>> aggregatorConnector2,
            Func<IObservable<TSource>, Func<TResult3>> aggregatorConnector3,
            Func<IObservable<TSource>, Func<TResult4>> aggregatorConnector4,
            Func<IObservable<TSource>, Func<TResult5>> aggregatorConnector5,
            Func<IObservable<TSource>, Func<TResult6>> aggregatorConnector6,
            Func<IObservable<TSource>, Func<TResult7>> aggregatorConnector7,
            Func<IObservable<TSource>, Func<TResult8>> aggregatorConnector8,
            Func<IObservable<TSource>, Func<TResult9>> aggregatorConnector9,
            Func<IObservable<TSource>, Func<TResult10>> aggregatorConnector10,
            Func<IObservable<TSource>, Func<TResult11>> aggregatorConnector11,
            Func<IObservable<TSource>, Func<TResult12>> aggregatorConnector12,
            Func<IObservable<TSource>, Func<TResult13>> aggregatorConnector13,
            Func<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9, TResult10, TResult11, TResult12, TResult13, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (aggregatorConnector1 == null) throw new ArgumentNullException(nameof(aggregatorConnector1));
            if (aggregatorConnector2 == null) throw new ArgumentNullException(nameof(aggregatorConnector2));
            if (aggregatorConnector3 == null) throw new ArgumentNullException(nameof(aggregatorConnector3));
            if (aggregatorConnector4 == null) throw new ArgumentNullException(nameof(aggregatorConnector4));
            if (aggregatorConnector5 == null) throw new ArgumentNullException(nameof(aggregatorConnector5));
            if (aggregatorConnector6 == null) throw new ArgumentNullException(nameof(aggregatorConnector6));
            if (aggregatorConnector7 == null) throw new ArgumentNullException(nameof(aggregatorConnector7));
            if (aggregatorConnector8 == null) throw new ArgumentNullException(nameof(aggregatorConnector8));
            if (aggregatorConnector9 == null) throw new ArgumentNullException(nameof(aggregatorConnector9));
            if (aggregatorConnector10 == null) throw new ArgumentNullException(nameof(aggregatorConnector10));
            if (aggregatorConnector11 == null) throw new ArgumentNullException(nameof(aggregatorConnector11));
            if (aggregatorConnector12 == null) throw new ArgumentNullException(nameof(aggregatorConnector12));
            if (aggregatorConnector13 == null) throw new ArgumentNullException(nameof(aggregatorConnector13));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            var s1 = new Subject<TSource>(); var c1 = aggregatorConnector1(s1);
            var s2 = new Subject<TSource>(); var c2 = aggregatorConnector2(s2);
            var s3 = new Subject<TSource>(); var c3 = aggregatorConnector3(s3);
            var s4 = new Subject<TSource>(); var c4 = aggregatorConnector4(s4);
            var s5 = new Subject<TSource>(); var c5 = aggregatorConnector5(s5);
            var s6 = new Subject<TSource>(); var c6 = aggregatorConnector6(s6);
            var s7 = new Subject<TSource>(); var c7 = aggregatorConnector7(s7);
            var s8 = new Subject<TSource>(); var c8 = aggregatorConnector8(s8);
            var s9 = new Subject<TSource>(); var c9 = aggregatorConnector9(s9);
            var s10 = new Subject<TSource>(); var c10 = aggregatorConnector10(s10);
            var s11 = new Subject<TSource>(); var c11 = aggregatorConnector11(s11);
            var s12 = new Subject<TSource>(); var c12 = aggregatorConnector12(s12);
            var s13 = new Subject<TSource>(); var c13 = aggregatorConnector13(s13);

            // TODO OnError

            foreach (var item in source)
            {
                s1.OnNext(item);
                s2.OnNext(item);
                s3.OnNext(item);
                s4.OnNext(item);
                s5.OnNext(item);
                s6.OnNext(item);
                s7.OnNext(item);
                s8.OnNext(item);
                s9.OnNext(item);
                s10.OnNext(item);
                s11.OnNext(item);
                s12.OnNext(item);
                s13.OnNext(item);
            }

            s1.OnCompleted();
            s2.OnCompleted();
            s3.OnCompleted();
            s4.OnCompleted();
            s5.OnCompleted();
            s6.OnCompleted();
            s7.OnCompleted();
            s8.OnCompleted();
            s9.OnCompleted();
            s10.OnCompleted();
            s11.OnCompleted();
            s12.OnCompleted();
            s13.OnCompleted();

            return resultSelector(c1(), c2(), c3(), c4(), c5(), c6(), c7(), c8(), c9(), c10(), c11(), c12(), c13());
        }

        /// <summary>
        /// Applies 14 accumulators over a sequence.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TResult1">The type of the first accumulator value.</typeparam>
        /// <typeparam name="TResult2">The type of the second accumulator value.</typeparam>
        /// <typeparam name="TResult3">The type of the third accumulator value.</typeparam>
        /// <typeparam name="TResult4">The type of the fourth accumulator value.</typeparam>
        /// <typeparam name="TResult5">The type of the fifth accumulator value.</typeparam>
        /// <typeparam name="TResult6">The type of the sixth accumulator value.</typeparam>
        /// <typeparam name="TResult7">The type of the seventh accumulator value.</typeparam>
        /// <typeparam name="TResult8">The type of the eighth accumulator value.</typeparam>
        /// <typeparam name="TResult9">The type of the ninth accumulator value.</typeparam>
        /// <typeparam name="TResult10">The type of the tenth accumulator value.</typeparam>
        /// <typeparam name="TResult11">The type of the eleventh accumulator value.</typeparam>
        /// <typeparam name="TResult12">The type of the twelfth accumulator value.</typeparam>
        /// <typeparam name="TResult13">The type of the thirteenth accumulator value.</typeparam>
        /// <typeparam name="TResult14">The type of the fourteenth accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="source">The sequence to aggregate over.</param>
        /// <param name="aggregatorConnector1">
        /// The first function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector2">
        /// The second function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector3">
        /// The third function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector4">
        /// The fourth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector5">
        /// The fifth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector6">
        /// The sixth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector7">
        /// The seventh function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector8">
        /// The eighth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector9">
        /// The ninth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector10">
        /// The tenth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector11">
        /// The eleventh function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector12">
        /// The twelfth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector13">
        /// The thirteenth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector14">
        /// The fourteenth function that connects an accumulator to the source.</param>
        /// <param name="resultSelector">
        /// A function to transform the final accumulator value into the result value.</param>
        /// <returns>The transformed final accumulator value.</returns>
        /// <remarks>This method uses immediate execution semantics.</remarks>

        public static TResult Aggregate<TSource, TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9, TResult10, TResult11, TResult12, TResult13, TResult14, TResult>(
            this IEnumerable<TSource> source,
            Func<IObservable<TSource>, Func<TResult1>> aggregatorConnector1,
            Func<IObservable<TSource>, Func<TResult2>> aggregatorConnector2,
            Func<IObservable<TSource>, Func<TResult3>> aggregatorConnector3,
            Func<IObservable<TSource>, Func<TResult4>> aggregatorConnector4,
            Func<IObservable<TSource>, Func<TResult5>> aggregatorConnector5,
            Func<IObservable<TSource>, Func<TResult6>> aggregatorConnector6,
            Func<IObservable<TSource>, Func<TResult7>> aggregatorConnector7,
            Func<IObservable<TSource>, Func<TResult8>> aggregatorConnector8,
            Func<IObservable<TSource>, Func<TResult9>> aggregatorConnector9,
            Func<IObservable<TSource>, Func<TResult10>> aggregatorConnector10,
            Func<IObservable<TSource>, Func<TResult11>> aggregatorConnector11,
            Func<IObservable<TSource>, Func<TResult12>> aggregatorConnector12,
            Func<IObservable<TSource>, Func<TResult13>> aggregatorConnector13,
            Func<IObservable<TSource>, Func<TResult14>> aggregatorConnector14,
            Func<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9, TResult10, TResult11, TResult12, TResult13, TResult14, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (aggregatorConnector1 == null) throw new ArgumentNullException(nameof(aggregatorConnector1));
            if (aggregatorConnector2 == null) throw new ArgumentNullException(nameof(aggregatorConnector2));
            if (aggregatorConnector3 == null) throw new ArgumentNullException(nameof(aggregatorConnector3));
            if (aggregatorConnector4 == null) throw new ArgumentNullException(nameof(aggregatorConnector4));
            if (aggregatorConnector5 == null) throw new ArgumentNullException(nameof(aggregatorConnector5));
            if (aggregatorConnector6 == null) throw new ArgumentNullException(nameof(aggregatorConnector6));
            if (aggregatorConnector7 == null) throw new ArgumentNullException(nameof(aggregatorConnector7));
            if (aggregatorConnector8 == null) throw new ArgumentNullException(nameof(aggregatorConnector8));
            if (aggregatorConnector9 == null) throw new ArgumentNullException(nameof(aggregatorConnector9));
            if (aggregatorConnector10 == null) throw new ArgumentNullException(nameof(aggregatorConnector10));
            if (aggregatorConnector11 == null) throw new ArgumentNullException(nameof(aggregatorConnector11));
            if (aggregatorConnector12 == null) throw new ArgumentNullException(nameof(aggregatorConnector12));
            if (aggregatorConnector13 == null) throw new ArgumentNullException(nameof(aggregatorConnector13));
            if (aggregatorConnector14 == null) throw new ArgumentNullException(nameof(aggregatorConnector14));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            var s1 = new Subject<TSource>(); var c1 = aggregatorConnector1(s1);
            var s2 = new Subject<TSource>(); var c2 = aggregatorConnector2(s2);
            var s3 = new Subject<TSource>(); var c3 = aggregatorConnector3(s3);
            var s4 = new Subject<TSource>(); var c4 = aggregatorConnector4(s4);
            var s5 = new Subject<TSource>(); var c5 = aggregatorConnector5(s5);
            var s6 = new Subject<TSource>(); var c6 = aggregatorConnector6(s6);
            var s7 = new Subject<TSource>(); var c7 = aggregatorConnector7(s7);
            var s8 = new Subject<TSource>(); var c8 = aggregatorConnector8(s8);
            var s9 = new Subject<TSource>(); var c9 = aggregatorConnector9(s9);
            var s10 = new Subject<TSource>(); var c10 = aggregatorConnector10(s10);
            var s11 = new Subject<TSource>(); var c11 = aggregatorConnector11(s11);
            var s12 = new Subject<TSource>(); var c12 = aggregatorConnector12(s12);
            var s13 = new Subject<TSource>(); var c13 = aggregatorConnector13(s13);
            var s14 = new Subject<TSource>(); var c14 = aggregatorConnector14(s14);

            // TODO OnError

            foreach (var item in source)
            {
                s1.OnNext(item);
                s2.OnNext(item);
                s3.OnNext(item);
                s4.OnNext(item);
                s5.OnNext(item);
                s6.OnNext(item);
                s7.OnNext(item);
                s8.OnNext(item);
                s9.OnNext(item);
                s10.OnNext(item);
                s11.OnNext(item);
                s12.OnNext(item);
                s13.OnNext(item);
                s14.OnNext(item);
            }

            s1.OnCompleted();
            s2.OnCompleted();
            s3.OnCompleted();
            s4.OnCompleted();
            s5.OnCompleted();
            s6.OnCompleted();
            s7.OnCompleted();
            s8.OnCompleted();
            s9.OnCompleted();
            s10.OnCompleted();
            s11.OnCompleted();
            s12.OnCompleted();
            s13.OnCompleted();
            s14.OnCompleted();

            return resultSelector(c1(), c2(), c3(), c4(), c5(), c6(), c7(), c8(), c9(), c10(), c11(), c12(), c13(), c14());
        }

        /// <summary>
        /// Applies 15 accumulators over a sequence.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TResult1">The type of the first accumulator value.</typeparam>
        /// <typeparam name="TResult2">The type of the second accumulator value.</typeparam>
        /// <typeparam name="TResult3">The type of the third accumulator value.</typeparam>
        /// <typeparam name="TResult4">The type of the fourth accumulator value.</typeparam>
        /// <typeparam name="TResult5">The type of the fifth accumulator value.</typeparam>
        /// <typeparam name="TResult6">The type of the sixth accumulator value.</typeparam>
        /// <typeparam name="TResult7">The type of the seventh accumulator value.</typeparam>
        /// <typeparam name="TResult8">The type of the eighth accumulator value.</typeparam>
        /// <typeparam name="TResult9">The type of the ninth accumulator value.</typeparam>
        /// <typeparam name="TResult10">The type of the tenth accumulator value.</typeparam>
        /// <typeparam name="TResult11">The type of the eleventh accumulator value.</typeparam>
        /// <typeparam name="TResult12">The type of the twelfth accumulator value.</typeparam>
        /// <typeparam name="TResult13">The type of the thirteenth accumulator value.</typeparam>
        /// <typeparam name="TResult14">The type of the fourteenth accumulator value.</typeparam>
        /// <typeparam name="TResult15">The type of the fifteenth accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="source">The sequence to aggregate over.</param>
        /// <param name="aggregatorConnector1">
        /// The first function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector2">
        /// The second function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector3">
        /// The third function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector4">
        /// The fourth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector5">
        /// The fifth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector6">
        /// The sixth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector7">
        /// The seventh function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector8">
        /// The eighth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector9">
        /// The ninth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector10">
        /// The tenth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector11">
        /// The eleventh function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector12">
        /// The twelfth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector13">
        /// The thirteenth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector14">
        /// The fourteenth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorConnector15">
        /// The fifteenth function that connects an accumulator to the source.</param>
        /// <param name="resultSelector">
        /// A function to transform the final accumulator value into the result value.</param>
        /// <returns>The transformed final accumulator value.</returns>
        /// <remarks>This method uses immediate execution semantics.</remarks>

        public static TResult Aggregate<TSource, TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9, TResult10, TResult11, TResult12, TResult13, TResult14, TResult15, TResult>(
            this IEnumerable<TSource> source,
            Func<IObservable<TSource>, Func<TResult1>> aggregatorConnector1,
            Func<IObservable<TSource>, Func<TResult2>> aggregatorConnector2,
            Func<IObservable<TSource>, Func<TResult3>> aggregatorConnector3,
            Func<IObservable<TSource>, Func<TResult4>> aggregatorConnector4,
            Func<IObservable<TSource>, Func<TResult5>> aggregatorConnector5,
            Func<IObservable<TSource>, Func<TResult6>> aggregatorConnector6,
            Func<IObservable<TSource>, Func<TResult7>> aggregatorConnector7,
            Func<IObservable<TSource>, Func<TResult8>> aggregatorConnector8,
            Func<IObservable<TSource>, Func<TResult9>> aggregatorConnector9,
            Func<IObservable<TSource>, Func<TResult10>> aggregatorConnector10,
            Func<IObservable<TSource>, Func<TResult11>> aggregatorConnector11,
            Func<IObservable<TSource>, Func<TResult12>> aggregatorConnector12,
            Func<IObservable<TSource>, Func<TResult13>> aggregatorConnector13,
            Func<IObservable<TSource>, Func<TResult14>> aggregatorConnector14,
            Func<IObservable<TSource>, Func<TResult15>> aggregatorConnector15,
            Func<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9, TResult10, TResult11, TResult12, TResult13, TResult14, TResult15, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (aggregatorConnector1 == null) throw new ArgumentNullException(nameof(aggregatorConnector1));
            if (aggregatorConnector2 == null) throw new ArgumentNullException(nameof(aggregatorConnector2));
            if (aggregatorConnector3 == null) throw new ArgumentNullException(nameof(aggregatorConnector3));
            if (aggregatorConnector4 == null) throw new ArgumentNullException(nameof(aggregatorConnector4));
            if (aggregatorConnector5 == null) throw new ArgumentNullException(nameof(aggregatorConnector5));
            if (aggregatorConnector6 == null) throw new ArgumentNullException(nameof(aggregatorConnector6));
            if (aggregatorConnector7 == null) throw new ArgumentNullException(nameof(aggregatorConnector7));
            if (aggregatorConnector8 == null) throw new ArgumentNullException(nameof(aggregatorConnector8));
            if (aggregatorConnector9 == null) throw new ArgumentNullException(nameof(aggregatorConnector9));
            if (aggregatorConnector10 == null) throw new ArgumentNullException(nameof(aggregatorConnector10));
            if (aggregatorConnector11 == null) throw new ArgumentNullException(nameof(aggregatorConnector11));
            if (aggregatorConnector12 == null) throw new ArgumentNullException(nameof(aggregatorConnector12));
            if (aggregatorConnector13 == null) throw new ArgumentNullException(nameof(aggregatorConnector13));
            if (aggregatorConnector14 == null) throw new ArgumentNullException(nameof(aggregatorConnector14));
            if (aggregatorConnector15 == null) throw new ArgumentNullException(nameof(aggregatorConnector15));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            var s1 = new Subject<TSource>(); var c1 = aggregatorConnector1(s1);
            var s2 = new Subject<TSource>(); var c2 = aggregatorConnector2(s2);
            var s3 = new Subject<TSource>(); var c3 = aggregatorConnector3(s3);
            var s4 = new Subject<TSource>(); var c4 = aggregatorConnector4(s4);
            var s5 = new Subject<TSource>(); var c5 = aggregatorConnector5(s5);
            var s6 = new Subject<TSource>(); var c6 = aggregatorConnector6(s6);
            var s7 = new Subject<TSource>(); var c7 = aggregatorConnector7(s7);
            var s8 = new Subject<TSource>(); var c8 = aggregatorConnector8(s8);
            var s9 = new Subject<TSource>(); var c9 = aggregatorConnector9(s9);
            var s10 = new Subject<TSource>(); var c10 = aggregatorConnector10(s10);
            var s11 = new Subject<TSource>(); var c11 = aggregatorConnector11(s11);
            var s12 = new Subject<TSource>(); var c12 = aggregatorConnector12(s12);
            var s13 = new Subject<TSource>(); var c13 = aggregatorConnector13(s13);
            var s14 = new Subject<TSource>(); var c14 = aggregatorConnector14(s14);
            var s15 = new Subject<TSource>(); var c15 = aggregatorConnector15(s15);

            // TODO OnError

            foreach (var item in source)
            {
                s1.OnNext(item);
                s2.OnNext(item);
                s3.OnNext(item);
                s4.OnNext(item);
                s5.OnNext(item);
                s6.OnNext(item);
                s7.OnNext(item);
                s8.OnNext(item);
                s9.OnNext(item);
                s10.OnNext(item);
                s11.OnNext(item);
                s12.OnNext(item);
                s13.OnNext(item);
                s14.OnNext(item);
                s15.OnNext(item);
            }

            s1.OnCompleted();
            s2.OnCompleted();
            s3.OnCompleted();
            s4.OnCompleted();
            s5.OnCompleted();
            s6.OnCompleted();
            s7.OnCompleted();
            s8.OnCompleted();
            s9.OnCompleted();
            s10.OnCompleted();
            s11.OnCompleted();
            s12.OnCompleted();
            s13.OnCompleted();
            s14.OnCompleted();
            s15.OnCompleted();

            return resultSelector(c1(), c2(), c3(), c4(), c5(), c6(), c7(), c8(), c9(), c10(), c11(), c12(), c13(), c14(), c15());
        }

    }
}

#endif // !NO_OBSERVABLES
