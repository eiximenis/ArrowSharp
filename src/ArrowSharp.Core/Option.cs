using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ArrowSharp.Core
{

    public static class Option
    {
        public static Option<T> Some<T>(in T value) => new Option<T>(in value);
        public static Option<T> None<T>() => new Option<T>();

        public static Option<Unit> Some(in Unit _) => None<Unit>();

        public static Option<T> Some<T>(in Id<T> value)
        {
            return Some(value.Extract());
        }

        public static Option<T> Some<T>(in Option<T> value) => value.IsNone ? None<T>() : value;

        public static Option<T> FromEither<_, T>(in Either<_, T> value) => value.IsRight ? value.Right : None<T>();
    }


    public enum OptionType
    {
        None,
        Some
    }
    public readonly struct Option<T>
    {
        private readonly T _value;

        private readonly bool _hasValue;


        public bool IsNone { get => !_hasValue; }
        public bool IsSome { get => _hasValue; }

        public OptionType Type { get => IsNone ? OptionType.None : OptionType.Some; }

        internal Option(in T value)
        {
            switch (value)
            {
                case null:
                    _hasValue = false;
                    _value = default;
                    break;
                default:
                    _hasValue = true;
                    _value = value;
                    break;
            }
        }

        public static implicit operator Option<T>(T value) => new Option<T>(value);

        public T GetOrElse(T emptyValue) => IsNone ? emptyValue : _value;

        public T Fold(T emptyValue, Func<T, T> folder) => IsNone ? emptyValue : folder(_value);

        public Option<U> FlatMap<U>(Func<T, Option<U>> mapper)
        {
            if (IsNone) return Option.None<U>();
            return Option.Some(mapper(_value));
        }


        public Option<T> Map(Func<T, T> mapper) => IsNone ? Option.None<T>() : new Option<T>(mapper(_value));
        public Option<U> Map<U>(Func<T, U> mapper) => IsNone ? Option.None<U>() : new Option<U>(mapper(_value));

        public IEnumerable<T> ToEnumerable()
        {
            if (_hasValue) yield return _value;
        }

        public static bool operator ==(Option<T> first, Option<T> second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(Option<T> first, Option<T> second)
        {
            return !first.Equals(second);
        }

        public static bool operator ==(Option<T> first, T second)
        {
            return first.Equals(new Option<T>(second));
        }

        public static bool operator !=(Option<T> first, T second)
        {
            return !first.Equals(new Option<T>(second));
        }

        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case null: return false;
                case Option<T> other:
                    if (!other._hasValue && !_hasValue) return true;
                    return other._hasValue && _hasValue && _value.Equals(other._value);
                default: return _hasValue ? _value.Equals(obj) : false;
            }
        }

        public override int GetHashCode() => HashCode.Combine(_value);

        public override string ToString()
        {
            return _hasValue ? $"Some({_value})" : "None";
        }

    }
}
