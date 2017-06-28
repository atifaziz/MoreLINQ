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
        /// <param name="aggregatorSelector1">
        /// The first function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector2">
        /// The second function that connects an accumulator to the source.</param>
        /// <param name="resultSelector">
        /// A function to transform the final accumulator value into the result value.</param>
        /// <returns>The transformed final accumulator value.</returns>
        /// <remarks>This method uses immediate execution semantics.</remarks>

        public static TResult Aggregate<TSource, TResult1, TResult2, TResult>(
            this IEnumerable<TSource> source,
            Func<IObservable<TSource>, IObservable<TResult1>> aggregatorSelector1,
            Func<IObservable<TSource>, IObservable<TResult2>> aggregatorSelector2,
            Func<TResult1, TResult2, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (aggregatorSelector1 == null) throw new ArgumentNullException(nameof(aggregatorSelector1));
            if (aggregatorSelector2 == null) throw new ArgumentNullException(nameof(aggregatorSelector2));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            var r1 = new TResult1[1]; var s1 = new Subject<TSource>();
            var r2 = new TResult2[1]; var s2 = new Subject<TSource>();

            // TODO OnError

            using (aggregatorSelector1(s1).Subscribe(Observer.Create((TResult1 r) => r1[0] = r)))
            using (aggregatorSelector2(s2).Subscribe(Observer.Create((TResult2 r) => r2[0] = r)))
            {
                foreach (var item in source)
                {
                    s1.OnNext(item);
                    s2.OnNext(item);
                }

                s1.OnCompleted();
                s2.OnCompleted();
            }

            return resultSelector(r1[0], r2[0]);
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
        /// <param name="aggregatorSelector1">
        /// The first function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector2">
        /// The second function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector3">
        /// The third function that connects an accumulator to the source.</param>
        /// <param name="resultSelector">
        /// A function to transform the final accumulator value into the result value.</param>
        /// <returns>The transformed final accumulator value.</returns>
        /// <remarks>This method uses immediate execution semantics.</remarks>

        public static TResult Aggregate<TSource, TResult1, TResult2, TResult3, TResult>(
            this IEnumerable<TSource> source,
            Func<IObservable<TSource>, IObservable<TResult1>> aggregatorSelector1,
            Func<IObservable<TSource>, IObservable<TResult2>> aggregatorSelector2,
            Func<IObservable<TSource>, IObservable<TResult3>> aggregatorSelector3,
            Func<TResult1, TResult2, TResult3, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (aggregatorSelector1 == null) throw new ArgumentNullException(nameof(aggregatorSelector1));
            if (aggregatorSelector2 == null) throw new ArgumentNullException(nameof(aggregatorSelector2));
            if (aggregatorSelector3 == null) throw new ArgumentNullException(nameof(aggregatorSelector3));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            var r1 = new TResult1[1]; var s1 = new Subject<TSource>();
            var r2 = new TResult2[1]; var s2 = new Subject<TSource>();
            var r3 = new TResult3[1]; var s3 = new Subject<TSource>();

            // TODO OnError

            using (aggregatorSelector1(s1).Subscribe(Observer.Create((TResult1 r) => r1[0] = r)))
            using (aggregatorSelector2(s2).Subscribe(Observer.Create((TResult2 r) => r2[0] = r)))
            using (aggregatorSelector3(s3).Subscribe(Observer.Create((TResult3 r) => r3[0] = r)))
            {
                foreach (var item in source)
                {
                    s1.OnNext(item);
                    s2.OnNext(item);
                    s3.OnNext(item);
                }

                s1.OnCompleted();
                s2.OnCompleted();
                s3.OnCompleted();
            }

            return resultSelector(r1[0], r2[0], r3[0]);
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
        /// <param name="aggregatorSelector1">
        /// The first function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector2">
        /// The second function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector3">
        /// The third function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector4">
        /// The fourth function that connects an accumulator to the source.</param>
        /// <param name="resultSelector">
        /// A function to transform the final accumulator value into the result value.</param>
        /// <returns>The transformed final accumulator value.</returns>
        /// <remarks>This method uses immediate execution semantics.</remarks>

        public static TResult Aggregate<TSource, TResult1, TResult2, TResult3, TResult4, TResult>(
            this IEnumerable<TSource> source,
            Func<IObservable<TSource>, IObservable<TResult1>> aggregatorSelector1,
            Func<IObservable<TSource>, IObservable<TResult2>> aggregatorSelector2,
            Func<IObservable<TSource>, IObservable<TResult3>> aggregatorSelector3,
            Func<IObservable<TSource>, IObservable<TResult4>> aggregatorSelector4,
            Func<TResult1, TResult2, TResult3, TResult4, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (aggregatorSelector1 == null) throw new ArgumentNullException(nameof(aggregatorSelector1));
            if (aggregatorSelector2 == null) throw new ArgumentNullException(nameof(aggregatorSelector2));
            if (aggregatorSelector3 == null) throw new ArgumentNullException(nameof(aggregatorSelector3));
            if (aggregatorSelector4 == null) throw new ArgumentNullException(nameof(aggregatorSelector4));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            var r1 = new TResult1[1]; var s1 = new Subject<TSource>();
            var r2 = new TResult2[1]; var s2 = new Subject<TSource>();
            var r3 = new TResult3[1]; var s3 = new Subject<TSource>();
            var r4 = new TResult4[1]; var s4 = new Subject<TSource>();

            // TODO OnError

            using (aggregatorSelector1(s1).Subscribe(Observer.Create((TResult1 r) => r1[0] = r)))
            using (aggregatorSelector2(s2).Subscribe(Observer.Create((TResult2 r) => r2[0] = r)))
            using (aggregatorSelector3(s3).Subscribe(Observer.Create((TResult3 r) => r3[0] = r)))
            using (aggregatorSelector4(s4).Subscribe(Observer.Create((TResult4 r) => r4[0] = r)))
            {
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
            }

            return resultSelector(r1[0], r2[0], r3[0], r4[0]);
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
        /// <param name="aggregatorSelector1">
        /// The first function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector2">
        /// The second function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector3">
        /// The third function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector4">
        /// The fourth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector5">
        /// The fifth function that connects an accumulator to the source.</param>
        /// <param name="resultSelector">
        /// A function to transform the final accumulator value into the result value.</param>
        /// <returns>The transformed final accumulator value.</returns>
        /// <remarks>This method uses immediate execution semantics.</remarks>

        public static TResult Aggregate<TSource, TResult1, TResult2, TResult3, TResult4, TResult5, TResult>(
            this IEnumerable<TSource> source,
            Func<IObservable<TSource>, IObservable<TResult1>> aggregatorSelector1,
            Func<IObservable<TSource>, IObservable<TResult2>> aggregatorSelector2,
            Func<IObservable<TSource>, IObservable<TResult3>> aggregatorSelector3,
            Func<IObservable<TSource>, IObservable<TResult4>> aggregatorSelector4,
            Func<IObservable<TSource>, IObservable<TResult5>> aggregatorSelector5,
            Func<TResult1, TResult2, TResult3, TResult4, TResult5, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (aggregatorSelector1 == null) throw new ArgumentNullException(nameof(aggregatorSelector1));
            if (aggregatorSelector2 == null) throw new ArgumentNullException(nameof(aggregatorSelector2));
            if (aggregatorSelector3 == null) throw new ArgumentNullException(nameof(aggregatorSelector3));
            if (aggregatorSelector4 == null) throw new ArgumentNullException(nameof(aggregatorSelector4));
            if (aggregatorSelector5 == null) throw new ArgumentNullException(nameof(aggregatorSelector5));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            var r1 = new TResult1[1]; var s1 = new Subject<TSource>();
            var r2 = new TResult2[1]; var s2 = new Subject<TSource>();
            var r3 = new TResult3[1]; var s3 = new Subject<TSource>();
            var r4 = new TResult4[1]; var s4 = new Subject<TSource>();
            var r5 = new TResult5[1]; var s5 = new Subject<TSource>();

            // TODO OnError

            using (aggregatorSelector1(s1).Subscribe(Observer.Create((TResult1 r) => r1[0] = r)))
            using (aggregatorSelector2(s2).Subscribe(Observer.Create((TResult2 r) => r2[0] = r)))
            using (aggregatorSelector3(s3).Subscribe(Observer.Create((TResult3 r) => r3[0] = r)))
            using (aggregatorSelector4(s4).Subscribe(Observer.Create((TResult4 r) => r4[0] = r)))
            using (aggregatorSelector5(s5).Subscribe(Observer.Create((TResult5 r) => r5[0] = r)))
            {
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
            }

            return resultSelector(r1[0], r2[0], r3[0], r4[0], r5[0]);
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
        /// <param name="aggregatorSelector1">
        /// The first function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector2">
        /// The second function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector3">
        /// The third function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector4">
        /// The fourth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector5">
        /// The fifth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector6">
        /// The sixth function that connects an accumulator to the source.</param>
        /// <param name="resultSelector">
        /// A function to transform the final accumulator value into the result value.</param>
        /// <returns>The transformed final accumulator value.</returns>
        /// <remarks>This method uses immediate execution semantics.</remarks>

        public static TResult Aggregate<TSource, TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult>(
            this IEnumerable<TSource> source,
            Func<IObservable<TSource>, IObservable<TResult1>> aggregatorSelector1,
            Func<IObservable<TSource>, IObservable<TResult2>> aggregatorSelector2,
            Func<IObservable<TSource>, IObservable<TResult3>> aggregatorSelector3,
            Func<IObservable<TSource>, IObservable<TResult4>> aggregatorSelector4,
            Func<IObservable<TSource>, IObservable<TResult5>> aggregatorSelector5,
            Func<IObservable<TSource>, IObservable<TResult6>> aggregatorSelector6,
            Func<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (aggregatorSelector1 == null) throw new ArgumentNullException(nameof(aggregatorSelector1));
            if (aggregatorSelector2 == null) throw new ArgumentNullException(nameof(aggregatorSelector2));
            if (aggregatorSelector3 == null) throw new ArgumentNullException(nameof(aggregatorSelector3));
            if (aggregatorSelector4 == null) throw new ArgumentNullException(nameof(aggregatorSelector4));
            if (aggregatorSelector5 == null) throw new ArgumentNullException(nameof(aggregatorSelector5));
            if (aggregatorSelector6 == null) throw new ArgumentNullException(nameof(aggregatorSelector6));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            var r1 = new TResult1[1]; var s1 = new Subject<TSource>();
            var r2 = new TResult2[1]; var s2 = new Subject<TSource>();
            var r3 = new TResult3[1]; var s3 = new Subject<TSource>();
            var r4 = new TResult4[1]; var s4 = new Subject<TSource>();
            var r5 = new TResult5[1]; var s5 = new Subject<TSource>();
            var r6 = new TResult6[1]; var s6 = new Subject<TSource>();

            // TODO OnError

            using (aggregatorSelector1(s1).Subscribe(Observer.Create((TResult1 r) => r1[0] = r)))
            using (aggregatorSelector2(s2).Subscribe(Observer.Create((TResult2 r) => r2[0] = r)))
            using (aggregatorSelector3(s3).Subscribe(Observer.Create((TResult3 r) => r3[0] = r)))
            using (aggregatorSelector4(s4).Subscribe(Observer.Create((TResult4 r) => r4[0] = r)))
            using (aggregatorSelector5(s5).Subscribe(Observer.Create((TResult5 r) => r5[0] = r)))
            using (aggregatorSelector6(s6).Subscribe(Observer.Create((TResult6 r) => r6[0] = r)))
            {
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
            }

            return resultSelector(r1[0], r2[0], r3[0], r4[0], r5[0], r6[0]);
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
        /// <param name="aggregatorSelector1">
        /// The first function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector2">
        /// The second function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector3">
        /// The third function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector4">
        /// The fourth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector5">
        /// The fifth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector6">
        /// The sixth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector7">
        /// The seventh function that connects an accumulator to the source.</param>
        /// <param name="resultSelector">
        /// A function to transform the final accumulator value into the result value.</param>
        /// <returns>The transformed final accumulator value.</returns>
        /// <remarks>This method uses immediate execution semantics.</remarks>

        public static TResult Aggregate<TSource, TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult>(
            this IEnumerable<TSource> source,
            Func<IObservable<TSource>, IObservable<TResult1>> aggregatorSelector1,
            Func<IObservable<TSource>, IObservable<TResult2>> aggregatorSelector2,
            Func<IObservable<TSource>, IObservable<TResult3>> aggregatorSelector3,
            Func<IObservable<TSource>, IObservable<TResult4>> aggregatorSelector4,
            Func<IObservable<TSource>, IObservable<TResult5>> aggregatorSelector5,
            Func<IObservable<TSource>, IObservable<TResult6>> aggregatorSelector6,
            Func<IObservable<TSource>, IObservable<TResult7>> aggregatorSelector7,
            Func<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (aggregatorSelector1 == null) throw new ArgumentNullException(nameof(aggregatorSelector1));
            if (aggregatorSelector2 == null) throw new ArgumentNullException(nameof(aggregatorSelector2));
            if (aggregatorSelector3 == null) throw new ArgumentNullException(nameof(aggregatorSelector3));
            if (aggregatorSelector4 == null) throw new ArgumentNullException(nameof(aggregatorSelector4));
            if (aggregatorSelector5 == null) throw new ArgumentNullException(nameof(aggregatorSelector5));
            if (aggregatorSelector6 == null) throw new ArgumentNullException(nameof(aggregatorSelector6));
            if (aggregatorSelector7 == null) throw new ArgumentNullException(nameof(aggregatorSelector7));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            var r1 = new TResult1[1]; var s1 = new Subject<TSource>();
            var r2 = new TResult2[1]; var s2 = new Subject<TSource>();
            var r3 = new TResult3[1]; var s3 = new Subject<TSource>();
            var r4 = new TResult4[1]; var s4 = new Subject<TSource>();
            var r5 = new TResult5[1]; var s5 = new Subject<TSource>();
            var r6 = new TResult6[1]; var s6 = new Subject<TSource>();
            var r7 = new TResult7[1]; var s7 = new Subject<TSource>();

            // TODO OnError

            using (aggregatorSelector1(s1).Subscribe(Observer.Create((TResult1 r) => r1[0] = r)))
            using (aggregatorSelector2(s2).Subscribe(Observer.Create((TResult2 r) => r2[0] = r)))
            using (aggregatorSelector3(s3).Subscribe(Observer.Create((TResult3 r) => r3[0] = r)))
            using (aggregatorSelector4(s4).Subscribe(Observer.Create((TResult4 r) => r4[0] = r)))
            using (aggregatorSelector5(s5).Subscribe(Observer.Create((TResult5 r) => r5[0] = r)))
            using (aggregatorSelector6(s6).Subscribe(Observer.Create((TResult6 r) => r6[0] = r)))
            using (aggregatorSelector7(s7).Subscribe(Observer.Create((TResult7 r) => r7[0] = r)))
            {
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
            }

            return resultSelector(r1[0], r2[0], r3[0], r4[0], r5[0], r6[0], r7[0]);
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
        /// <param name="aggregatorSelector1">
        /// The first function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector2">
        /// The second function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector3">
        /// The third function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector4">
        /// The fourth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector5">
        /// The fifth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector6">
        /// The sixth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector7">
        /// The seventh function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector8">
        /// The eighth function that connects an accumulator to the source.</param>
        /// <param name="resultSelector">
        /// A function to transform the final accumulator value into the result value.</param>
        /// <returns>The transformed final accumulator value.</returns>
        /// <remarks>This method uses immediate execution semantics.</remarks>

        public static TResult Aggregate<TSource, TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult>(
            this IEnumerable<TSource> source,
            Func<IObservable<TSource>, IObservable<TResult1>> aggregatorSelector1,
            Func<IObservable<TSource>, IObservable<TResult2>> aggregatorSelector2,
            Func<IObservable<TSource>, IObservable<TResult3>> aggregatorSelector3,
            Func<IObservable<TSource>, IObservable<TResult4>> aggregatorSelector4,
            Func<IObservable<TSource>, IObservable<TResult5>> aggregatorSelector5,
            Func<IObservable<TSource>, IObservable<TResult6>> aggregatorSelector6,
            Func<IObservable<TSource>, IObservable<TResult7>> aggregatorSelector7,
            Func<IObservable<TSource>, IObservable<TResult8>> aggregatorSelector8,
            Func<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (aggregatorSelector1 == null) throw new ArgumentNullException(nameof(aggregatorSelector1));
            if (aggregatorSelector2 == null) throw new ArgumentNullException(nameof(aggregatorSelector2));
            if (aggregatorSelector3 == null) throw new ArgumentNullException(nameof(aggregatorSelector3));
            if (aggregatorSelector4 == null) throw new ArgumentNullException(nameof(aggregatorSelector4));
            if (aggregatorSelector5 == null) throw new ArgumentNullException(nameof(aggregatorSelector5));
            if (aggregatorSelector6 == null) throw new ArgumentNullException(nameof(aggregatorSelector6));
            if (aggregatorSelector7 == null) throw new ArgumentNullException(nameof(aggregatorSelector7));
            if (aggregatorSelector8 == null) throw new ArgumentNullException(nameof(aggregatorSelector8));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            var r1 = new TResult1[1]; var s1 = new Subject<TSource>();
            var r2 = new TResult2[1]; var s2 = new Subject<TSource>();
            var r3 = new TResult3[1]; var s3 = new Subject<TSource>();
            var r4 = new TResult4[1]; var s4 = new Subject<TSource>();
            var r5 = new TResult5[1]; var s5 = new Subject<TSource>();
            var r6 = new TResult6[1]; var s6 = new Subject<TSource>();
            var r7 = new TResult7[1]; var s7 = new Subject<TSource>();
            var r8 = new TResult8[1]; var s8 = new Subject<TSource>();

            // TODO OnError

            using (aggregatorSelector1(s1).Subscribe(Observer.Create((TResult1 r) => r1[0] = r)))
            using (aggregatorSelector2(s2).Subscribe(Observer.Create((TResult2 r) => r2[0] = r)))
            using (aggregatorSelector3(s3).Subscribe(Observer.Create((TResult3 r) => r3[0] = r)))
            using (aggregatorSelector4(s4).Subscribe(Observer.Create((TResult4 r) => r4[0] = r)))
            using (aggregatorSelector5(s5).Subscribe(Observer.Create((TResult5 r) => r5[0] = r)))
            using (aggregatorSelector6(s6).Subscribe(Observer.Create((TResult6 r) => r6[0] = r)))
            using (aggregatorSelector7(s7).Subscribe(Observer.Create((TResult7 r) => r7[0] = r)))
            using (aggregatorSelector8(s8).Subscribe(Observer.Create((TResult8 r) => r8[0] = r)))
            {
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
            }

            return resultSelector(r1[0], r2[0], r3[0], r4[0], r5[0], r6[0], r7[0], r8[0]);
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
        /// <param name="aggregatorSelector1">
        /// The first function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector2">
        /// The second function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector3">
        /// The third function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector4">
        /// The fourth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector5">
        /// The fifth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector6">
        /// The sixth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector7">
        /// The seventh function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector8">
        /// The eighth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector9">
        /// The ninth function that connects an accumulator to the source.</param>
        /// <param name="resultSelector">
        /// A function to transform the final accumulator value into the result value.</param>
        /// <returns>The transformed final accumulator value.</returns>
        /// <remarks>This method uses immediate execution semantics.</remarks>

        public static TResult Aggregate<TSource, TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9, TResult>(
            this IEnumerable<TSource> source,
            Func<IObservable<TSource>, IObservable<TResult1>> aggregatorSelector1,
            Func<IObservable<TSource>, IObservable<TResult2>> aggregatorSelector2,
            Func<IObservable<TSource>, IObservable<TResult3>> aggregatorSelector3,
            Func<IObservable<TSource>, IObservable<TResult4>> aggregatorSelector4,
            Func<IObservable<TSource>, IObservable<TResult5>> aggregatorSelector5,
            Func<IObservable<TSource>, IObservable<TResult6>> aggregatorSelector6,
            Func<IObservable<TSource>, IObservable<TResult7>> aggregatorSelector7,
            Func<IObservable<TSource>, IObservable<TResult8>> aggregatorSelector8,
            Func<IObservable<TSource>, IObservable<TResult9>> aggregatorSelector9,
            Func<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (aggregatorSelector1 == null) throw new ArgumentNullException(nameof(aggregatorSelector1));
            if (aggregatorSelector2 == null) throw new ArgumentNullException(nameof(aggregatorSelector2));
            if (aggregatorSelector3 == null) throw new ArgumentNullException(nameof(aggregatorSelector3));
            if (aggregatorSelector4 == null) throw new ArgumentNullException(nameof(aggregatorSelector4));
            if (aggregatorSelector5 == null) throw new ArgumentNullException(nameof(aggregatorSelector5));
            if (aggregatorSelector6 == null) throw new ArgumentNullException(nameof(aggregatorSelector6));
            if (aggregatorSelector7 == null) throw new ArgumentNullException(nameof(aggregatorSelector7));
            if (aggregatorSelector8 == null) throw new ArgumentNullException(nameof(aggregatorSelector8));
            if (aggregatorSelector9 == null) throw new ArgumentNullException(nameof(aggregatorSelector9));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            var r1 = new TResult1[1]; var s1 = new Subject<TSource>();
            var r2 = new TResult2[1]; var s2 = new Subject<TSource>();
            var r3 = new TResult3[1]; var s3 = new Subject<TSource>();
            var r4 = new TResult4[1]; var s4 = new Subject<TSource>();
            var r5 = new TResult5[1]; var s5 = new Subject<TSource>();
            var r6 = new TResult6[1]; var s6 = new Subject<TSource>();
            var r7 = new TResult7[1]; var s7 = new Subject<TSource>();
            var r8 = new TResult8[1]; var s8 = new Subject<TSource>();
            var r9 = new TResult9[1]; var s9 = new Subject<TSource>();

            // TODO OnError

            using (aggregatorSelector1(s1).Subscribe(Observer.Create((TResult1 r) => r1[0] = r)))
            using (aggregatorSelector2(s2).Subscribe(Observer.Create((TResult2 r) => r2[0] = r)))
            using (aggregatorSelector3(s3).Subscribe(Observer.Create((TResult3 r) => r3[0] = r)))
            using (aggregatorSelector4(s4).Subscribe(Observer.Create((TResult4 r) => r4[0] = r)))
            using (aggregatorSelector5(s5).Subscribe(Observer.Create((TResult5 r) => r5[0] = r)))
            using (aggregatorSelector6(s6).Subscribe(Observer.Create((TResult6 r) => r6[0] = r)))
            using (aggregatorSelector7(s7).Subscribe(Observer.Create((TResult7 r) => r7[0] = r)))
            using (aggregatorSelector8(s8).Subscribe(Observer.Create((TResult8 r) => r8[0] = r)))
            using (aggregatorSelector9(s9).Subscribe(Observer.Create((TResult9 r) => r9[0] = r)))
            {
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
            }

            return resultSelector(r1[0], r2[0], r3[0], r4[0], r5[0], r6[0], r7[0], r8[0], r9[0]);
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
        /// <param name="aggregatorSelector1">
        /// The first function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector2">
        /// The second function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector3">
        /// The third function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector4">
        /// The fourth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector5">
        /// The fifth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector6">
        /// The sixth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector7">
        /// The seventh function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector8">
        /// The eighth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector9">
        /// The ninth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector10">
        /// The tenth function that connects an accumulator to the source.</param>
        /// <param name="resultSelector">
        /// A function to transform the final accumulator value into the result value.</param>
        /// <returns>The transformed final accumulator value.</returns>
        /// <remarks>This method uses immediate execution semantics.</remarks>

        public static TResult Aggregate<TSource, TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9, TResult10, TResult>(
            this IEnumerable<TSource> source,
            Func<IObservable<TSource>, IObservable<TResult1>> aggregatorSelector1,
            Func<IObservable<TSource>, IObservable<TResult2>> aggregatorSelector2,
            Func<IObservable<TSource>, IObservable<TResult3>> aggregatorSelector3,
            Func<IObservable<TSource>, IObservable<TResult4>> aggregatorSelector4,
            Func<IObservable<TSource>, IObservable<TResult5>> aggregatorSelector5,
            Func<IObservable<TSource>, IObservable<TResult6>> aggregatorSelector6,
            Func<IObservable<TSource>, IObservable<TResult7>> aggregatorSelector7,
            Func<IObservable<TSource>, IObservable<TResult8>> aggregatorSelector8,
            Func<IObservable<TSource>, IObservable<TResult9>> aggregatorSelector9,
            Func<IObservable<TSource>, IObservable<TResult10>> aggregatorSelector10,
            Func<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9, TResult10, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (aggregatorSelector1 == null) throw new ArgumentNullException(nameof(aggregatorSelector1));
            if (aggregatorSelector2 == null) throw new ArgumentNullException(nameof(aggregatorSelector2));
            if (aggregatorSelector3 == null) throw new ArgumentNullException(nameof(aggregatorSelector3));
            if (aggregatorSelector4 == null) throw new ArgumentNullException(nameof(aggregatorSelector4));
            if (aggregatorSelector5 == null) throw new ArgumentNullException(nameof(aggregatorSelector5));
            if (aggregatorSelector6 == null) throw new ArgumentNullException(nameof(aggregatorSelector6));
            if (aggregatorSelector7 == null) throw new ArgumentNullException(nameof(aggregatorSelector7));
            if (aggregatorSelector8 == null) throw new ArgumentNullException(nameof(aggregatorSelector8));
            if (aggregatorSelector9 == null) throw new ArgumentNullException(nameof(aggregatorSelector9));
            if (aggregatorSelector10 == null) throw new ArgumentNullException(nameof(aggregatorSelector10));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            var r1 = new TResult1[1]; var s1 = new Subject<TSource>();
            var r2 = new TResult2[1]; var s2 = new Subject<TSource>();
            var r3 = new TResult3[1]; var s3 = new Subject<TSource>();
            var r4 = new TResult4[1]; var s4 = new Subject<TSource>();
            var r5 = new TResult5[1]; var s5 = new Subject<TSource>();
            var r6 = new TResult6[1]; var s6 = new Subject<TSource>();
            var r7 = new TResult7[1]; var s7 = new Subject<TSource>();
            var r8 = new TResult8[1]; var s8 = new Subject<TSource>();
            var r9 = new TResult9[1]; var s9 = new Subject<TSource>();
            var r10 = new TResult10[1]; var s10 = new Subject<TSource>();

            // TODO OnError

            using (aggregatorSelector1(s1).Subscribe(Observer.Create((TResult1 r) => r1[0] = r)))
            using (aggregatorSelector2(s2).Subscribe(Observer.Create((TResult2 r) => r2[0] = r)))
            using (aggregatorSelector3(s3).Subscribe(Observer.Create((TResult3 r) => r3[0] = r)))
            using (aggregatorSelector4(s4).Subscribe(Observer.Create((TResult4 r) => r4[0] = r)))
            using (aggregatorSelector5(s5).Subscribe(Observer.Create((TResult5 r) => r5[0] = r)))
            using (aggregatorSelector6(s6).Subscribe(Observer.Create((TResult6 r) => r6[0] = r)))
            using (aggregatorSelector7(s7).Subscribe(Observer.Create((TResult7 r) => r7[0] = r)))
            using (aggregatorSelector8(s8).Subscribe(Observer.Create((TResult8 r) => r8[0] = r)))
            using (aggregatorSelector9(s9).Subscribe(Observer.Create((TResult9 r) => r9[0] = r)))
            using (aggregatorSelector10(s10).Subscribe(Observer.Create((TResult10 r) => r10[0] = r)))
            {
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
            }

            return resultSelector(r1[0], r2[0], r3[0], r4[0], r5[0], r6[0], r7[0], r8[0], r9[0], r10[0]);
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
        /// <param name="aggregatorSelector1">
        /// The first function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector2">
        /// The second function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector3">
        /// The third function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector4">
        /// The fourth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector5">
        /// The fifth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector6">
        /// The sixth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector7">
        /// The seventh function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector8">
        /// The eighth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector9">
        /// The ninth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector10">
        /// The tenth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector11">
        /// The eleventh function that connects an accumulator to the source.</param>
        /// <param name="resultSelector">
        /// A function to transform the final accumulator value into the result value.</param>
        /// <returns>The transformed final accumulator value.</returns>
        /// <remarks>This method uses immediate execution semantics.</remarks>

        public static TResult Aggregate<TSource, TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9, TResult10, TResult11, TResult>(
            this IEnumerable<TSource> source,
            Func<IObservable<TSource>, IObservable<TResult1>> aggregatorSelector1,
            Func<IObservable<TSource>, IObservable<TResult2>> aggregatorSelector2,
            Func<IObservable<TSource>, IObservable<TResult3>> aggregatorSelector3,
            Func<IObservable<TSource>, IObservable<TResult4>> aggregatorSelector4,
            Func<IObservable<TSource>, IObservable<TResult5>> aggregatorSelector5,
            Func<IObservable<TSource>, IObservable<TResult6>> aggregatorSelector6,
            Func<IObservable<TSource>, IObservable<TResult7>> aggregatorSelector7,
            Func<IObservable<TSource>, IObservable<TResult8>> aggregatorSelector8,
            Func<IObservable<TSource>, IObservable<TResult9>> aggregatorSelector9,
            Func<IObservable<TSource>, IObservable<TResult10>> aggregatorSelector10,
            Func<IObservable<TSource>, IObservable<TResult11>> aggregatorSelector11,
            Func<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9, TResult10, TResult11, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (aggregatorSelector1 == null) throw new ArgumentNullException(nameof(aggregatorSelector1));
            if (aggregatorSelector2 == null) throw new ArgumentNullException(nameof(aggregatorSelector2));
            if (aggregatorSelector3 == null) throw new ArgumentNullException(nameof(aggregatorSelector3));
            if (aggregatorSelector4 == null) throw new ArgumentNullException(nameof(aggregatorSelector4));
            if (aggregatorSelector5 == null) throw new ArgumentNullException(nameof(aggregatorSelector5));
            if (aggregatorSelector6 == null) throw new ArgumentNullException(nameof(aggregatorSelector6));
            if (aggregatorSelector7 == null) throw new ArgumentNullException(nameof(aggregatorSelector7));
            if (aggregatorSelector8 == null) throw new ArgumentNullException(nameof(aggregatorSelector8));
            if (aggregatorSelector9 == null) throw new ArgumentNullException(nameof(aggregatorSelector9));
            if (aggregatorSelector10 == null) throw new ArgumentNullException(nameof(aggregatorSelector10));
            if (aggregatorSelector11 == null) throw new ArgumentNullException(nameof(aggregatorSelector11));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            var r1 = new TResult1[1]; var s1 = new Subject<TSource>();
            var r2 = new TResult2[1]; var s2 = new Subject<TSource>();
            var r3 = new TResult3[1]; var s3 = new Subject<TSource>();
            var r4 = new TResult4[1]; var s4 = new Subject<TSource>();
            var r5 = new TResult5[1]; var s5 = new Subject<TSource>();
            var r6 = new TResult6[1]; var s6 = new Subject<TSource>();
            var r7 = new TResult7[1]; var s7 = new Subject<TSource>();
            var r8 = new TResult8[1]; var s8 = new Subject<TSource>();
            var r9 = new TResult9[1]; var s9 = new Subject<TSource>();
            var r10 = new TResult10[1]; var s10 = new Subject<TSource>();
            var r11 = new TResult11[1]; var s11 = new Subject<TSource>();

            // TODO OnError

            using (aggregatorSelector1(s1).Subscribe(Observer.Create((TResult1 r) => r1[0] = r)))
            using (aggregatorSelector2(s2).Subscribe(Observer.Create((TResult2 r) => r2[0] = r)))
            using (aggregatorSelector3(s3).Subscribe(Observer.Create((TResult3 r) => r3[0] = r)))
            using (aggregatorSelector4(s4).Subscribe(Observer.Create((TResult4 r) => r4[0] = r)))
            using (aggregatorSelector5(s5).Subscribe(Observer.Create((TResult5 r) => r5[0] = r)))
            using (aggregatorSelector6(s6).Subscribe(Observer.Create((TResult6 r) => r6[0] = r)))
            using (aggregatorSelector7(s7).Subscribe(Observer.Create((TResult7 r) => r7[0] = r)))
            using (aggregatorSelector8(s8).Subscribe(Observer.Create((TResult8 r) => r8[0] = r)))
            using (aggregatorSelector9(s9).Subscribe(Observer.Create((TResult9 r) => r9[0] = r)))
            using (aggregatorSelector10(s10).Subscribe(Observer.Create((TResult10 r) => r10[0] = r)))
            using (aggregatorSelector11(s11).Subscribe(Observer.Create((TResult11 r) => r11[0] = r)))
            {
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
            }

            return resultSelector(r1[0], r2[0], r3[0], r4[0], r5[0], r6[0], r7[0], r8[0], r9[0], r10[0], r11[0]);
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
        /// <param name="aggregatorSelector1">
        /// The first function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector2">
        /// The second function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector3">
        /// The third function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector4">
        /// The fourth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector5">
        /// The fifth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector6">
        /// The sixth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector7">
        /// The seventh function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector8">
        /// The eighth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector9">
        /// The ninth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector10">
        /// The tenth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector11">
        /// The eleventh function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector12">
        /// The twelfth function that connects an accumulator to the source.</param>
        /// <param name="resultSelector">
        /// A function to transform the final accumulator value into the result value.</param>
        /// <returns>The transformed final accumulator value.</returns>
        /// <remarks>This method uses immediate execution semantics.</remarks>

        public static TResult Aggregate<TSource, TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9, TResult10, TResult11, TResult12, TResult>(
            this IEnumerable<TSource> source,
            Func<IObservable<TSource>, IObservable<TResult1>> aggregatorSelector1,
            Func<IObservable<TSource>, IObservable<TResult2>> aggregatorSelector2,
            Func<IObservable<TSource>, IObservable<TResult3>> aggregatorSelector3,
            Func<IObservable<TSource>, IObservable<TResult4>> aggregatorSelector4,
            Func<IObservable<TSource>, IObservable<TResult5>> aggregatorSelector5,
            Func<IObservable<TSource>, IObservable<TResult6>> aggregatorSelector6,
            Func<IObservable<TSource>, IObservable<TResult7>> aggregatorSelector7,
            Func<IObservable<TSource>, IObservable<TResult8>> aggregatorSelector8,
            Func<IObservable<TSource>, IObservable<TResult9>> aggregatorSelector9,
            Func<IObservable<TSource>, IObservable<TResult10>> aggregatorSelector10,
            Func<IObservable<TSource>, IObservable<TResult11>> aggregatorSelector11,
            Func<IObservable<TSource>, IObservable<TResult12>> aggregatorSelector12,
            Func<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9, TResult10, TResult11, TResult12, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (aggregatorSelector1 == null) throw new ArgumentNullException(nameof(aggregatorSelector1));
            if (aggregatorSelector2 == null) throw new ArgumentNullException(nameof(aggregatorSelector2));
            if (aggregatorSelector3 == null) throw new ArgumentNullException(nameof(aggregatorSelector3));
            if (aggregatorSelector4 == null) throw new ArgumentNullException(nameof(aggregatorSelector4));
            if (aggregatorSelector5 == null) throw new ArgumentNullException(nameof(aggregatorSelector5));
            if (aggregatorSelector6 == null) throw new ArgumentNullException(nameof(aggregatorSelector6));
            if (aggregatorSelector7 == null) throw new ArgumentNullException(nameof(aggregatorSelector7));
            if (aggregatorSelector8 == null) throw new ArgumentNullException(nameof(aggregatorSelector8));
            if (aggregatorSelector9 == null) throw new ArgumentNullException(nameof(aggregatorSelector9));
            if (aggregatorSelector10 == null) throw new ArgumentNullException(nameof(aggregatorSelector10));
            if (aggregatorSelector11 == null) throw new ArgumentNullException(nameof(aggregatorSelector11));
            if (aggregatorSelector12 == null) throw new ArgumentNullException(nameof(aggregatorSelector12));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            var r1 = new TResult1[1]; var s1 = new Subject<TSource>();
            var r2 = new TResult2[1]; var s2 = new Subject<TSource>();
            var r3 = new TResult3[1]; var s3 = new Subject<TSource>();
            var r4 = new TResult4[1]; var s4 = new Subject<TSource>();
            var r5 = new TResult5[1]; var s5 = new Subject<TSource>();
            var r6 = new TResult6[1]; var s6 = new Subject<TSource>();
            var r7 = new TResult7[1]; var s7 = new Subject<TSource>();
            var r8 = new TResult8[1]; var s8 = new Subject<TSource>();
            var r9 = new TResult9[1]; var s9 = new Subject<TSource>();
            var r10 = new TResult10[1]; var s10 = new Subject<TSource>();
            var r11 = new TResult11[1]; var s11 = new Subject<TSource>();
            var r12 = new TResult12[1]; var s12 = new Subject<TSource>();

            // TODO OnError

            using (aggregatorSelector1(s1).Subscribe(Observer.Create((TResult1 r) => r1[0] = r)))
            using (aggregatorSelector2(s2).Subscribe(Observer.Create((TResult2 r) => r2[0] = r)))
            using (aggregatorSelector3(s3).Subscribe(Observer.Create((TResult3 r) => r3[0] = r)))
            using (aggregatorSelector4(s4).Subscribe(Observer.Create((TResult4 r) => r4[0] = r)))
            using (aggregatorSelector5(s5).Subscribe(Observer.Create((TResult5 r) => r5[0] = r)))
            using (aggregatorSelector6(s6).Subscribe(Observer.Create((TResult6 r) => r6[0] = r)))
            using (aggregatorSelector7(s7).Subscribe(Observer.Create((TResult7 r) => r7[0] = r)))
            using (aggregatorSelector8(s8).Subscribe(Observer.Create((TResult8 r) => r8[0] = r)))
            using (aggregatorSelector9(s9).Subscribe(Observer.Create((TResult9 r) => r9[0] = r)))
            using (aggregatorSelector10(s10).Subscribe(Observer.Create((TResult10 r) => r10[0] = r)))
            using (aggregatorSelector11(s11).Subscribe(Observer.Create((TResult11 r) => r11[0] = r)))
            using (aggregatorSelector12(s12).Subscribe(Observer.Create((TResult12 r) => r12[0] = r)))
            {
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
            }

            return resultSelector(r1[0], r2[0], r3[0], r4[0], r5[0], r6[0], r7[0], r8[0], r9[0], r10[0], r11[0], r12[0]);
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
        /// <param name="aggregatorSelector1">
        /// The first function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector2">
        /// The second function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector3">
        /// The third function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector4">
        /// The fourth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector5">
        /// The fifth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector6">
        /// The sixth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector7">
        /// The seventh function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector8">
        /// The eighth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector9">
        /// The ninth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector10">
        /// The tenth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector11">
        /// The eleventh function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector12">
        /// The twelfth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector13">
        /// The thirteenth function that connects an accumulator to the source.</param>
        /// <param name="resultSelector">
        /// A function to transform the final accumulator value into the result value.</param>
        /// <returns>The transformed final accumulator value.</returns>
        /// <remarks>This method uses immediate execution semantics.</remarks>

        public static TResult Aggregate<TSource, TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9, TResult10, TResult11, TResult12, TResult13, TResult>(
            this IEnumerable<TSource> source,
            Func<IObservable<TSource>, IObservable<TResult1>> aggregatorSelector1,
            Func<IObservable<TSource>, IObservable<TResult2>> aggregatorSelector2,
            Func<IObservable<TSource>, IObservable<TResult3>> aggregatorSelector3,
            Func<IObservable<TSource>, IObservable<TResult4>> aggregatorSelector4,
            Func<IObservable<TSource>, IObservable<TResult5>> aggregatorSelector5,
            Func<IObservable<TSource>, IObservable<TResult6>> aggregatorSelector6,
            Func<IObservable<TSource>, IObservable<TResult7>> aggregatorSelector7,
            Func<IObservable<TSource>, IObservable<TResult8>> aggregatorSelector8,
            Func<IObservable<TSource>, IObservable<TResult9>> aggregatorSelector9,
            Func<IObservable<TSource>, IObservable<TResult10>> aggregatorSelector10,
            Func<IObservable<TSource>, IObservable<TResult11>> aggregatorSelector11,
            Func<IObservable<TSource>, IObservable<TResult12>> aggregatorSelector12,
            Func<IObservable<TSource>, IObservable<TResult13>> aggregatorSelector13,
            Func<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9, TResult10, TResult11, TResult12, TResult13, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (aggregatorSelector1 == null) throw new ArgumentNullException(nameof(aggregatorSelector1));
            if (aggregatorSelector2 == null) throw new ArgumentNullException(nameof(aggregatorSelector2));
            if (aggregatorSelector3 == null) throw new ArgumentNullException(nameof(aggregatorSelector3));
            if (aggregatorSelector4 == null) throw new ArgumentNullException(nameof(aggregatorSelector4));
            if (aggregatorSelector5 == null) throw new ArgumentNullException(nameof(aggregatorSelector5));
            if (aggregatorSelector6 == null) throw new ArgumentNullException(nameof(aggregatorSelector6));
            if (aggregatorSelector7 == null) throw new ArgumentNullException(nameof(aggregatorSelector7));
            if (aggregatorSelector8 == null) throw new ArgumentNullException(nameof(aggregatorSelector8));
            if (aggregatorSelector9 == null) throw new ArgumentNullException(nameof(aggregatorSelector9));
            if (aggregatorSelector10 == null) throw new ArgumentNullException(nameof(aggregatorSelector10));
            if (aggregatorSelector11 == null) throw new ArgumentNullException(nameof(aggregatorSelector11));
            if (aggregatorSelector12 == null) throw new ArgumentNullException(nameof(aggregatorSelector12));
            if (aggregatorSelector13 == null) throw new ArgumentNullException(nameof(aggregatorSelector13));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            var r1 = new TResult1[1]; var s1 = new Subject<TSource>();
            var r2 = new TResult2[1]; var s2 = new Subject<TSource>();
            var r3 = new TResult3[1]; var s3 = new Subject<TSource>();
            var r4 = new TResult4[1]; var s4 = new Subject<TSource>();
            var r5 = new TResult5[1]; var s5 = new Subject<TSource>();
            var r6 = new TResult6[1]; var s6 = new Subject<TSource>();
            var r7 = new TResult7[1]; var s7 = new Subject<TSource>();
            var r8 = new TResult8[1]; var s8 = new Subject<TSource>();
            var r9 = new TResult9[1]; var s9 = new Subject<TSource>();
            var r10 = new TResult10[1]; var s10 = new Subject<TSource>();
            var r11 = new TResult11[1]; var s11 = new Subject<TSource>();
            var r12 = new TResult12[1]; var s12 = new Subject<TSource>();
            var r13 = new TResult13[1]; var s13 = new Subject<TSource>();

            // TODO OnError

            using (aggregatorSelector1(s1).Subscribe(Observer.Create((TResult1 r) => r1[0] = r)))
            using (aggregatorSelector2(s2).Subscribe(Observer.Create((TResult2 r) => r2[0] = r)))
            using (aggregatorSelector3(s3).Subscribe(Observer.Create((TResult3 r) => r3[0] = r)))
            using (aggregatorSelector4(s4).Subscribe(Observer.Create((TResult4 r) => r4[0] = r)))
            using (aggregatorSelector5(s5).Subscribe(Observer.Create((TResult5 r) => r5[0] = r)))
            using (aggregatorSelector6(s6).Subscribe(Observer.Create((TResult6 r) => r6[0] = r)))
            using (aggregatorSelector7(s7).Subscribe(Observer.Create((TResult7 r) => r7[0] = r)))
            using (aggregatorSelector8(s8).Subscribe(Observer.Create((TResult8 r) => r8[0] = r)))
            using (aggregatorSelector9(s9).Subscribe(Observer.Create((TResult9 r) => r9[0] = r)))
            using (aggregatorSelector10(s10).Subscribe(Observer.Create((TResult10 r) => r10[0] = r)))
            using (aggregatorSelector11(s11).Subscribe(Observer.Create((TResult11 r) => r11[0] = r)))
            using (aggregatorSelector12(s12).Subscribe(Observer.Create((TResult12 r) => r12[0] = r)))
            using (aggregatorSelector13(s13).Subscribe(Observer.Create((TResult13 r) => r13[0] = r)))
            {
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
            }

            return resultSelector(r1[0], r2[0], r3[0], r4[0], r5[0], r6[0], r7[0], r8[0], r9[0], r10[0], r11[0], r12[0], r13[0]);
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
        /// <param name="aggregatorSelector1">
        /// The first function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector2">
        /// The second function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector3">
        /// The third function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector4">
        /// The fourth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector5">
        /// The fifth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector6">
        /// The sixth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector7">
        /// The seventh function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector8">
        /// The eighth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector9">
        /// The ninth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector10">
        /// The tenth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector11">
        /// The eleventh function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector12">
        /// The twelfth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector13">
        /// The thirteenth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector14">
        /// The fourteenth function that connects an accumulator to the source.</param>
        /// <param name="resultSelector">
        /// A function to transform the final accumulator value into the result value.</param>
        /// <returns>The transformed final accumulator value.</returns>
        /// <remarks>This method uses immediate execution semantics.</remarks>

        public static TResult Aggregate<TSource, TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9, TResult10, TResult11, TResult12, TResult13, TResult14, TResult>(
            this IEnumerable<TSource> source,
            Func<IObservable<TSource>, IObservable<TResult1>> aggregatorSelector1,
            Func<IObservable<TSource>, IObservable<TResult2>> aggregatorSelector2,
            Func<IObservable<TSource>, IObservable<TResult3>> aggregatorSelector3,
            Func<IObservable<TSource>, IObservable<TResult4>> aggregatorSelector4,
            Func<IObservable<TSource>, IObservable<TResult5>> aggregatorSelector5,
            Func<IObservable<TSource>, IObservable<TResult6>> aggregatorSelector6,
            Func<IObservable<TSource>, IObservable<TResult7>> aggregatorSelector7,
            Func<IObservable<TSource>, IObservable<TResult8>> aggregatorSelector8,
            Func<IObservable<TSource>, IObservable<TResult9>> aggregatorSelector9,
            Func<IObservable<TSource>, IObservable<TResult10>> aggregatorSelector10,
            Func<IObservable<TSource>, IObservable<TResult11>> aggregatorSelector11,
            Func<IObservable<TSource>, IObservable<TResult12>> aggregatorSelector12,
            Func<IObservable<TSource>, IObservable<TResult13>> aggregatorSelector13,
            Func<IObservable<TSource>, IObservable<TResult14>> aggregatorSelector14,
            Func<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9, TResult10, TResult11, TResult12, TResult13, TResult14, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (aggregatorSelector1 == null) throw new ArgumentNullException(nameof(aggregatorSelector1));
            if (aggregatorSelector2 == null) throw new ArgumentNullException(nameof(aggregatorSelector2));
            if (aggregatorSelector3 == null) throw new ArgumentNullException(nameof(aggregatorSelector3));
            if (aggregatorSelector4 == null) throw new ArgumentNullException(nameof(aggregatorSelector4));
            if (aggregatorSelector5 == null) throw new ArgumentNullException(nameof(aggregatorSelector5));
            if (aggregatorSelector6 == null) throw new ArgumentNullException(nameof(aggregatorSelector6));
            if (aggregatorSelector7 == null) throw new ArgumentNullException(nameof(aggregatorSelector7));
            if (aggregatorSelector8 == null) throw new ArgumentNullException(nameof(aggregatorSelector8));
            if (aggregatorSelector9 == null) throw new ArgumentNullException(nameof(aggregatorSelector9));
            if (aggregatorSelector10 == null) throw new ArgumentNullException(nameof(aggregatorSelector10));
            if (aggregatorSelector11 == null) throw new ArgumentNullException(nameof(aggregatorSelector11));
            if (aggregatorSelector12 == null) throw new ArgumentNullException(nameof(aggregatorSelector12));
            if (aggregatorSelector13 == null) throw new ArgumentNullException(nameof(aggregatorSelector13));
            if (aggregatorSelector14 == null) throw new ArgumentNullException(nameof(aggregatorSelector14));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            var r1 = new TResult1[1]; var s1 = new Subject<TSource>();
            var r2 = new TResult2[1]; var s2 = new Subject<TSource>();
            var r3 = new TResult3[1]; var s3 = new Subject<TSource>();
            var r4 = new TResult4[1]; var s4 = new Subject<TSource>();
            var r5 = new TResult5[1]; var s5 = new Subject<TSource>();
            var r6 = new TResult6[1]; var s6 = new Subject<TSource>();
            var r7 = new TResult7[1]; var s7 = new Subject<TSource>();
            var r8 = new TResult8[1]; var s8 = new Subject<TSource>();
            var r9 = new TResult9[1]; var s9 = new Subject<TSource>();
            var r10 = new TResult10[1]; var s10 = new Subject<TSource>();
            var r11 = new TResult11[1]; var s11 = new Subject<TSource>();
            var r12 = new TResult12[1]; var s12 = new Subject<TSource>();
            var r13 = new TResult13[1]; var s13 = new Subject<TSource>();
            var r14 = new TResult14[1]; var s14 = new Subject<TSource>();

            // TODO OnError

            using (aggregatorSelector1(s1).Subscribe(Observer.Create((TResult1 r) => r1[0] = r)))
            using (aggregatorSelector2(s2).Subscribe(Observer.Create((TResult2 r) => r2[0] = r)))
            using (aggregatorSelector3(s3).Subscribe(Observer.Create((TResult3 r) => r3[0] = r)))
            using (aggregatorSelector4(s4).Subscribe(Observer.Create((TResult4 r) => r4[0] = r)))
            using (aggregatorSelector5(s5).Subscribe(Observer.Create((TResult5 r) => r5[0] = r)))
            using (aggregatorSelector6(s6).Subscribe(Observer.Create((TResult6 r) => r6[0] = r)))
            using (aggregatorSelector7(s7).Subscribe(Observer.Create((TResult7 r) => r7[0] = r)))
            using (aggregatorSelector8(s8).Subscribe(Observer.Create((TResult8 r) => r8[0] = r)))
            using (aggregatorSelector9(s9).Subscribe(Observer.Create((TResult9 r) => r9[0] = r)))
            using (aggregatorSelector10(s10).Subscribe(Observer.Create((TResult10 r) => r10[0] = r)))
            using (aggregatorSelector11(s11).Subscribe(Observer.Create((TResult11 r) => r11[0] = r)))
            using (aggregatorSelector12(s12).Subscribe(Observer.Create((TResult12 r) => r12[0] = r)))
            using (aggregatorSelector13(s13).Subscribe(Observer.Create((TResult13 r) => r13[0] = r)))
            using (aggregatorSelector14(s14).Subscribe(Observer.Create((TResult14 r) => r14[0] = r)))
            {
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
            }

            return resultSelector(r1[0], r2[0], r3[0], r4[0], r5[0], r6[0], r7[0], r8[0], r9[0], r10[0], r11[0], r12[0], r13[0], r14[0]);
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
        /// <param name="aggregatorSelector1">
        /// The first function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector2">
        /// The second function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector3">
        /// The third function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector4">
        /// The fourth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector5">
        /// The fifth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector6">
        /// The sixth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector7">
        /// The seventh function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector8">
        /// The eighth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector9">
        /// The ninth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector10">
        /// The tenth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector11">
        /// The eleventh function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector12">
        /// The twelfth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector13">
        /// The thirteenth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector14">
        /// The fourteenth function that connects an accumulator to the source.</param>
        /// <param name="aggregatorSelector15">
        /// The fifteenth function that connects an accumulator to the source.</param>
        /// <param name="resultSelector">
        /// A function to transform the final accumulator value into the result value.</param>
        /// <returns>The transformed final accumulator value.</returns>
        /// <remarks>This method uses immediate execution semantics.</remarks>

        public static TResult Aggregate<TSource, TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9, TResult10, TResult11, TResult12, TResult13, TResult14, TResult15, TResult>(
            this IEnumerable<TSource> source,
            Func<IObservable<TSource>, IObservable<TResult1>> aggregatorSelector1,
            Func<IObservable<TSource>, IObservable<TResult2>> aggregatorSelector2,
            Func<IObservable<TSource>, IObservable<TResult3>> aggregatorSelector3,
            Func<IObservable<TSource>, IObservable<TResult4>> aggregatorSelector4,
            Func<IObservable<TSource>, IObservable<TResult5>> aggregatorSelector5,
            Func<IObservable<TSource>, IObservable<TResult6>> aggregatorSelector6,
            Func<IObservable<TSource>, IObservable<TResult7>> aggregatorSelector7,
            Func<IObservable<TSource>, IObservable<TResult8>> aggregatorSelector8,
            Func<IObservable<TSource>, IObservable<TResult9>> aggregatorSelector9,
            Func<IObservable<TSource>, IObservable<TResult10>> aggregatorSelector10,
            Func<IObservable<TSource>, IObservable<TResult11>> aggregatorSelector11,
            Func<IObservable<TSource>, IObservable<TResult12>> aggregatorSelector12,
            Func<IObservable<TSource>, IObservable<TResult13>> aggregatorSelector13,
            Func<IObservable<TSource>, IObservable<TResult14>> aggregatorSelector14,
            Func<IObservable<TSource>, IObservable<TResult15>> aggregatorSelector15,
            Func<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9, TResult10, TResult11, TResult12, TResult13, TResult14, TResult15, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (aggregatorSelector1 == null) throw new ArgumentNullException(nameof(aggregatorSelector1));
            if (aggregatorSelector2 == null) throw new ArgumentNullException(nameof(aggregatorSelector2));
            if (aggregatorSelector3 == null) throw new ArgumentNullException(nameof(aggregatorSelector3));
            if (aggregatorSelector4 == null) throw new ArgumentNullException(nameof(aggregatorSelector4));
            if (aggregatorSelector5 == null) throw new ArgumentNullException(nameof(aggregatorSelector5));
            if (aggregatorSelector6 == null) throw new ArgumentNullException(nameof(aggregatorSelector6));
            if (aggregatorSelector7 == null) throw new ArgumentNullException(nameof(aggregatorSelector7));
            if (aggregatorSelector8 == null) throw new ArgumentNullException(nameof(aggregatorSelector8));
            if (aggregatorSelector9 == null) throw new ArgumentNullException(nameof(aggregatorSelector9));
            if (aggregatorSelector10 == null) throw new ArgumentNullException(nameof(aggregatorSelector10));
            if (aggregatorSelector11 == null) throw new ArgumentNullException(nameof(aggregatorSelector11));
            if (aggregatorSelector12 == null) throw new ArgumentNullException(nameof(aggregatorSelector12));
            if (aggregatorSelector13 == null) throw new ArgumentNullException(nameof(aggregatorSelector13));
            if (aggregatorSelector14 == null) throw new ArgumentNullException(nameof(aggregatorSelector14));
            if (aggregatorSelector15 == null) throw new ArgumentNullException(nameof(aggregatorSelector15));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            var r1 = new TResult1[1]; var s1 = new Subject<TSource>();
            var r2 = new TResult2[1]; var s2 = new Subject<TSource>();
            var r3 = new TResult3[1]; var s3 = new Subject<TSource>();
            var r4 = new TResult4[1]; var s4 = new Subject<TSource>();
            var r5 = new TResult5[1]; var s5 = new Subject<TSource>();
            var r6 = new TResult6[1]; var s6 = new Subject<TSource>();
            var r7 = new TResult7[1]; var s7 = new Subject<TSource>();
            var r8 = new TResult8[1]; var s8 = new Subject<TSource>();
            var r9 = new TResult9[1]; var s9 = new Subject<TSource>();
            var r10 = new TResult10[1]; var s10 = new Subject<TSource>();
            var r11 = new TResult11[1]; var s11 = new Subject<TSource>();
            var r12 = new TResult12[1]; var s12 = new Subject<TSource>();
            var r13 = new TResult13[1]; var s13 = new Subject<TSource>();
            var r14 = new TResult14[1]; var s14 = new Subject<TSource>();
            var r15 = new TResult15[1]; var s15 = new Subject<TSource>();

            // TODO OnError

            using (aggregatorSelector1(s1).Subscribe(Observer.Create((TResult1 r) => r1[0] = r)))
            using (aggregatorSelector2(s2).Subscribe(Observer.Create((TResult2 r) => r2[0] = r)))
            using (aggregatorSelector3(s3).Subscribe(Observer.Create((TResult3 r) => r3[0] = r)))
            using (aggregatorSelector4(s4).Subscribe(Observer.Create((TResult4 r) => r4[0] = r)))
            using (aggregatorSelector5(s5).Subscribe(Observer.Create((TResult5 r) => r5[0] = r)))
            using (aggregatorSelector6(s6).Subscribe(Observer.Create((TResult6 r) => r6[0] = r)))
            using (aggregatorSelector7(s7).Subscribe(Observer.Create((TResult7 r) => r7[0] = r)))
            using (aggregatorSelector8(s8).Subscribe(Observer.Create((TResult8 r) => r8[0] = r)))
            using (aggregatorSelector9(s9).Subscribe(Observer.Create((TResult9 r) => r9[0] = r)))
            using (aggregatorSelector10(s10).Subscribe(Observer.Create((TResult10 r) => r10[0] = r)))
            using (aggregatorSelector11(s11).Subscribe(Observer.Create((TResult11 r) => r11[0] = r)))
            using (aggregatorSelector12(s12).Subscribe(Observer.Create((TResult12 r) => r12[0] = r)))
            using (aggregatorSelector13(s13).Subscribe(Observer.Create((TResult13 r) => r13[0] = r)))
            using (aggregatorSelector14(s14).Subscribe(Observer.Create((TResult14 r) => r14[0] = r)))
            using (aggregatorSelector15(s15).Subscribe(Observer.Create((TResult15 r) => r15[0] = r)))
            {
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
            }

            return resultSelector(r1[0], r2[0], r3[0], r4[0], r5[0], r6[0], r7[0], r8[0], r9[0], r10[0], r11[0], r12[0], r13[0], r14[0], r15[0]);
        }

    }
}

#endif // !NO_OBSERVABLES
