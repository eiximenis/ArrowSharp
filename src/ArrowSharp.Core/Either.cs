using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace ArrowSharp.Core
{
    public static class Either 
    {
        public static Either<L, R> Right<L, R>(R value) => new Either<L, R>(default(L), value, isRight: true);
        public static Either<L, R> Left<L, R>(L value) => new Either<L, R>(value, default(R), isRight: false);
    }

    public struct Either<L,R>
    {
        private readonly L _left;
        private readonly R _right;
        public bool IsRight { get; }
        public bool IsLeft { get => !IsRight; }

        internal Either(L left, R right, bool isRight)
        {
            _left = !isRight ? left : default(L);
            _right = isRight ? right : default(R);
            IsRight = isRight;
        }

        public Either<R, L> Swap() => new Either<R, L>(_right, _left, !IsRight);

        public override string ToString()
        {
            return IsRight ? $"Right({_right})" : $"Left({_right})";
        }

        public Option<L> Left => !IsRight ? new Option<L>(_left) : Option.None<L>();
        public Option<R> Right => IsRight ? new Option<R>(_right) : Option.None<R>();

        public R Fold(R emptyValue, Func<R, R> folder) => IsRight ? folder(_right) : emptyValue;


        public R GetOrElse(R value) => IsRight ? _right : value;

        public R GetOrElse(Func<R> valueGenerator) => IsRight ? _right : valueGenerator();

        public R GetOrHandle(Func<L, R> handler) => IsRight ? _right : handler(_left);

        public Option<R> ToOption() => IsRight ? Option.Some(_right) : Option.None<R>();

        public IEnumerable<R> ToEnumerable()
        {
            if (IsRight) yield return _right;
        }

        public void Deconstruct(out Option<L> left, out Option<R> right)
        {
            left = Left;
            right = Right;
        }
    }
}
