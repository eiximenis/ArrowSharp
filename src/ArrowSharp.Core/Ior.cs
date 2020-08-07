using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ArrowSharp.Core
{
    public enum IorValueFilled
    {
        Left,
        Right,
        Both
    }

    public static class Ior
    {
        public static Ior<L, R> Right<L, R>(R value) => new Ior<L, R>(default(L), value, IorValueFilled.Right);
        public static Ior<L, R> Left<L, R>(L value) => new Ior<L, R>(value, default(R), IorValueFilled.Left);
        public static Ior<L, R> Both<L, R>(L lvalue, R rvalue) => new Ior<L, R>(lvalue, rvalue, IorValueFilled.Both);

    }

    public readonly struct Ior<L, R>
    {
        private readonly L _left;
        private readonly R _right;
        private readonly IorValueFilled _valuesFilled;

        internal Ior(in L left, in R right, IorValueFilled valuesFilled)
        {
            _left = valuesFilled == IorValueFilled.Left || valuesFilled == IorValueFilled.Both ? left : default(L);
            _right = valuesFilled == IorValueFilled.Right || valuesFilled == IorValueFilled.Both ? right : default(R);
            _valuesFilled = valuesFilled;
        }

        public override string ToString()
        {
            switch (_valuesFilled)
            {
                case IorValueFilled.Left:
                    return $"Left({_left})";
                case IorValueFilled.Right:
                    return $"Right({_right})";
                default:
                    return $"Both({_left},{_right})";
            }
        }

        public bool IsLeft { get => _valuesFilled == IorValueFilled.Left; }
        public bool IsRight { get => _valuesFilled == IorValueFilled.Right; }
        public bool IsBoth { get => _valuesFilled == IorValueFilled.Both; }

        public Option<L> Left { get => IsLeft  || IsBoth ? Option.Some(_left) : Option.None<L>(); }
        public Option<R> Right { get => IsRight || IsBoth ? Option.Some(_right) : Option.None<R>(); }

        public Ior<L, X> Map<X>(Func<R, X> mapper)
        {
            switch (_valuesFilled)
            {
                case IorValueFilled.Left:
                    return Ior.Left<L, X>(_left);
                case IorValueFilled.Right:
                    return Ior.Right<L, X>(mapper(_right));
                default:
                    return Ior.Both(_left, mapper(_right));
            }
        }

        public Ior<X, R> MapLeft<X>(Func<L, X> mapper)
        {
            switch (_valuesFilled)
            {
                case IorValueFilled.Left:
                    return Ior.Left<X, R>(mapper(_left));
                case IorValueFilled.Right:
                    return Ior.Right<X, R>(_right);
                default:
                    return Ior.Both(mapper(_left), _right);
            }
        }

        public Ior<X, S> Bimap<X, S>(Func<L, X> leftMapper, Func<R, S> rightMapper)
        {
            switch (_valuesFilled)
            {
                case IorValueFilled.Left:
                    return Ior.Left<X, S>(leftMapper(_left));
                case IorValueFilled.Right:
                    return Ior.Right<X, S>(rightMapper(_right));
                default:
                    return Ior.Both(leftMapper(_left), rightMapper(_right));
            }
        }

        public Option<L> ToLeftOption()
        {
            if (IsRight) return Option.None<L>();
            return Option.Some(_left);
        }
        public Option<R> ToOption()
        {
            if (IsLeft) return Option.None<R>();
            return Option.Some(_right);
        }

        public Either<L,R> ToEither()
        {
            switch (_valuesFilled)
            {
                case IorValueFilled.Left:
                    return Either.Left<L, R>(_left);
                default:
                    return Either.Right<L, R>(_right);
            }
        }

        public void Deconstruct(out Option<L> left, out Option<R> right)
        {
            left = IsLeft || IsBoth ? Option.Some(_left) : Option.None<L>();
            right = IsRight || IsBoth ? Option.Some(_right) : Option.None<R>();
        }
    }
}
