using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;
using FluentAssertions.Common;
using System.Linq;
using System.Globalization;
using FluentAssertions.Specialized;

namespace ArrowSharp.Core.Tests
{
    
    public class OptionTests
    {
        [Fact]
        public void Given_Empty_Option_Then_GetOrElse_Should_Return_ElseValue()
        {
            var elseValue = "No value";
            Option.None<string>().GetOrElse(elseValue).Should().BeSameAs(elseValue);
        }

        [Fact]
        public void Given_Option_Created_Using_Null_Value_Then_It_Should_Be_Empty_Option()
        {
            Option.Some((string)null).IsNone.Should().BeTrue();
        }

        [Fact]
        public void Given_Empty_Option_Then_Fold_Should_Return_ElseValue()
        {
            var elseValue = "No value";
            Option.None<string>().Fold(elseValue, s => s.ToUpper()).Should().BeSameAs(elseValue);
        }

        [Fact]
        public void Given_EmptyOption_Then_Map_Should_Return_EmptyOption()
        {
            Option.None<string>().Map(s => s.ToUpper()).IsNone.Should().BeTrue();
        }

        [Fact]
        public void Given_EmptyOption_Then_AsEnumerable_Should_Return_Empty_Enumerable()
        {
            Option.None<int>().ToEnumerable().Any().Should().BeFalse();
        }

        [Fact]
        public void Given_Two_EmptyOptions_Then_Should_Be_Equal()
        {
            Option.None<int>().Equals(Option.None<int>()).Should().BeTrue();
        }

        [Fact]
        public void Given_EmptyOption_Then_Should_Not_Equals_To_Null()
        {
            Option.None<string>().Equals(null).Should().BeFalse();
        }

        [Fact]
        public void Given_SomeOption_Then_Map_Shoud_Return_SomeOption_With_Transformed_Value()
        {
            Option.Some(10).Map(i => i * 2).GetOrElse(0).Should().Be(20);
        }

        [Fact]
        public void Given_SomeOption_Then_Map_Shoud_Return_SomeOption_With_Transformed_Value_And_Type()
        {
            Option.Some(10).Map(i => i.ToString(CultureInfo.InvariantCulture)).GetOrElse("").Should().Be("10");
        }


        [Fact]
        public void Given_SomeOption_Then_AsEnumerable_Should_Return_Enumerable_With_Value()
        {
            var value = 10;
            Option.Some<int>(value).ToEnumerable().Single().Should().Be(value);
        }

        [Fact]
        public void Given_SomeOption_Used_To_Build_An_Option_Then_The_New_Option_Should_Be_An_Equal_Some()
        {
            Option<int> Maybe5() { return Option.Some(5); }

            var option1 = Maybe5();
            var option2 = Option.Some(option1);
            option2.Should().Be(option2);
        }

        [Fact]
        public void Given_SomeNone_Used_To_Build_An_Option_Then_The_New_Option_Should_Be_An_Equal_None()
        {
            var none1 = Option.None<int>();
            var none2 = Option.Some(none1);
            none1.Should().Be(none2);
        }

        [Fact]
        public void Given_RightEither_Then_FromEither_Should_Return_A_Some_With_Either_Right_Value()
        {
            var value = "Ok";
            var either = Either.Right<int, string>(value);
            Option.FromEither(either).GetOrElse("").Should().Be(value);
        }

        [Fact]
        public void Given_LeftEither_Then_FromEither_Should_Return_A_None()
        {
            var value = 10;
            var either = Either.Left<int, string>(value);
            Option.FromEither(either).IsNone.Should().BeTrue();
        }

        [Fact]
        public void Given_SomeNone_Then_FlatMap_Should_Return_A_None()
        {
            Option.None<int>().FlatMap(i => Option.Some(i)).IsNone.Should().BeTrue();
        }

        [Fact]
        public void Given_SomeOption_Then_FlatMap_Should_Return_A_New_SomeOption_With_New_Value()
        {
            var defaultValue = 0;
            Option.Some(100).FlatMap(i => Option.Some(i * 2)).GetOrElse(defaultValue).Should().Be(200);
        }

        [Fact]
        public void Given_SomeOption_Then_FlatMap_Should_Return_A_None_If_Mapper_Returns_A_None()
        {
            Option.Some(100).FlatMap(i => Option.None<int>()).IsNone.Should().BeTrue();
        }

        [Fact]
        public void Given_An_Id_Then_Option_Some_Should_Return_Unwrapped_Option()
        {
            var value = "somevalue";
            var id = Id.Just(value);
            Option.Some(id).GetOrElse("").Should().Be(value);
        }

        [Fact]
        public void Given_A_Unit_Then_Option_Some_Should_Return_A_None()
        {
            Option.Some(Unit.Value).IsNone.Should().BeTrue();
        }

        [Fact]
        public void Given_A_Unit_Then_Option_Some_Should_Return_A_None_For_Any_Type()
        {
            Option.Some<int>(Unit.Value).IsNone.Should().BeTrue();
        }

    }
}
