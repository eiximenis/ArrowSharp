using System;
using System.Collections.Generic;
using System.Text;

namespace ArrowSharp.Core
{
    public class Unit
    {
        static Unit()
        {
            Value = new Unit();
        }
        public static Unit Value { get; private set; }
        private Unit() { }
        public override string ToString() => string.Empty;
        public override int GetHashCode() => 1;
        public override bool Equals(object obj) => false;
    }
}
