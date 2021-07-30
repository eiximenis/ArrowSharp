using System;
using System.Collections.Generic;
using System.Text;

namespace ArrowSharp.Core
{

    public static class Lens
    {
        public static Lens<Outer, Value> Compose<Outer, Inner, Value> (Lens<Outer, Inner> outer, Lens<Inner, Value> inner) => 
            new Lens<Outer, Value>(v => inner.Get(outer.Get(v)), (o, v) => outer.Set(o, inner.Set(outer.Get(o), v)));

        
    }

    public class Lens<Inner, Value>
    {
        private readonly Func<Inner, Value> _getter;
        private readonly Func<Inner, Value, Inner> _setter;

        public Lens(Func<Inner, Value> getter, Func<Inner, Value, Inner> setter)
        {
            _getter = getter;
            _setter = setter;
        }

        public Value Get(Inner inner) => _getter(inner);
        public Inner Set(Inner inner, Value value) => _setter(inner, value);

    }
}
