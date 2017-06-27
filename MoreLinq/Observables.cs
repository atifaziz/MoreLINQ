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

    static class Observable
    {
        public static IObservable<T> Create<T>(Func<IObserver<T>, IDisposable> subscribeHandler) =>
            new Observable<T>(subscribeHandler);

        public static IObservable<TResult> Select<T, TResult>(this IObservable<T> source, Func<T, TResult> selector) =>
            Create<TResult>(observer => source.Subscribe(Observer.Create((T item) => observer.OnNext(selector(item)))));

        public static IObservable<T> Where<T>(this IObservable<T> source, Func<T, bool> predicate) =>
            Create<T>(observer => source.Subscribe(Observer.Create((T item) => { if (predicate(item)) observer.OnNext(item); })));
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

    sealed class DelegatingDisposable : IDisposable
    {
        Action _delegatee;

        public DelegatingDisposable(Action delegatee) =>
            _delegatee = delegatee ?? throw new ArgumentNullException(nameof(delegatee));

        public void Dispose()
        {
            var delegatee = _delegatee;
            if (delegatee == null || Interlocked.CompareExchange(ref _delegatee, null, delegatee) != delegatee)
                return;
            delegatee();
        }
    }

    sealed class Subject<T> : IObservable<T>, IObserver<T>
    {
        List<IObserver<T>> _observers;

        public IDisposable Subscribe(IObserver<T> observer)
        {
            if (_observers == null)
                _observers = new List<IObserver<T>>();
            _observers.Add(observer);
            return new DelegatingDisposable(() => _observers.Remove(observer));
        }

        public void OnCompleted() => _observers.ToArray().ForEach(o => o.OnCompleted());
        public void OnError(Exception error) => _observers.ToArray().ForEach(o => o.OnError(error));
        public void OnNext(T value) => _observers.ToArray().ForEach(o => o.OnNext(value));
    }
}

#endif