using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ArrowSharp.Core
{
    public static class ConsList
    {

        public static ConsList<T> Cons<T>(T head, ConsList<T> tail) => new ConsList<T>(head, tail);

        public static ConsList<T> Empty<T>() => new ConsList<T>();

    }

    public sealed class ConsList<T> : IEnumerable<T>
    {
        public T Head { get; }
        public ConsList<T> Tail { get; }

        public bool IsEmpty { get; }

        public int Count => IsEmpty ? 0 : 1 + Tail?.Count ?? 0;

        internal ConsList()
        {
            IsEmpty = true;
            Head = default;
            Tail = null;
        }

        internal ConsList(T head, ConsList<T> tail)
        {
            IsEmpty = false;
            Head = head;
            Tail = tail;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (IsEmpty) yield break;
            else
            {
                yield return Head;
                if (Tail != null)
                {
                    foreach (var item in Tail) { yield return item; }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
    }
}
