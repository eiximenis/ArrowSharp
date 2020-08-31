using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ArrowSharp.Core.Tests
{
    public class SequenceTests
    {

        [Fact]
        public void Given_An_Empty_Sequence_Then_Count_Shoud_Return_Zero()
        {
            Sequence.Of(new int[] { }).Count.Should().Be(0);
        }

        [Fact]
        public void Given_An_Empty_Sequence_Then_IsEmpty_Shoud_Return_True()
        {
            Sequence.Of(new int[] { }).IsEmpty.Should().BeTrue();
        }


        [Fact]
        public void Given_A_Null_Value_Then_Of_Should_Create_An_Empty_Sequence()
        {
            string value = null;
            Sequence.Of(value).IsEmpty.Should().BeTrue();
        }

        [Fact]
        public void Given_An_Array_Of_NonNull_Items_Then_Of_Should_Create_Sequence_With_Same_Number_Of_Items()
        {
            var arr = new[] { 1, 2, 3, 4, 5 };
            Sequence.Of(arr).Count.Should().Be(arr.Length);
        }

        [Fact]
        public void Given_An_Array_With_Null_Values_Then_Of_Should_Create_Sequence_Without_Null_Values()
        {
            var arr = new[] { "one", null, "three", "four", null };
            Sequence.Of(arr).Count.Should().Be(arr.Length - 2);
        }

        [Fact]
        public void Given_An_Array_With_Units_Then_Of_Should_Create_An_Empty_Sequence()
        {
            var arr = new[] { Unit.Value, Unit.Value, Unit.Value };
            Sequence.Of(arr).IsEmpty.Should().BeTrue();
        }

        [Fact]
        public void Given_An_Array_With_Units_Then_Of_Should_Create_Sequence_Without_Units()
        {
            int f1() => 42;
            Unit f2() => Unit.Value;
            int f3() => 60;
            var seq = Sequence.Of(Option.Some(f1()), Option.Some<int>(f2()), Option.Some(f3()));
            seq.Count.Should().Be(2);
        }
    }
}
