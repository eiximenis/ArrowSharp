using FluentAssertions.Equivalency;
using FluentAssertions.Specialized;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ArrowSharp.Core.Extensions
{
    public static class FuncExtensions
    {

        public static Func<R> Bind<T, R>(this Func<T, R> func, T value) => () => func(value);
        public static Func<T2, R> Bind<T, T2, R>(this Func<T, T2, R> func, T value) => t2 => func(value, t2);
        public static Func<T2, T3, R> Bind<T, T2, T3, R>(this Func<T, T2, T3, R> func, T value) => (t2, t3) => func(value, t2, t3);
        public static Func<T2, T3, T4, R> Bind<T, T2, T3, T4, R>(this Func<T, T2, T3, T4, R> func, T value) => (t2, t3, t4) => func(value, t2, t3, t4);
        public static Func<T2, T3, T4, T5, R> Bind<T, T2, T3, T4, T5, R>(this Func<T, T2, T3, T4, T5, R> func, T value) => (t2, t3, t4, t5) => func(value, t2, t3, t4, t5);


        public static Func<T, V> Compose<T, R, V>(this Func<R, V> outer, Func<T, R> inner)
        {
            return x => outer(inner(x));
        }


        public static R Invoke<T, R>(this Func<T, R> func, Option<T> arg, R defaultValue = default)
        {
            return arg.IsSome ? func(arg.ForceGet()) : defaultValue;
        }

        public static R Invoke<T, R>(this Func<T, R> func, Id<T> arg)
        {
            return func(arg.Extract());
        }

        public static Func<T, R> Fold<T, R>(this Func<T, Option<R>> func, R defaultValue = default)
        {
            return t => func(t).GetOrElse(defaultValue);
        }

        public static Func<T, R> Fold<T, R>(this Func<T, Option<R>> func, Func<R> defaultValueFactory)
        {
            return t => func(t).GetOrElse(defaultValueFactory());
        }

        public static Func<T, T2, R> Fold<T, T2, R>(this Func<T, T2, Option<R>> func, R defaultValue = default)
        {
            return (t, t2) => func(t, t2).GetOrElse(defaultValue);
        }

        public static Func<T, T2, R> Fold<T, T2, R>(this Func<T, T2, Option<R>> func, Func<R> defaultValueFactory)
        {
            return (t, t2) => func(t, t2).GetOrElse(defaultValueFactory());
        }

        public static Func<T, R> Fold<T, R>(this Func<T, Id<R>> func)
        {
            return t => func(t).Extract();
        }


    }
}
