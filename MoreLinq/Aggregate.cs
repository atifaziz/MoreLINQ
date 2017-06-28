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

#if !NO_OBSERVABLES

namespace MoreLinq
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    static partial class MoreEnumerable
    {
        public static TResult Aggregate<T, TResult1, TResult2, TResult3, TResult4, TResult5, TResult>(
            this IEnumerable<T> source,
            Func<IObservable<T>, IAggregateResultProvider<TResult1>> aggregatorConnector1,
            Func<IObservable<T>, IAggregateResultProvider<TResult2>> aggregatorConnector2,
            Func<IObservable<T>, IAggregateResultProvider<TResult3>> aggregatorConnector3,
            Func<IObservable<T>, IAggregateResultProvider<TResult4>> aggregatorConnector4,
            Func<IObservable<T>, IAggregateResultProvider<TResult5>> aggregatorConnector5,
            Func<TResult1, TResult2, TResult3, TResult4, TResult5, TResult> resultSelector)
        {
            var s1 = new AggregatorSource<T>(); var c1 = aggregatorConnector1(s1);
            var s2 = new AggregatorSource<T>(); var c2 = aggregatorConnector2(s2);
            var s3 = new AggregatorSource<T>(); var c3 = aggregatorConnector3(s3);
            var s4 = new AggregatorSource<T>(); var c4 = aggregatorConnector4(s4);
            var s5 = new AggregatorSource<T>(); var c5 = aggregatorConnector5(s5);

            foreach (var item in source)
            {
                s1.OnNext(item);
                s2.OnNext(item);
                s3.OnNext(item);
                s4.OnNext(item);
                s5.OnNext(item);
            }

            s1.OnCompleted();
            s1.OnCompleted();
            s2.OnCompleted();
            s3.OnCompleted();
            s4.OnCompleted();
            s5.OnCompleted();

            return resultSelector(c1.GetResult(), c2.GetResult(), c3.GetResult(), c4.GetResult(), c5.GetResult());
        }

        sealed class AggregatorSource<T> : IObservable<T>, IObserver<T>
        {
            List<IObserver<T>> _observers;

            public IDisposable Subscribe(IObserver<T> observer)
            {
                if (_observers == null)
                    _observers = new List<IObserver<T>>();
                _observers.Add(observer);
                return new DelegatingDisposable(() => _observers.Remove(observer));
            }

            public void OnCompleted() => _observers.ForEach(o => o.OnCompleted());
            public void OnError(Exception error) => _observers.ForEach(o => o.OnError(error));
            public void OnNext(T value) => _observers.ForEach(o => o.OnNext(value));
        }

        public static IEnumerable<TResult> Scan<T, TResult1, TResult2, TResult3, TResult4, TResult5, TResult>(
            this IEnumerable<T> source,
            Func<IObservable<T>, IScanResultProvider<TResult1>> aggregatorConnector1,
            Func<IObservable<T>, IScanResultProvider<TResult2>> aggregatorConnector2,
            Func<IObservable<T>, IScanResultProvider<TResult3>> aggregatorConnector3,
            Func<IObservable<T>, IScanResultProvider<TResult4>> aggregatorConnector4,
            Func<IObservable<T>, IScanResultProvider<TResult5>> aggregatorConnector5,
            Func<T, TResult1, TResult2, TResult3, TResult4, TResult5, TResult> resultSelector)
        {
            var s1 = new AggregatorSource<T>(); var c1 = aggregatorConnector1(s1);
            var s2 = new AggregatorSource<T>(); var c2 = aggregatorConnector2(s2);
            var s3 = new AggregatorSource<T>(); var c3 = aggregatorConnector3(s3);
            var s4 = new AggregatorSource<T>(); var c4 = aggregatorConnector4(s4);
            var s5 = new AggregatorSource<T>(); var c5 = aggregatorConnector5(s5);

            foreach (var item in source)
            {
                s1.OnNext(item);
                s2.OnNext(item);
                s3.OnNext(item);
                s4.OnNext(item);
                s5.OnNext(item);
                yield return resultSelector(item, c1.GetResult(), c2.GetResult(), c3.GetResult(), c4.GetResult(), c5.GetResult());
            }

            s1.OnCompleted();
            s1.OnCompleted();
            s2.OnCompleted();
            s3.OnCompleted();
            s4.OnCompleted();
            s5.OnCompleted();
        }
    }

    public static class Aggregator
    {
        public static Func<TState> Connect<TState, T>(
            this IObservable<T> source,
            TState seed, Func<TState, T, TState> accumulator) =>
            Connect(source, seed, accumulator, s => s);

        public static Func<TResult> Connect<TState, T, TResult>(
            this IObservable<T> source,
            TState seed, Func<TState, T, TState> accumulator,
            Func<TState, TResult> resultSelector)
        {
            var state = seed;
            source.Subscribe(Observer.Create((T item) => state = accumulator(state, item)));
            return () => resultSelector(state);
        }

        public static IScanResultProvider<int> Sum(this IObservable<int> source) =>
            ScanResultProvider.Create(source.Connect(0, (s, x) => s + x));

        public static IScanResultProvider<int> Count<T>(this IObservable<T> source) =>
            ScanResultProvider.Create(source.Connect(0, (s, _) => s + 1));

        public static IScanResultProvider<T> Some<T>(this IObservable<T> source, Func<T, T, T> accumulator) =>
            ScanResultProvider.Create(
                source.Connect(
                    (Count: 0, State: default(T)),
                    (s, e) => (s.Count + 1, s.Count > 0 ? accumulator(s.State, e) : e),
                    s => s.Count > 0 ? s.State : throw new InvalidOperationException()));

        public static IScanResultProvider<T> Min<T>(this IObservable<T> source)
            where T : IComparable<T> =>
            source.Some((x, y) => x.CompareTo(y) < 0 ? x : y);

        public static IScanResultProvider<T> Max<T>(this IObservable<T> source)
            where T : IComparable<T> =>
            source.Some((x, y) => x.CompareTo(y) > 0 ? x : y);

        public static IAggregateResultProvider<List<T>> List<T>(this IObservable<T> source) =>
            source.Collect(new List<T>());

        public static IAggregateResultProvider<ISet<T>> Distinct<T>(this IObservable<T> source, IEqualityComparer<T> comparer = null) =>
            source.Collect(new HashSet<T>(comparer));

        public static IAggregateResultProvider<TCollection> Collect<T, TCollection>(this IObservable<T> source, TCollection collection)
            where TCollection : ICollection<T> =>
            AggregateResultProvider.Create(source.Connect(collection, (s, t) => { s.Add(t); return s; }, s => s));
    }

    public interface IAggregateResultProvider<out T>
    {
        T GetResult();
    }

    public static class AggregateResultProvider
    {
        public static IAggregateResultProvider<T> Create<T>(Func<T> delegatee) =>
            new DelegatingAggregateResultProvider<T>(delegatee);

        sealed class DelegatingAggregateResultProvider<T> : IAggregateResultProvider<T>
        {
            readonly Func<T> _delegatee;
            public DelegatingAggregateResultProvider(Func<T> delegatee) =>
                _delegatee = delegatee;
            public T GetResult() => _delegatee();
        }
    }

    public interface IScanResultProvider<out T> : IAggregateResultProvider<T> { }

    public static class ScanResultProvider
    {
        public static IScanResultProvider<T> Create<T>(Func<T> delegatee) =>
            new DelegatingScanResultProvider<T>(delegatee);

        sealed class DelegatingScanResultProvider<T> : IScanResultProvider<T>
        {
            readonly Func<T> _delegatee;
            public DelegatingScanResultProvider(Func<T> delegatee) =>
                _delegatee = delegatee;
            public T GetResult() => _delegatee();
        }
    }

    public static class AggregatorSource
    {
        public static IObservable<U> Select<T, U>(this IObservable<T> source, Func<T, U> selector) =>
            Observable.Create<U>(observer => source.Subscribe(Observer.Create((T item) => observer.OnNext(selector(item)))));

        public static IObservable<T> Where<T>(this IObservable<T> source, Func<T, bool> predicate) =>
            Observable.Create<T>(observer => source.Subscribe(Observer.Create((T item) => { if (predicate(item)) observer.OnNext(item); })));
    }

    sealed class Observable
    {
        public static IObservable<T> Create<T>(Func<IObserver<T>, IDisposable> subscribeHandler) =>
            new Observable<T>(subscribeHandler);
    }

    sealed class Observer
    {
        public static IObserver<T> Create<T>(Action<T> onNext, Action<Exception> onError = null, Action onCompleted = null) =>
            new Observer<T>(onNext, onError, onCompleted);
    }

    sealed class Observer<T> : IObserver<T>
    {
        readonly Action<T> _onNext;
        readonly Action<Exception> _onError;
        readonly Action _onCompleted;

        public Observer(Action<T> onNext, Action<Exception> onError = null, Action onCompleted = null)
        {
            _onNext      = onNext ?? throw new ArgumentNullException(nameof(onNext));
            _onError     = onError;
            _onCompleted = onCompleted;
        }

        public void OnCompleted() => _onCompleted?.Invoke();
        public void OnError(Exception error) => _onError?.Invoke(error);
        public void OnNext(T value) => _onNext(value);
    }

    sealed class Observable<T> : IObservable<T>
    {
        readonly Func<IObserver<T>, IDisposable> _subscribeHandler;
        public Observable(Func<IObserver<T>, IDisposable> subscribeHandler) => _subscribeHandler = subscribeHandler;
        public IDisposable Subscribe(IObserver<T> observer) => _subscribeHandler(observer);
    }

    class DelegatingDisposable : IDisposable
    {
        Action _delegatee;

        public DelegatingDisposable(Action delegatee) =>
            _delegatee = delegatee ?? throw new ArgumentNullException(nameof(delegatee));

        public virtual void Dispose()
        {
            var delegatee = _delegatee;
            if (delegatee == null || Interlocked.CompareExchange(ref _delegatee, null, delegatee) != delegatee)
                return;
            delegatee();
        }
    }
}

#endif