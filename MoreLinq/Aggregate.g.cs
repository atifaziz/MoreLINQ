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

            var r1 = new (bool, TResult1)[1];
            var r2 = new (bool, TResult2)[1];

            var subject = new Subject<TSource>();

            IDisposable Subscribe<T>(int n, IObservable<T> aggregator, (bool Defined, T)[] r, string paramName) =>
                ReferenceEquals(aggregator, subject)
                ? throw new ArgumentException($"Aggregator cannot be identical to the source.", paramName)
                : aggregator.Subscribe(s =>
                      r[0] = r[0].Defined
                           ? throw new InvalidOperationException($"Aggregator #{n} produced multiple results when only one is allowed.")
                           : (true, s));

            // TODO OnError

            using (Subscribe(1, aggregatorSelector1(subject), r1, nameof(aggregatorSelector1)))
            using (Subscribe(2, aggregatorSelector2(subject), r2, nameof(aggregatorSelector2)))
            {
                foreach (var item in source)
                    subject.OnNext(item);
                subject.OnCompleted();
            }

            T Get<T>(int n, (bool Defined, T Value) result) =>
                !result.Defined
                ? throw new InvalidOperationException($"Aggregator #{n} has an undefined result.")
                : result.Value;

            return resultSelector(Get(1, r1[0]), Get(2, r2[0]));
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

            var r1 = new (bool, TResult1)[1];
            var r2 = new (bool, TResult2)[1];
            var r3 = new (bool, TResult3)[1];

            var subject = new Subject<TSource>();

            IDisposable Subscribe<T>(int n, IObservable<T> aggregator, (bool Defined, T)[] r, string paramName) =>
                ReferenceEquals(aggregator, subject)
                ? throw new ArgumentException($"Aggregator cannot be identical to the source.", paramName)
                : aggregator.Subscribe(s =>
                      r[0] = r[0].Defined
                           ? throw new InvalidOperationException($"Aggregator #{n} produced multiple results when only one is allowed.")
                           : (true, s));

            // TODO OnError

            using (Subscribe(1, aggregatorSelector1(subject), r1, nameof(aggregatorSelector1)))
            using (Subscribe(2, aggregatorSelector2(subject), r2, nameof(aggregatorSelector2)))
            using (Subscribe(3, aggregatorSelector3(subject), r3, nameof(aggregatorSelector3)))
            {
                foreach (var item in source)
                    subject.OnNext(item);
                subject.OnCompleted();
            }

            T Get<T>(int n, (bool Defined, T Value) result) =>
                !result.Defined
                ? throw new InvalidOperationException($"Aggregator #{n} has an undefined result.")
                : result.Value;

            return resultSelector(Get(1, r1[0]), Get(2, r2[0]), Get(3, r3[0]));
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

            var r1 = new (bool, TResult1)[1];
            var r2 = new (bool, TResult2)[1];
            var r3 = new (bool, TResult3)[1];
            var r4 = new (bool, TResult4)[1];

            var subject = new Subject<TSource>();

            IDisposable Subscribe<T>(int n, IObservable<T> aggregator, (bool Defined, T)[] r, string paramName) =>
                ReferenceEquals(aggregator, subject)
                ? throw new ArgumentException($"Aggregator cannot be identical to the source.", paramName)
                : aggregator.Subscribe(s =>
                      r[0] = r[0].Defined
                           ? throw new InvalidOperationException($"Aggregator #{n} produced multiple results when only one is allowed.")
                           : (true, s));

            // TODO OnError

            using (Subscribe(1, aggregatorSelector1(subject), r1, nameof(aggregatorSelector1)))
            using (Subscribe(2, aggregatorSelector2(subject), r2, nameof(aggregatorSelector2)))
            using (Subscribe(3, aggregatorSelector3(subject), r3, nameof(aggregatorSelector3)))
            using (Subscribe(4, aggregatorSelector4(subject), r4, nameof(aggregatorSelector4)))
            {
                foreach (var item in source)
                    subject.OnNext(item);
                subject.OnCompleted();
            }

            T Get<T>(int n, (bool Defined, T Value) result) =>
                !result.Defined
                ? throw new InvalidOperationException($"Aggregator #{n} has an undefined result.")
                : result.Value;

            return resultSelector(Get(1, r1[0]), Get(2, r2[0]), Get(3, r3[0]), Get(4, r4[0]));
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

            var r1 = new (bool, TResult1)[1];
            var r2 = new (bool, TResult2)[1];
            var r3 = new (bool, TResult3)[1];
            var r4 = new (bool, TResult4)[1];
            var r5 = new (bool, TResult5)[1];

            var subject = new Subject<TSource>();

            IDisposable Subscribe<T>(int n, IObservable<T> aggregator, (bool Defined, T)[] r, string paramName) =>
                ReferenceEquals(aggregator, subject)
                ? throw new ArgumentException($"Aggregator cannot be identical to the source.", paramName)
                : aggregator.Subscribe(s =>
                      r[0] = r[0].Defined
                           ? throw new InvalidOperationException($"Aggregator #{n} produced multiple results when only one is allowed.")
                           : (true, s));

            // TODO OnError

            using (Subscribe(1, aggregatorSelector1(subject), r1, nameof(aggregatorSelector1)))
            using (Subscribe(2, aggregatorSelector2(subject), r2, nameof(aggregatorSelector2)))
            using (Subscribe(3, aggregatorSelector3(subject), r3, nameof(aggregatorSelector3)))
            using (Subscribe(4, aggregatorSelector4(subject), r4, nameof(aggregatorSelector4)))
            using (Subscribe(5, aggregatorSelector5(subject), r5, nameof(aggregatorSelector5)))
            {
                foreach (var item in source)
                    subject.OnNext(item);
                subject.OnCompleted();
            }

            T Get<T>(int n, (bool Defined, T Value) result) =>
                !result.Defined
                ? throw new InvalidOperationException($"Aggregator #{n} has an undefined result.")
                : result.Value;

            return resultSelector(Get(1, r1[0]), Get(2, r2[0]), Get(3, r3[0]), Get(4, r4[0]), Get(5, r5[0]));
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

            var r1 = new (bool, TResult1)[1];
            var r2 = new (bool, TResult2)[1];
            var r3 = new (bool, TResult3)[1];
            var r4 = new (bool, TResult4)[1];
            var r5 = new (bool, TResult5)[1];
            var r6 = new (bool, TResult6)[1];

            var subject = new Subject<TSource>();

            IDisposable Subscribe<T>(int n, IObservable<T> aggregator, (bool Defined, T)[] r, string paramName) =>
                ReferenceEquals(aggregator, subject)
                ? throw new ArgumentException($"Aggregator cannot be identical to the source.", paramName)
                : aggregator.Subscribe(s =>
                      r[0] = r[0].Defined
                           ? throw new InvalidOperationException($"Aggregator #{n} produced multiple results when only one is allowed.")
                           : (true, s));

            // TODO OnError

            using (Subscribe(1, aggregatorSelector1(subject), r1, nameof(aggregatorSelector1)))
            using (Subscribe(2, aggregatorSelector2(subject), r2, nameof(aggregatorSelector2)))
            using (Subscribe(3, aggregatorSelector3(subject), r3, nameof(aggregatorSelector3)))
            using (Subscribe(4, aggregatorSelector4(subject), r4, nameof(aggregatorSelector4)))
            using (Subscribe(5, aggregatorSelector5(subject), r5, nameof(aggregatorSelector5)))
            using (Subscribe(6, aggregatorSelector6(subject), r6, nameof(aggregatorSelector6)))
            {
                foreach (var item in source)
                    subject.OnNext(item);
                subject.OnCompleted();
            }

            T Get<T>(int n, (bool Defined, T Value) result) =>
                !result.Defined
                ? throw new InvalidOperationException($"Aggregator #{n} has an undefined result.")
                : result.Value;

            return resultSelector(Get(1, r1[0]), Get(2, r2[0]), Get(3, r3[0]), Get(4, r4[0]), Get(5, r5[0]), Get(6, r6[0]));
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

            var r1 = new (bool, TResult1)[1];
            var r2 = new (bool, TResult2)[1];
            var r3 = new (bool, TResult3)[1];
            var r4 = new (bool, TResult4)[1];
            var r5 = new (bool, TResult5)[1];
            var r6 = new (bool, TResult6)[1];
            var r7 = new (bool, TResult7)[1];

            var subject = new Subject<TSource>();

            IDisposable Subscribe<T>(int n, IObservable<T> aggregator, (bool Defined, T)[] r, string paramName) =>
                ReferenceEquals(aggregator, subject)
                ? throw new ArgumentException($"Aggregator cannot be identical to the source.", paramName)
                : aggregator.Subscribe(s =>
                      r[0] = r[0].Defined
                           ? throw new InvalidOperationException($"Aggregator #{n} produced multiple results when only one is allowed.")
                           : (true, s));

            // TODO OnError

            using (Subscribe(1, aggregatorSelector1(subject), r1, nameof(aggregatorSelector1)))
            using (Subscribe(2, aggregatorSelector2(subject), r2, nameof(aggregatorSelector2)))
            using (Subscribe(3, aggregatorSelector3(subject), r3, nameof(aggregatorSelector3)))
            using (Subscribe(4, aggregatorSelector4(subject), r4, nameof(aggregatorSelector4)))
            using (Subscribe(5, aggregatorSelector5(subject), r5, nameof(aggregatorSelector5)))
            using (Subscribe(6, aggregatorSelector6(subject), r6, nameof(aggregatorSelector6)))
            using (Subscribe(7, aggregatorSelector7(subject), r7, nameof(aggregatorSelector7)))
            {
                foreach (var item in source)
                    subject.OnNext(item);
                subject.OnCompleted();
            }

            T Get<T>(int n, (bool Defined, T Value) result) =>
                !result.Defined
                ? throw new InvalidOperationException($"Aggregator #{n} has an undefined result.")
                : result.Value;

            return resultSelector(Get(1, r1[0]), Get(2, r2[0]), Get(3, r3[0]), Get(4, r4[0]), Get(5, r5[0]), Get(6, r6[0]), Get(7, r7[0]));
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

            var r1 = new (bool, TResult1)[1];
            var r2 = new (bool, TResult2)[1];
            var r3 = new (bool, TResult3)[1];
            var r4 = new (bool, TResult4)[1];
            var r5 = new (bool, TResult5)[1];
            var r6 = new (bool, TResult6)[1];
            var r7 = new (bool, TResult7)[1];
            var r8 = new (bool, TResult8)[1];

            var subject = new Subject<TSource>();

            IDisposable Subscribe<T>(int n, IObservable<T> aggregator, (bool Defined, T)[] r, string paramName) =>
                ReferenceEquals(aggregator, subject)
                ? throw new ArgumentException($"Aggregator cannot be identical to the source.", paramName)
                : aggregator.Subscribe(s =>
                      r[0] = r[0].Defined
                           ? throw new InvalidOperationException($"Aggregator #{n} produced multiple results when only one is allowed.")
                           : (true, s));

            // TODO OnError

            using (Subscribe(1, aggregatorSelector1(subject), r1, nameof(aggregatorSelector1)))
            using (Subscribe(2, aggregatorSelector2(subject), r2, nameof(aggregatorSelector2)))
            using (Subscribe(3, aggregatorSelector3(subject), r3, nameof(aggregatorSelector3)))
            using (Subscribe(4, aggregatorSelector4(subject), r4, nameof(aggregatorSelector4)))
            using (Subscribe(5, aggregatorSelector5(subject), r5, nameof(aggregatorSelector5)))
            using (Subscribe(6, aggregatorSelector6(subject), r6, nameof(aggregatorSelector6)))
            using (Subscribe(7, aggregatorSelector7(subject), r7, nameof(aggregatorSelector7)))
            using (Subscribe(8, aggregatorSelector8(subject), r8, nameof(aggregatorSelector8)))
            {
                foreach (var item in source)
                    subject.OnNext(item);
                subject.OnCompleted();
            }

            T Get<T>(int n, (bool Defined, T Value) result) =>
                !result.Defined
                ? throw new InvalidOperationException($"Aggregator #{n} has an undefined result.")
                : result.Value;

            return resultSelector(Get(1, r1[0]), Get(2, r2[0]), Get(3, r3[0]), Get(4, r4[0]), Get(5, r5[0]), Get(6, r6[0]), Get(7, r7[0]), Get(8, r8[0]));
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

            var r1 = new (bool, TResult1)[1];
            var r2 = new (bool, TResult2)[1];
            var r3 = new (bool, TResult3)[1];
            var r4 = new (bool, TResult4)[1];
            var r5 = new (bool, TResult5)[1];
            var r6 = new (bool, TResult6)[1];
            var r7 = new (bool, TResult7)[1];
            var r8 = new (bool, TResult8)[1];
            var r9 = new (bool, TResult9)[1];

            var subject = new Subject<TSource>();

            IDisposable Subscribe<T>(int n, IObservable<T> aggregator, (bool Defined, T)[] r, string paramName) =>
                ReferenceEquals(aggregator, subject)
                ? throw new ArgumentException($"Aggregator cannot be identical to the source.", paramName)
                : aggregator.Subscribe(s =>
                      r[0] = r[0].Defined
                           ? throw new InvalidOperationException($"Aggregator #{n} produced multiple results when only one is allowed.")
                           : (true, s));

            // TODO OnError

            using (Subscribe(1, aggregatorSelector1(subject), r1, nameof(aggregatorSelector1)))
            using (Subscribe(2, aggregatorSelector2(subject), r2, nameof(aggregatorSelector2)))
            using (Subscribe(3, aggregatorSelector3(subject), r3, nameof(aggregatorSelector3)))
            using (Subscribe(4, aggregatorSelector4(subject), r4, nameof(aggregatorSelector4)))
            using (Subscribe(5, aggregatorSelector5(subject), r5, nameof(aggregatorSelector5)))
            using (Subscribe(6, aggregatorSelector6(subject), r6, nameof(aggregatorSelector6)))
            using (Subscribe(7, aggregatorSelector7(subject), r7, nameof(aggregatorSelector7)))
            using (Subscribe(8, aggregatorSelector8(subject), r8, nameof(aggregatorSelector8)))
            using (Subscribe(9, aggregatorSelector9(subject), r9, nameof(aggregatorSelector9)))
            {
                foreach (var item in source)
                    subject.OnNext(item);
                subject.OnCompleted();
            }

            T Get<T>(int n, (bool Defined, T Value) result) =>
                !result.Defined
                ? throw new InvalidOperationException($"Aggregator #{n} has an undefined result.")
                : result.Value;

            return resultSelector(Get(1, r1[0]), Get(2, r2[0]), Get(3, r3[0]), Get(4, r4[0]), Get(5, r5[0]), Get(6, r6[0]), Get(7, r7[0]), Get(8, r8[0]), Get(9, r9[0]));
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

            var r1 = new (bool, TResult1)[1];
            var r2 = new (bool, TResult2)[1];
            var r3 = new (bool, TResult3)[1];
            var r4 = new (bool, TResult4)[1];
            var r5 = new (bool, TResult5)[1];
            var r6 = new (bool, TResult6)[1];
            var r7 = new (bool, TResult7)[1];
            var r8 = new (bool, TResult8)[1];
            var r9 = new (bool, TResult9)[1];
            var r10 = new (bool, TResult10)[1];

            var subject = new Subject<TSource>();

            IDisposable Subscribe<T>(int n, IObservable<T> aggregator, (bool Defined, T)[] r, string paramName) =>
                ReferenceEquals(aggregator, subject)
                ? throw new ArgumentException($"Aggregator cannot be identical to the source.", paramName)
                : aggregator.Subscribe(s =>
                      r[0] = r[0].Defined
                           ? throw new InvalidOperationException($"Aggregator #{n} produced multiple results when only one is allowed.")
                           : (true, s));

            // TODO OnError

            using (Subscribe(1, aggregatorSelector1(subject), r1, nameof(aggregatorSelector1)))
            using (Subscribe(2, aggregatorSelector2(subject), r2, nameof(aggregatorSelector2)))
            using (Subscribe(3, aggregatorSelector3(subject), r3, nameof(aggregatorSelector3)))
            using (Subscribe(4, aggregatorSelector4(subject), r4, nameof(aggregatorSelector4)))
            using (Subscribe(5, aggregatorSelector5(subject), r5, nameof(aggregatorSelector5)))
            using (Subscribe(6, aggregatorSelector6(subject), r6, nameof(aggregatorSelector6)))
            using (Subscribe(7, aggregatorSelector7(subject), r7, nameof(aggregatorSelector7)))
            using (Subscribe(8, aggregatorSelector8(subject), r8, nameof(aggregatorSelector8)))
            using (Subscribe(9, aggregatorSelector9(subject), r9, nameof(aggregatorSelector9)))
            using (Subscribe(10, aggregatorSelector10(subject), r10, nameof(aggregatorSelector10)))
            {
                foreach (var item in source)
                    subject.OnNext(item);
                subject.OnCompleted();
            }

            T Get<T>(int n, (bool Defined, T Value) result) =>
                !result.Defined
                ? throw new InvalidOperationException($"Aggregator #{n} has an undefined result.")
                : result.Value;

            return resultSelector(Get(1, r1[0]), Get(2, r2[0]), Get(3, r3[0]), Get(4, r4[0]), Get(5, r5[0]), Get(6, r6[0]), Get(7, r7[0]), Get(8, r8[0]), Get(9, r9[0]), Get(10, r10[0]));
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

            var r1 = new (bool, TResult1)[1];
            var r2 = new (bool, TResult2)[1];
            var r3 = new (bool, TResult3)[1];
            var r4 = new (bool, TResult4)[1];
            var r5 = new (bool, TResult5)[1];
            var r6 = new (bool, TResult6)[1];
            var r7 = new (bool, TResult7)[1];
            var r8 = new (bool, TResult8)[1];
            var r9 = new (bool, TResult9)[1];
            var r10 = new (bool, TResult10)[1];
            var r11 = new (bool, TResult11)[1];

            var subject = new Subject<TSource>();

            IDisposable Subscribe<T>(int n, IObservable<T> aggregator, (bool Defined, T)[] r, string paramName) =>
                ReferenceEquals(aggregator, subject)
                ? throw new ArgumentException($"Aggregator cannot be identical to the source.", paramName)
                : aggregator.Subscribe(s =>
                      r[0] = r[0].Defined
                           ? throw new InvalidOperationException($"Aggregator #{n} produced multiple results when only one is allowed.")
                           : (true, s));

            // TODO OnError

            using (Subscribe(1, aggregatorSelector1(subject), r1, nameof(aggregatorSelector1)))
            using (Subscribe(2, aggregatorSelector2(subject), r2, nameof(aggregatorSelector2)))
            using (Subscribe(3, aggregatorSelector3(subject), r3, nameof(aggregatorSelector3)))
            using (Subscribe(4, aggregatorSelector4(subject), r4, nameof(aggregatorSelector4)))
            using (Subscribe(5, aggregatorSelector5(subject), r5, nameof(aggregatorSelector5)))
            using (Subscribe(6, aggregatorSelector6(subject), r6, nameof(aggregatorSelector6)))
            using (Subscribe(7, aggregatorSelector7(subject), r7, nameof(aggregatorSelector7)))
            using (Subscribe(8, aggregatorSelector8(subject), r8, nameof(aggregatorSelector8)))
            using (Subscribe(9, aggregatorSelector9(subject), r9, nameof(aggregatorSelector9)))
            using (Subscribe(10, aggregatorSelector10(subject), r10, nameof(aggregatorSelector10)))
            using (Subscribe(11, aggregatorSelector11(subject), r11, nameof(aggregatorSelector11)))
            {
                foreach (var item in source)
                    subject.OnNext(item);
                subject.OnCompleted();
            }

            T Get<T>(int n, (bool Defined, T Value) result) =>
                !result.Defined
                ? throw new InvalidOperationException($"Aggregator #{n} has an undefined result.")
                : result.Value;

            return resultSelector(Get(1, r1[0]), Get(2, r2[0]), Get(3, r3[0]), Get(4, r4[0]), Get(5, r5[0]), Get(6, r6[0]), Get(7, r7[0]), Get(8, r8[0]), Get(9, r9[0]), Get(10, r10[0]), Get(11, r11[0]));
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

            var r1 = new (bool, TResult1)[1];
            var r2 = new (bool, TResult2)[1];
            var r3 = new (bool, TResult3)[1];
            var r4 = new (bool, TResult4)[1];
            var r5 = new (bool, TResult5)[1];
            var r6 = new (bool, TResult6)[1];
            var r7 = new (bool, TResult7)[1];
            var r8 = new (bool, TResult8)[1];
            var r9 = new (bool, TResult9)[1];
            var r10 = new (bool, TResult10)[1];
            var r11 = new (bool, TResult11)[1];
            var r12 = new (bool, TResult12)[1];

            var subject = new Subject<TSource>();

            IDisposable Subscribe<T>(int n, IObservable<T> aggregator, (bool Defined, T)[] r, string paramName) =>
                ReferenceEquals(aggregator, subject)
                ? throw new ArgumentException($"Aggregator cannot be identical to the source.", paramName)
                : aggregator.Subscribe(s =>
                      r[0] = r[0].Defined
                           ? throw new InvalidOperationException($"Aggregator #{n} produced multiple results when only one is allowed.")
                           : (true, s));

            // TODO OnError

            using (Subscribe(1, aggregatorSelector1(subject), r1, nameof(aggregatorSelector1)))
            using (Subscribe(2, aggregatorSelector2(subject), r2, nameof(aggregatorSelector2)))
            using (Subscribe(3, aggregatorSelector3(subject), r3, nameof(aggregatorSelector3)))
            using (Subscribe(4, aggregatorSelector4(subject), r4, nameof(aggregatorSelector4)))
            using (Subscribe(5, aggregatorSelector5(subject), r5, nameof(aggregatorSelector5)))
            using (Subscribe(6, aggregatorSelector6(subject), r6, nameof(aggregatorSelector6)))
            using (Subscribe(7, aggregatorSelector7(subject), r7, nameof(aggregatorSelector7)))
            using (Subscribe(8, aggregatorSelector8(subject), r8, nameof(aggregatorSelector8)))
            using (Subscribe(9, aggregatorSelector9(subject), r9, nameof(aggregatorSelector9)))
            using (Subscribe(10, aggregatorSelector10(subject), r10, nameof(aggregatorSelector10)))
            using (Subscribe(11, aggregatorSelector11(subject), r11, nameof(aggregatorSelector11)))
            using (Subscribe(12, aggregatorSelector12(subject), r12, nameof(aggregatorSelector12)))
            {
                foreach (var item in source)
                    subject.OnNext(item);
                subject.OnCompleted();
            }

            T Get<T>(int n, (bool Defined, T Value) result) =>
                !result.Defined
                ? throw new InvalidOperationException($"Aggregator #{n} has an undefined result.")
                : result.Value;

            return resultSelector(Get(1, r1[0]), Get(2, r2[0]), Get(3, r3[0]), Get(4, r4[0]), Get(5, r5[0]), Get(6, r6[0]), Get(7, r7[0]), Get(8, r8[0]), Get(9, r9[0]), Get(10, r10[0]), Get(11, r11[0]), Get(12, r12[0]));
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

            var r1 = new (bool, TResult1)[1];
            var r2 = new (bool, TResult2)[1];
            var r3 = new (bool, TResult3)[1];
            var r4 = new (bool, TResult4)[1];
            var r5 = new (bool, TResult5)[1];
            var r6 = new (bool, TResult6)[1];
            var r7 = new (bool, TResult7)[1];
            var r8 = new (bool, TResult8)[1];
            var r9 = new (bool, TResult9)[1];
            var r10 = new (bool, TResult10)[1];
            var r11 = new (bool, TResult11)[1];
            var r12 = new (bool, TResult12)[1];
            var r13 = new (bool, TResult13)[1];

            var subject = new Subject<TSource>();

            IDisposable Subscribe<T>(int n, IObservable<T> aggregator, (bool Defined, T)[] r, string paramName) =>
                ReferenceEquals(aggregator, subject)
                ? throw new ArgumentException($"Aggregator cannot be identical to the source.", paramName)
                : aggregator.Subscribe(s =>
                      r[0] = r[0].Defined
                           ? throw new InvalidOperationException($"Aggregator #{n} produced multiple results when only one is allowed.")
                           : (true, s));

            // TODO OnError

            using (Subscribe(1, aggregatorSelector1(subject), r1, nameof(aggregatorSelector1)))
            using (Subscribe(2, aggregatorSelector2(subject), r2, nameof(aggregatorSelector2)))
            using (Subscribe(3, aggregatorSelector3(subject), r3, nameof(aggregatorSelector3)))
            using (Subscribe(4, aggregatorSelector4(subject), r4, nameof(aggregatorSelector4)))
            using (Subscribe(5, aggregatorSelector5(subject), r5, nameof(aggregatorSelector5)))
            using (Subscribe(6, aggregatorSelector6(subject), r6, nameof(aggregatorSelector6)))
            using (Subscribe(7, aggregatorSelector7(subject), r7, nameof(aggregatorSelector7)))
            using (Subscribe(8, aggregatorSelector8(subject), r8, nameof(aggregatorSelector8)))
            using (Subscribe(9, aggregatorSelector9(subject), r9, nameof(aggregatorSelector9)))
            using (Subscribe(10, aggregatorSelector10(subject), r10, nameof(aggregatorSelector10)))
            using (Subscribe(11, aggregatorSelector11(subject), r11, nameof(aggregatorSelector11)))
            using (Subscribe(12, aggregatorSelector12(subject), r12, nameof(aggregatorSelector12)))
            using (Subscribe(13, aggregatorSelector13(subject), r13, nameof(aggregatorSelector13)))
            {
                foreach (var item in source)
                    subject.OnNext(item);
                subject.OnCompleted();
            }

            T Get<T>(int n, (bool Defined, T Value) result) =>
                !result.Defined
                ? throw new InvalidOperationException($"Aggregator #{n} has an undefined result.")
                : result.Value;

            return resultSelector(Get(1, r1[0]), Get(2, r2[0]), Get(3, r3[0]), Get(4, r4[0]), Get(5, r5[0]), Get(6, r6[0]), Get(7, r7[0]), Get(8, r8[0]), Get(9, r9[0]), Get(10, r10[0]), Get(11, r11[0]), Get(12, r12[0]), Get(13, r13[0]));
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

            var r1 = new (bool, TResult1)[1];
            var r2 = new (bool, TResult2)[1];
            var r3 = new (bool, TResult3)[1];
            var r4 = new (bool, TResult4)[1];
            var r5 = new (bool, TResult5)[1];
            var r6 = new (bool, TResult6)[1];
            var r7 = new (bool, TResult7)[1];
            var r8 = new (bool, TResult8)[1];
            var r9 = new (bool, TResult9)[1];
            var r10 = new (bool, TResult10)[1];
            var r11 = new (bool, TResult11)[1];
            var r12 = new (bool, TResult12)[1];
            var r13 = new (bool, TResult13)[1];
            var r14 = new (bool, TResult14)[1];

            var subject = new Subject<TSource>();

            IDisposable Subscribe<T>(int n, IObservable<T> aggregator, (bool Defined, T)[] r, string paramName) =>
                ReferenceEquals(aggregator, subject)
                ? throw new ArgumentException($"Aggregator cannot be identical to the source.", paramName)
                : aggregator.Subscribe(s =>
                      r[0] = r[0].Defined
                           ? throw new InvalidOperationException($"Aggregator #{n} produced multiple results when only one is allowed.")
                           : (true, s));

            // TODO OnError

            using (Subscribe(1, aggregatorSelector1(subject), r1, nameof(aggregatorSelector1)))
            using (Subscribe(2, aggregatorSelector2(subject), r2, nameof(aggregatorSelector2)))
            using (Subscribe(3, aggregatorSelector3(subject), r3, nameof(aggregatorSelector3)))
            using (Subscribe(4, aggregatorSelector4(subject), r4, nameof(aggregatorSelector4)))
            using (Subscribe(5, aggregatorSelector5(subject), r5, nameof(aggregatorSelector5)))
            using (Subscribe(6, aggregatorSelector6(subject), r6, nameof(aggregatorSelector6)))
            using (Subscribe(7, aggregatorSelector7(subject), r7, nameof(aggregatorSelector7)))
            using (Subscribe(8, aggregatorSelector8(subject), r8, nameof(aggregatorSelector8)))
            using (Subscribe(9, aggregatorSelector9(subject), r9, nameof(aggregatorSelector9)))
            using (Subscribe(10, aggregatorSelector10(subject), r10, nameof(aggregatorSelector10)))
            using (Subscribe(11, aggregatorSelector11(subject), r11, nameof(aggregatorSelector11)))
            using (Subscribe(12, aggregatorSelector12(subject), r12, nameof(aggregatorSelector12)))
            using (Subscribe(13, aggregatorSelector13(subject), r13, nameof(aggregatorSelector13)))
            using (Subscribe(14, aggregatorSelector14(subject), r14, nameof(aggregatorSelector14)))
            {
                foreach (var item in source)
                    subject.OnNext(item);
                subject.OnCompleted();
            }

            T Get<T>(int n, (bool Defined, T Value) result) =>
                !result.Defined
                ? throw new InvalidOperationException($"Aggregator #{n} has an undefined result.")
                : result.Value;

            return resultSelector(Get(1, r1[0]), Get(2, r2[0]), Get(3, r3[0]), Get(4, r4[0]), Get(5, r5[0]), Get(6, r6[0]), Get(7, r7[0]), Get(8, r8[0]), Get(9, r9[0]), Get(10, r10[0]), Get(11, r11[0]), Get(12, r12[0]), Get(13, r13[0]), Get(14, r14[0]));
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

            var r1 = new (bool, TResult1)[1];
            var r2 = new (bool, TResult2)[1];
            var r3 = new (bool, TResult3)[1];
            var r4 = new (bool, TResult4)[1];
            var r5 = new (bool, TResult5)[1];
            var r6 = new (bool, TResult6)[1];
            var r7 = new (bool, TResult7)[1];
            var r8 = new (bool, TResult8)[1];
            var r9 = new (bool, TResult9)[1];
            var r10 = new (bool, TResult10)[1];
            var r11 = new (bool, TResult11)[1];
            var r12 = new (bool, TResult12)[1];
            var r13 = new (bool, TResult13)[1];
            var r14 = new (bool, TResult14)[1];
            var r15 = new (bool, TResult15)[1];

            var subject = new Subject<TSource>();

            IDisposable Subscribe<T>(int n, IObservable<T> aggregator, (bool Defined, T)[] r, string paramName) =>
                ReferenceEquals(aggregator, subject)
                ? throw new ArgumentException($"Aggregator cannot be identical to the source.", paramName)
                : aggregator.Subscribe(s =>
                      r[0] = r[0].Defined
                           ? throw new InvalidOperationException($"Aggregator #{n} produced multiple results when only one is allowed.")
                           : (true, s));

            // TODO OnError

            using (Subscribe(1, aggregatorSelector1(subject), r1, nameof(aggregatorSelector1)))
            using (Subscribe(2, aggregatorSelector2(subject), r2, nameof(aggregatorSelector2)))
            using (Subscribe(3, aggregatorSelector3(subject), r3, nameof(aggregatorSelector3)))
            using (Subscribe(4, aggregatorSelector4(subject), r4, nameof(aggregatorSelector4)))
            using (Subscribe(5, aggregatorSelector5(subject), r5, nameof(aggregatorSelector5)))
            using (Subscribe(6, aggregatorSelector6(subject), r6, nameof(aggregatorSelector6)))
            using (Subscribe(7, aggregatorSelector7(subject), r7, nameof(aggregatorSelector7)))
            using (Subscribe(8, aggregatorSelector8(subject), r8, nameof(aggregatorSelector8)))
            using (Subscribe(9, aggregatorSelector9(subject), r9, nameof(aggregatorSelector9)))
            using (Subscribe(10, aggregatorSelector10(subject), r10, nameof(aggregatorSelector10)))
            using (Subscribe(11, aggregatorSelector11(subject), r11, nameof(aggregatorSelector11)))
            using (Subscribe(12, aggregatorSelector12(subject), r12, nameof(aggregatorSelector12)))
            using (Subscribe(13, aggregatorSelector13(subject), r13, nameof(aggregatorSelector13)))
            using (Subscribe(14, aggregatorSelector14(subject), r14, nameof(aggregatorSelector14)))
            using (Subscribe(15, aggregatorSelector15(subject), r15, nameof(aggregatorSelector15)))
            {
                foreach (var item in source)
                    subject.OnNext(item);
                subject.OnCompleted();
            }

            T Get<T>(int n, (bool Defined, T Value) result) =>
                !result.Defined
                ? throw new InvalidOperationException($"Aggregator #{n} has an undefined result.")
                : result.Value;

            return resultSelector(Get(1, r1[0]), Get(2, r2[0]), Get(3, r3[0]), Get(4, r4[0]), Get(5, r5[0]), Get(6, r6[0]), Get(7, r7[0]), Get(8, r8[0]), Get(9, r9[0]), Get(10, r10[0]), Get(11, r11[0]), Get(12, r12[0]), Get(13, r13[0]), Get(14, r14[0]), Get(15, r15[0]));
        }

    }
}

#endif // !NO_OBSERVABLES
