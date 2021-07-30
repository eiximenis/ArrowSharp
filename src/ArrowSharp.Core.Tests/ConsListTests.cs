using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [Fact]
        public void Given_An_Empty_ConsList_Count_Should_Be_Zero()
        {
            Empty<int>().Count.Should().Be(0);
        }

        [Fact]
        public void Given_An_Empty_ConsList_As_IEnumerable_Then_The_IEnumerable_Count_Should_Be_Zero()
        {
            IEnumerable<int> empyCons = Empty<int>();
            empyCons.Count().Should().Be(0);
        }

        [Fact]
        public void Given_ConsList_As_IEnumerable_Then_The_IEnumerable_Should_Have_Same_Number_Of_Items()
        {
            IEnumerable<int> list = Cons(1, Cons(2, Cons(3, Empty<int>())));
            list.Count().Should().Be(3);
        }


        [Fact]
        public void Given_ConsList_As_IEnumerable_Then_The_First_Item_Should_Be_The_Head()
        {
            var list = Cons(1, Cons(2, Cons(3, Empty<int>())));
            list.First().Should().Be(list.Head);
        }

        [Fact]
        public void Given_ConsList_As_IEnumerable_Then_The_Last_Element_Should_Be_The_Last_Element_Of_ConsList()
        {
            var last = 3;
            var list = Cons(1, Cons(2, Cons(last, Empty<int>())));
            list.Last().Should().Be(last);
        }
    }
}
