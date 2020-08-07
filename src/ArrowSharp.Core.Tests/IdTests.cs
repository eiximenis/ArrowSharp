using FluentAssertions;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ArrowSharp.Core.Tests
{
    public class IdTests
    {
        [Fact]
        public void Given_An_Id_Then_Extract_Should_Return_Its_Value()
        {
            var value = 200;
            Id.Just(value).Extract().Should().Be(value);
        }

        [Fact]
        public void Given_A_Null_Id_Then_ToOption_Should_Return_None()
        {
            Id.Just((string)null).ToOption().IsNone.Should().BeTrue();
        }

        [Fact]
        public void Given_An_Id_Built_From_Another_Id_Then_It_Should_Be_Unwrapped()
        {
            var id = Id.Just(100);
            Id.Just(id).Extract().Should().Be(100);
        }

        [Fact]
        public void Given_An_Id_Then_Map_Should_Return_Another_Id_With_Mapped_Value()
        {
            Id.Just(100).Map(i => i * 2).Extract().Should().Be(200);
        }

        [Fact]
        public void Given_An_Id_Then_FlatMap_Should_Return_Another_Id_With_Mapped_Value_Unwrapped()
        {
            Id.Just(100).FlatMap(i => Id.Just(i * 2)).Extract().Should().Be(200);
        }

        [Fact]
        public void Given_A_Null_Id_Then_FlatMap_Should_Return_Another_Id_With_Default_Value()
        {
            string value = null;
            int defaultIntValue = default;
            Id.Just(value).FlatMap(s => Id.Just(s.Length)).Extract().Should().Be(defaultIntValue);
        }

        [Fact]
        public void Given_An_Id_Then_ToEnumerable_Should_Return_An_Enumerable_With_Single_Same_Value()
        {
            var value = 100;
            Id.Just(value).ToEnumerable().Single().Should().Be(value);
        }

        [Fact]
        public void Given_A_Null_Id_Then_ToEnumerable_Should_Return_An_Enumerable_With_Single_Null_Value()
        {
            string value = null;
            Id.Just(value).ToEnumerable().Single().Should().Be(value);
        }

        [Fact]
        public void Given_A_Null_Id_Then_Should_Be_Equal_To_Another_Null_Id_Of_The_Same_Type()
        {
            string value = null;
            string value2 = null;
            Id.Just(value).Equals(Id.Just(value2)).Should().BeTrue();
        }


        [Fact]
        public void Given_A_Null_Id_Then_Should_Be_Equal_To_Null()
        {
            string value = null;
            Id.Just(value).Equals(null).Should().BeTrue();
        }

        [Fact]
        public void Given_A_Null_Id_Then_Should_Be_Equal_To_Another_Null_Id_Of_The_Same_Type_Using_Operator()
        {
            string value = null;
            string value2 = null;
            (Id.Just(value) == Id.Just(value2)).Should().BeTrue();
        }

        [Fact]
        public void Given_An_Id_Then_Should_Be_Equal_To_Another_Id_With_Same_Value()
        {
            var value = "somevalue";
            Id.Just(value).Equals(Id.Just(value)).Should().BeTrue();
        }

        [Fact]
        public void Given_An_Id_Then_Should_Be_Equal_To_Another_Id_With_Same_Value_Using_Operator()
        {
            var value = "somevalue";
            (Id.Just(value)== Id.Just(value)).Should().BeTrue();
        }

        [Fact]
        public void Given_A_Some_Then_Just_Should_Return_An_Id_With_Value_Unwrapped()
        {
            var value = 200;
            Id.Just(Option.Some(value)).Extract().Should().Be(value);
        }

        [Fact]
        public void Given_A_LeftEither_Then_Just_Should_Return_An_Id_With_DefaultValue_Of_Type()
        {
            int defaultValue = default;
            Id.Just(Either.Left<int, int>(100)).Extract().Should().Be(defaultValue);
        }

        [Fact]
        public void Given_A_RightEither_Then_Just_Should_Return_An_Id_With_Value_Unwrapped()
        {
            var value = 200;
            Id.Just(Either.Right<int, int>(value)).Extract().Should().Be(value);
        }

    }
}
