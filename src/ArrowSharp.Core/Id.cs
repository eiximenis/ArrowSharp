using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace ArrowSharp.Core
{
    public static class Id
    {
        public static Id<T> Just<T>(in T value) => new Id<T>(in value);

        public static Id<T> Just<T>(in Id<T> value) => new Id<T>(value.Extract());

        public static Id<Unit> Just(in Unit value) => new Id<Unit>(in value, isnull: true);

        public static Id<T> Just<T>(in Option<T> value)
        {
            if (value.IsNone) return None<T>();
            return Just(value.GetOrElse(default));
        }

        public static Id<T> Just<L,T>(in Either<L,T> either)
        {
            if (either.IsLeft) return None<T>();
            return Just(either.Right);
        }

        internal static Id<T> None<T>() => new Id<T>(default, isnull: true);
    }

    public readonly struct Id<T> : IEquatable<Id<T>>
    {
        private readonly T _value;
        private bool IsNone { get; }

        internal Id(in T value) { 
            _value = value;
            IsNone = (value is null);
        }

        internal Id(in T value, bool isnull)
        {
            _value = value;
            IsNone = isnull;
        }

        public Id<R> Map<R>(Func<T, R> mapper) => IsNone ? Id.None<R>() : Id.Just(mapper(_value));

        public Id<R> FlatMap<R>(Func<T, Id<R>> mapper) => IsNone ? Id.None<R>() : Id.Just(mapper(_value));

        public IEnumerable<T> ToEnumerable()
        {
            yield return _value;
        }
       
        public Option<T> ToOption()
        {
            if (IsNone) return Option.None<T>();
            else return Option.Some(_value);
        }

        public static implicit operator Id<T> (T value) => new Id<T>(value);

        public T Extract() => _value;

        public override string ToString() => IsNone ? "NoneId()" : $"Id({_value})";

        public override bool Equals(object obj)
        {
            if (obj is Id<T> id) return Equals(id);
            return (obj is null) && IsNone;
        }

        public bool Equals(Id<T> other)
        {
            if (other.IsNone && IsNone) return true;
            return other._value.Equals(_value);
        }

        public override int GetHashCode()
        {
            return IsNone ? HashCode.Combine($"NoneId_{typeof(T).Name}") : HashCode.Combine(_value);
        }

        public static bool operator ==(Id<T> left, Id<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Id<T> left, Id<T> right)
        {
            return !(left == right);
        }
    }
}
