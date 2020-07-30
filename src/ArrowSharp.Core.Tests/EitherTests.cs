using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Xunit;

namespace ArrowSharp.Core.Tests
{
    public class EitherTests
    {
        [Fact]
        public void Given_EitherLeft_Called_Then_A_LeftEither_Is_Returned()
        {
            Either.Left<int, string>(10).IsLeft.Should().BeTrue();
        }

        [Fact]
        public void Given_EitherRight_Called_Then_A_LeftEither_Is_Returned()
        {
            Either.Right<int, string>("Ok").IsRight.Should().BeTrue();
        }

        [Fact]
        public void Given_LeftEither_Then_Right_Value_Is_None()
        {
            Either.Left<int, string>(10).Right.IsNone.Should().BeTrue();
        }

        [Fact]
        public void Given_RightEither_Then_Left_Value_Is_None()
        {
            Either.Right<int, string>("Ok").Left.IsNone.Should().BeTrue();
        }


        [Fact]
        public void Given_LeftEither_Then_Fold_Returns_Default_Value()
        {
            var defaultValue = "some value";
            Either.Left<int, string>(10).Fold(defaultValue, s => s.ToUpperInvariant()).Should().Be(defaultValue);
        }

        [Fact]
        public void Given_RightEither_Then_Fold_Transforms_Right_Value()
        {
            var original = "some string";
            var final = original.ToUpperInvariant();
            Either.Right<int, string>(original).Fold("", s => s.ToUpperInvariant()).Should().Be(final);
        }

        [Fact]
        public void Given_LeftEither_Then_Swap_Should_Return_RightEither()
        {
            Either.Left<int, string>(10).Swap().IsRight.Should().BeTrue();
        }

        [Fact]
        public void Given_RightEither_Then_Swap_Should_Return_LeftEither()
        {
            Either.Right<int, string>("Message").Swap().IsLeft.Should().BeTrue();
        }

        [Fact]
        public void Deconstructing_RightEither_Returns_A_None_At_First()
        {
            var (none, _) = Either.Right<int, string>("Ok");
            none.IsNone.Should().BeTrue();
        }

        [Fact]
        public void Deconstructing_LeftEither_Returns_A_Some_At_First()
        {
            var (left, _) = Either.Left<int, string>(10);
            left.IsNone.Should().BeFalse();
        }


        [Fact]
        public void Deconstructing_RightEither_Returns_A_Some_At_Second()
        {
            var (_, right) = Either.Right<int, string>("Ok");
            right.IsNone.Should().BeFalse();
        }

        [Fact]
        public void Deconstructing_LeftEither_Returns_A_None_At_Second()
        {
            var (_, right) = Either.Left<int, string>(10);
            right.IsNone.Should().BeTrue();
        }

        [Fact]
        public void Given_RightEither_Then_GetOrElse_Returns_Right_Value()
        {
            Either.Right<int, string>("Ok").GetOrElse("").Should().Be("Ok");
        }

        [Fact]
        public void Given_LeftEither_Then_GetOrElse_Returns_Passed_Value()
        {
            Either.Left<int, string>(10).GetOrElse("").Should().Be("");
        }

        [Fact]
        public void Given_RightEither_Then_GetOrHandle_Returns_Right_Value()
        {
            Either.Right<int, string>("Ok").GetOrHandle(i => i.ToString(CultureInfo.InvariantCulture)).Should().Be("Ok");
        }

        [Fact]
        public void Given_LeftEither_Then_GetOrHandle_Uses_Left_Value_And_The_Generator()
        {
            var expected = 10.ToString(CultureInfo.InvariantCulture);
            Either.Left<int, string>(10).GetOrHandle(i => i.ToString(CultureInfo.InvariantCulture)).Should().Be(expected);
        }

        [Fact]
        public void Given_RightEither_Then_ToOption_Returns_A_Some_With_Right_Value()
        {
            Either.Right<int, string>("Ok").ToOption().GetOrElse("").Should().Be("Ok");
        }

        [Fact]
        public void Given_LeftEither_Then_ToOption_Returns_A_None()
        {
            Either.Left<int, string>(10).ToOption().IsNone.Should().BeTrue();
        }

        [Fact]
        public void Given_RightEither_Then_ToEnumerable_Returns_Enumerable_With_Right_Value()
        {
            Either.Right<int, string>("Ok").ToEnumerable().Single().Should().Be("Ok");
        }

        [Fact]
        public void Given_LeftEither_Then_ToEnumerable_Returns_Empty_Enumerable()
        {
            Either.Left<int, string>(10).ToEnumerable().Any().Should().BeFalse();
        }

    }
}
