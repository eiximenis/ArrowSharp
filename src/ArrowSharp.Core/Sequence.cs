using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrowSharp.Core
{
    public static class Sequence
    {
        public static Sequence<T> Of<T>(params T[] elements) => Of(elements.AsEnumerable());
        public static Sequence<T> Of<T>(IEnumerable<T> elements) => new Sequence<T>(elements);

        public static Sequence<T> Of<T>(params Option<T>[] elements) => Of(elements.AsEnumerable());
        public static Sequence<T> Of<T>(IEnumerable<Option<T>> elements)
        {
            var els = elements.Where(o => !o.IsNone).Select(o => o.GetOrElse(default));
            return new Sequence<T>(els);
        }

        public static async Task<Sequence<T>> OfAsync<T>(IEnumerable<Task<Option<T>>> elements)
        {
            var els = await Task.WhenAll(elements);
            return Of(els);
        }

        public static Sequence<T> Of<_, T>(params Either<_, T>[] elements) => Of(elements.AsEnumerable());
        public static Sequence<T> Of<_, T>(IEnumerable<Either<_, T>> elements)
        {
            var els = elements.Where(e => e.IsRight)
                .Select(e => e.Right.GetOrElse(default));

            return new Sequence<T>(els);
        }

        public static Sequence<X> OfEithers<X, E, R>(IEnumerable<Either<E,R>> eithers, Func<E, X> leftMapper, Func<R, X> rightMapper)
        {
            var els = eithers.Select(e => e.Fold(leftMapper, rightMapper));
            return new Sequence<X>(els);
        }

        public static async Task<Sequence<T>> OfAsync<_,T>(IEnumerable<Task<Either<_,T>>> elements)
        {
            var els = await Task.WhenAll(elements);
            return Of(els);
        }

        public static Sequence<T> Of<T>(params Id<T>[] elements) => Of(elements.AsEnumerable());

        public static Sequence<T> Of<T>(IEnumerable<Id<T>> elements)
        {
            var els = elements.Select(i => i.Extract());
            return new Sequence<T>(els);
        }

    }


    public class Sequence<T> : IEnumerable<T>
    {
        private readonly T[] _data;

        public bool IsEmpty { get => _data != null || _data.Length == 0; }

        public int Count { get => _data?.Length ?? 0; }

        internal Sequence(IEnumerable<T> elements)
        {
            _data = elements?.ToArray() ?? null;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _data?.AsEnumerable().GetEnumerator() ?? Enumerable.Empty<T>().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
