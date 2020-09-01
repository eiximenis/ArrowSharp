using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using static ArrowSharp.Core.ConsList;

namespace ArrowSharp.Core.Tests
{
    public class ConsListTests
    {
        [Fact]
        public void Given_N_Chained_Calls_To_ConsListConst_Then_The_ConsList_Created_Should_Have_N_Items()
        {
            var list = Cons(1, Cons(2, Cons(3, Empty<int>())));
            list.Count.Should().Be(3);
        }
    }
}
