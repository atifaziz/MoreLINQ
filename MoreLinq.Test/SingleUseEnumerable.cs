using System;
using System.Collections;
using System.Collections.Generic;

namespace MoreLinq.Test
{
    static class SingleUseEnumerable
    {
        public static IEnumerable<T> Create<T>(IEnumerable<T> source)
        {
            bool enumerated = false;
            return Impl();

             IEnumerable<T> Impl()
             {
                if (enumerated) throw new InvalidOperationException("enumerated twice");

                foreach (var i in source)
                    yield return i;

                enumerated = true;
             }
        }

        sealed class X<T> : IEnumerable<T>
        {
            readonly Func<IEnumerator<T>> _ef;

            public X(Func<IEnumerator<T>> ef) { _ef = ef; }
            public IEnumerator<T> GetEnumerator() => _ef();
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
