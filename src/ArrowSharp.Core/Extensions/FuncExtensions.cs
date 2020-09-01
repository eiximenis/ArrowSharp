using FluentAssertions.Equivalency;
using FluentAssertions.Specialized;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ArrowSharp.Core.Extensions
{
    public static class FuncExtensions
    {
        public static Func<T, V> Compose<T, R, V>(this Func<R, V> outer, Func<T, R> inner)
        {
            return x => outer(inner(x));
        }


        public static R Invoke<T, R> (this Func<T, R> func, Option<T> arg, R defaultValue = default)
        {
            return arg.IsSome ? func(arg.ForceGet()) : defaultValue;
        }

        public static Func<T, R> Fold<T, R>(this Func<T, Option<R>> func, R defaultValue = default)
        {
            return t => func(t).GetOrElse(defaultValue);
        }

        public static Func<T, T2, R> Fold<T,T2,  R>(this Func<T, T2, Option<R>> func, R defaultValue = default)
        {
            return (t, t2) => func(t, t2).GetOrElse(defaultValue);
        }


    }
}
