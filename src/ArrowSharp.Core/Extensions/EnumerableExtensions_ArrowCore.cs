using ArrowSharp.Core;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Linq
{
    public static class EnumerableExtensions_ArrowCore
    {
        public static IEnumerable<T> Unwrap<T>(this IEnumerable<Option<T>> enumerable)
        {
            foreach (var option in enumerable)
            {
                if (!option.IsNone) yield return option.GetOrElse(default);
            }
        }

        public static IEnumerable<T> Unwrap<T>(this IEnumerable<Option<T>> enumerable, T defaultValue)
        {
            foreach (var option in enumerable)
            {
                yield return option.GetOrElse(defaultValue);
            }
        }

        public static IEnumerable<T> Unwrap<T>(this IEnumerable<Id<T>> enumerable)
        {
            foreach (var id in enumerable)
            {
                yield return id.Extract();
            }
        }
    }
}
