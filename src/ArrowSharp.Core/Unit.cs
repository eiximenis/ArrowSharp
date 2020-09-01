using System;
using System.Collections.Generic;
using System.Text;

namespace ArrowSharp.Core
{
    public sealed class Unit : IEquatable<Unit>
    {
        static Unit()
        {
            Value = new Unit();
        }
        public static Unit Value { get; private set; }
        private Unit() { }
        public override string ToString() => nameof(Unit);
        public override int GetHashCode() => 1;
        public override bool Equals(object obj) => obj is Unit;
        public bool Equals(Unit other) => true;
        public static bool operator ==(Unit one, Unit two) => true;
        public static bool operator !=(Unit one, Unit two) => false;
    } 
}
