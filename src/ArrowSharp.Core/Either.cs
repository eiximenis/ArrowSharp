using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ArrowSharp.Core
{
    public static class Either 
    {
        public static Either<L, R> Right<L, R>(R value) => new Either<L, R>(default(L), value, isRight: true);
        public static Either<L, R> Left<L, R>(L value) => new Either<L, R>(value, default(R), isRight: false);
        public static Either<L, R> Cond<L, R>(Func<bool> predicate, Func<L> left, Func<R> right) => predicate() ? Right<L, R>(right()) : Left<L, R>(left());
        public static async Task<Either<L, R>> RightAsync<L, R>(Task<R> value) => Right<L, R>(await value.ConfigureAwait(false));
        public static async Task<Either<L, R>> LeftAsync<L, R>(Task<L> value) => Left<L, R>(await value.ConfigureAwait(false));
        public static async Task<Either<L, R>> CondAsync<L, R>(Func<bool> predicate, Func<Task<L>> left, Func<Task<R>> right) =>
            predicate() ? Right<L, R>(await right().ConfigureAwait(false)) : Left<L, R>(await left().ConfigureAwait(false));
    }


    public enum EitherType
    {
        Left,
        Right
    }

    public struct Either<L,R>
    {
        private readonly L _left;
        private readonly R _right;
        public bool IsRight { get; }
        public bool IsLeft { get => !IsRight; }

        public EitherType Type { get => IsRight ? EitherType.Right : EitherType.Left; }

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

        public X Fold<X>(X emptyValue, Func<R, X> folder) => IsRight ? folder(_right) : emptyValue;
        public X FoldLeft<X>(X emptyValue, Func<L, X> leftFolder) => IsLeft ? leftFolder(_left) : emptyValue;
        public X Fold<X>(Func<L, X> leftFolder, Func<R, X> rightFolder) => IsRight ? rightFolder(_right) : leftFolder(_left);

        public R GetOrElse(R value) => IsRight ? _right : value;

        public R GetOrHandle(Func<L, R> handler) => IsRight ? _right : handler(_left);

        public Either<L, X> Map<X>(Func<R, X> mapper) => IsRight ? Either.Right<L, X>(mapper(_right)) : Either.Left<L, X>(_left);

        public Either<X, R> MapLeft<X>(Func<L, X> mapper) => IsLeft ? Either.Left<X, R>(mapper(_left)) : Either.Right<X, R>(_right);

        public Either<X, S> Bimap<X, S>(Func<L, X> leftMapper, Func<R, S> rightMapper) => IsRight ? Either.Right<X, S>(rightMapper(_right)) : Either.Left<X, S>(leftMapper(_left));

        public Option<R> ToOption() => IsRight ? Option.Some(_right) : Option.None<R>();

        public Option<L> ToOptionLeft() => IsLeft ? Option.Some(_left) : Option.None<L>();

        public void Catch(Action<L> catcher)
        {
            if (IsLeft) catcher(_left);
        }

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
