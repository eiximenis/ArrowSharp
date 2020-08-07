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
        public void Given_LeftEither_Then_Fold_Applies_Left_Folder_Function()
        {
            Either.Left<int, int>(10).Fold(i => i * 2, s => 0).Should().Be(20);
        }

        [Fact]
        public void Given_RightEither_Then_Fold_Applies_Right_Folder_Function()
        {
            Either.Right<int, int>(10).Fold(i => 0, s => s * 2).Should().Be(20);
        }

        [Fact]
        public void Given_LeftEither_Then_Swap_Should_Return_RightEither()
        {
            Either.Left<int, string>(10).Swap().IsRight.Should().BeTrue();
        }

        [Fact]
        public void Given_LeftEither_Then_Swap_Should_Return_RightEither_With_Previous_Left_Value()
        {
            var defaultValue = -1;
            Either.Left<int, string>(10).Swap().Right.GetOrElse(defaultValue).Should().Be(10);
        }

        [Fact]
        public void Given_RightEither_Then_Swap_Should_Return_LeftEither()
        {
            Either.Right<int, string>("Message").Swap().IsLeft.Should().BeTrue();
        }

        [Fact]
        public void Given_RightEither_Then_Swap_Should_Return_LeftEither_With_Previous_Right_Value()
        {
            var defaultValue = "";
            var message = "Message";
            Either.Right<int, string>(message).Swap().Left.GetOrElse(defaultValue).Should().Be(message);
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

        [Fact]
        public void Given_LeftEither_Then_Catch_Should_Invoke_The_Catcher_Action_With_Left_Value()
        {
            int holder = 0;
            Either.Left<int, string>(10).Catch(v => holder = v);
            holder.Should().Be(10);
        }

        [Fact]
        public void Given_RightEither_Then_Catch_Should_Do_Nothing()
        {
            int holder = 0;
            Either.Right<int, string>("Ok").Catch(v => holder = v);
            holder.Should().Be(0);
        }

        [Fact]
        public void Given_RightEither_Then_Map_Should_Return_A_New_Right_Either_With_Value_Mapped()
        {
            var defaultValue = -1;
            Either.Right<int, double>(10.5).Map(r => (int)r).GetOrElse(defaultValue).Should().Be(10);
        }

        [Fact]
        public void Given_LeftEither_Then_Map_Should_Return_A_New_LeftEither()
        {
            Either.Left<int, double>(10).Map(r => (int)r).IsLeft.Should().BeTrue();
        }
        [Fact]
        public void Given_LeftEither_Then_Map_Should_Return_A_New_LeftEither_With_Same_Value()
        {
            var defaultValue = -1;
            Either.Left<int, double>(10).Map(r => (int)r).Left.GetOrElse(defaultValue).Should().Be(10);
        }

        [Fact]
        public void Given_LeftEither_Then_MapLeft_Should_Return_A_New_LeftEither()
        {
            Either.Left<int, double>(10).MapLeft(l => l * 2).IsLeft.Should().BeTrue();
        }
        
        [Fact]
        public void Given_LeftEither_Then_MapLeft_Should_Return_A_New_LeftEither_With_New_Value()
        {
            var defaultValue = -1;
            Either.Left<int, double>(10).MapLeft(l => l * 2).Left.GetOrElse(defaultValue).Should().Be(20);
        }

        [Fact]
        public void Given_RightEither_Then_MapLeft_Should_Return_A_New_RightEither_With_Same_Value()
        {
            var defaultValue = 0.5;
            Either.Right<int, double>(10.5).MapLeft(l => l * 2).GetOrElse(defaultValue).Should().Be(10.5);
        }

        [Fact]
        public void Given_False_Predicate_Then_Cond_Should_Return_A_LeftEither()
        {
            Either.Cond(() => false, () => 10, () => 20).IsLeft.Should().BeTrue();
        }

        [Fact]
        public void Given_False_Predicate_Then_Cond_Should_Return_A_LeftEither_With_The_Value_Generated()
        {
            var defaultValue = -1;
            Either.Cond(() => false, () => 10, () => 20).Left.GetOrElse(defaultValue).Should().Be(10);
        }

        [Fact]
        public void Given_True_Predicate_Then_Cond_Should_Return_A_RightEither()
        {
            Either.Cond(() => true, () => 10, () => 20).IsRight.Should().BeTrue();
        }

        [Fact]
        public void Given_True_Predicate_Then_Cond_Should_Return_A_RightEither_With_The_Value_Generated()
        {
            var defaultValue = -1;
            Either.Cond(() => true, () => 10, () => 20).Right.GetOrElse(defaultValue).Should().Be(20);
        }



    }
}
