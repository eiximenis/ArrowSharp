using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Xunit;

namespace ArrowSharp.Core.Tests
{
    public class IorTests
    {
        [Fact]
        public void Given_LeftIor_Then_Map_Should_Return_A_New_LeftIor()
        {
            Ior.Left<int, int>(100).Map(i => i * 2).IsLeft.Should().BeTrue();
        }

        [Fact]
        public void Given_RightIor_Then_Map_Should_Return_A_New_RightIor()
        {
            Ior.Right<int, int>(100).Map(i => i * 2).IsRight.Should().BeTrue();
        }

        [Fact]
        public void Given_BothIor_Then_Map_Should_Return_A_New_BothIor()
        {
            Ior.Both(100, 100).Map(i => i * 2).IsBoth.Should().BeTrue();
        }

        [Fact]
        public void Given_LeftIor_Then_Map_Should_Return_A_New_LeftIor_With_Same_Value()
        {
            var value = 100;
            var defaultValue = -1;
            Ior.Left<int, int>(value).Map(i => i * 2).Left.GetOrElse(defaultValue).Should().Be(value);
        }


        [Fact]
        public void Given_RightIor_Then_Map_Should_Return_A_New_RightIor_With_Transformed_Value()
        {
            var value = 100;
            var transformedValue = value * 2;
            var defaultValue = -1;
            Ior.Right<int, int>(value).Map(i => i * 2).Right.GetOrElse(defaultValue).Should().Be(transformedValue);
        }


        [Fact]
        public void Given_BothIor_Then_Map_Should_Return_A_New_BothIor_With_Transformed_Right_Value()
        {
            var value = 100;
            var transformedValue = value * 2;
            var defaultValue = -1;
            Ior.Both(value, value).Map(i => i * 2).Right.GetOrElse(defaultValue).Should().Be(transformedValue);
        }

        [Fact]
        public void Given_BothIor_Then_Map_Should_Return_A_New_BothIor_With_Same_Left_Value()
        {
            var value = 100;
            var defaultValue = -1;
            Ior.Both(value, value).Map(i => i * 2).Left.GetOrElse(defaultValue).Should().Be(value);
        }

        [Fact]
        public void Given_LeftIor_Then_MapLeft_Should_Return_A_New_LeftIor()
        {
            Ior.Left<int, int>(100).MapLeft(i => i * 2).IsLeft.Should().BeTrue();
        }

        [Fact]
        public void Given_RightIor_Then_MapLeft_Should_Return_A_New_RightIor()
        {
            Ior.Right<int, int>(100).MapLeft(i => i * 2).IsRight.Should().BeTrue();
        }

        [Fact]

        public void Given_BothIor_Then_MapLeft_Should_Return_A_New_BothIor()
        {
            Ior.Both(100, 100).MapLeft(i => i * 2).IsBoth.Should().BeTrue();
        }


        [Fact]
        public void Given_LeftIor_Then_MapLeft_Should_Return_A_New_LeftIor_With_Transformed_Value()
        {
            var value = 100;
            var transformedValue = value * 2;
            var defaultValue = -1;
            Ior.Left<int, int>(value).MapLeft(i => i * 2).Left.GetOrElse(defaultValue).Should().Be(transformedValue);
        }

        [Fact]
        public void Given_RightIor_Then_MapLeft_Should_Return_A_New_RightIor_With_Same_Value()
        {
            var value = 100;
            var defaultValue = -1;
            Ior.Right<int, int>(value).MapLeft(i => i * 2).Right.GetOrElse(defaultValue).Should().Be(value);
        }

        [Fact]
        public void Given_BothIor_Then_MapLeft_Should_Return_A_New_BothIor_With_Transformed_Left_Value()
        {
            var value = 100;
            var transformedValue = value * 2;
            var defaultValue = -1;
            Ior.Both(value, value).MapLeft(i => i * 2).Left.GetOrElse(defaultValue).Should().Be(transformedValue);
        }

        [Fact]
        public void Given_BothIor_Then_MapLeft_Should_Return_A_New_BothIor_With_Same_Right_Value()
        {
            var value = 100;
            var defaultValue = -1;
            Ior.Both(value, value).MapLeft(i => i * 2).Right.GetOrElse(defaultValue).Should().Be(value);
        }

        [Fact]
        public void Given_LeftIor_Then_Bimap_Should_Return_A_New_LeftIor()
        {
            Ior.Left<int, int>(100).Bimap(i => i * 2, i => i * 4).IsLeft.Should().BeTrue();
        }

        [Fact]
        public void Given_RightIor_Then_Bimap_Should_Return_A_New_RightIor()
        {
            Ior.Right<int, int>(100).Bimap(i => i * 2, i => i * 4).IsRight.Should().BeTrue();
        }

        [Fact]
        public void Given_BothIor_Then_Bimap_Should_Return_A_New_BothIor()
        {
            Ior.Both(100, 100).Bimap(i => i * 2, i => i * 4).IsBoth.Should().BeTrue();
        }

        [Fact]
        public void Given_Left_Ior_Then_Bimap_Should_Return_A_New_LeftIor_With_Transformed_Value()
        {
            var value = 100;
            var transformedValue = value * 2;
            var defaultValue = -1;
            Ior.Left<int, int>(value).Bimap(i => i * 2, i => i * 4).Left.GetOrElse(defaultValue).Should().Be(transformedValue);
        }

        [Fact]
        public void Given_Right_Ior_Then_Bimap_Should_Return_A_New_RightIor_With_Transformed_Value()
        {
            var value = 100;
            var transformedValue = value * 4;
            var defaultValue = -1;
            Ior.Right<int, int>(value).Bimap(i => i * 2, i => i * 4).Right.GetOrElse(defaultValue).Should().Be(transformedValue);
        }

        [Fact]
        public void Given_Both_Ior_Then_Bimap_Should_Return_A_New_BothIor_With_Transformed_Left_Value()
        {
            var value = 100;
            var transformedValue = value * 2;
            var defaultValue = -1;
            Ior.Both(value, value).Bimap(i => i * 2, i => i * 4).Left.GetOrElse(defaultValue).Should().Be(transformedValue);
        }

        [Fact]
        public void Given_Both_Ior_Then_Bimap_Should_Return_A_New_BothIor_With_Transformed_Right_Value()
        {
            var value = 100;
            var transformedValue = value * 4;
            var defaultValue = -1;
            Ior.Both(value, value).Bimap(i => i * 2, i => i * 4).Right.GetOrElse(defaultValue).Should().Be(transformedValue);
        }

        [Fact]
        public void Given_Left_Ior_Then_ToOption_Should_Return_A_None()
        {
            Ior.Left<int, int>(100).ToOption().IsNone.Should().BeTrue();
        }

        [Fact]
        public void Given_Right_Ior_Then_ToOption_Should_Return_A_Some_With_The_Value()
        {
            var defaultValue = -1;
            var value = 100;
            Ior.Right<int, int>(value).ToOption().GetOrElse(defaultValue).Should().Be(value);
        }

        [Fact]
        public void Given_Both_Ior_Then_ToOption_Should_Return_A_Some_With_The_Right_Value()
        {
            var defaultValue = -1;
            var lvalue = 100;
            var rvalue = 200;
            Ior.Both(lvalue, rvalue).ToOption().GetOrElse(defaultValue).Should().Be(rvalue);
        }

        [Fact]
        public void Given_Right_Ior_Then_ToLeftOption_Should_Return_A_None()
        {
            Ior.Right<int, int>(100).ToLeftOption().IsNone.Should().BeTrue();
        }

        [Fact]
        public void Given_Left_Ior_Then_ToLeftOption_Should_Return_A_Some_With_The_Value()
        {
            var defaultValue = -1;
            var value = 100;
            Ior.Left<int, int>(value).ToLeftOption().GetOrElse(defaultValue).Should().Be(value);
        }

        [Fact]
        public void Given_Both_Ior_Then_ToLeftOption_Should_Return_A_Some_With_The_Left_Value()
        {
            var defaultValue = -1;
            var lvalue = 100;
            var rvalue = 200;
            Ior.Both(lvalue, rvalue).ToLeftOption().GetOrElse(defaultValue).Should().Be(lvalue);
        }

        [Fact]
        public void Given_Left_Ior_Then_ToEither_Should_Return_A_Left_Either()
        {
            Ior.Left<int, int>(100).ToEither().IsLeft.Should().BeTrue();
        }

        [Fact]
        public void Given_Left_Ior_Then_ToEither_Should_Return_A_Left_Either_With_The_Value()
        {
            var value = 100;
            var defaultValue = -1;
            Ior.Left<int, int>(value).ToEither().Left.GetOrElse(defaultValue).Should().Be(value);
        }

        [Fact]
        public void Given_Right_Ior_Then_ToEither_Should_Return_A_Right_Either()
        {
            Ior.Right<int, int>(100).ToEither().IsRight.Should().BeTrue();
        }

        [Fact]
        public void Given_Right_Ior_Then_ToEither_Should_Return_A_Right_Either_With_The_Value()
        {
            var value = 100;
            var defaultValue = -1;
            Ior.Right<int, int>(value).ToEither().Right.GetOrElse(defaultValue).Should().Be(value);
        }

        [Fact]
        public void Given_Both_Ior_Then_ToEither_Should_Return_A_Right_Either()
        {
            Ior.Both(100, 200).ToEither().IsRight.Should().BeTrue();
        }

        [Fact]
        public void Given_Both_Ior_Then_ToEither_Should_Return_A_Right_Either_With_The_Right_Value()
        {
            var lvalue = 100;
            var rvalue = 200;
            var defaultValue = -1;
            Ior.Both(lvalue, rvalue).ToEither().Right.GetOrElse(defaultValue).Should().Be(rvalue);
        }

        [Fact]
        public void Given_Left_Ior_Then_Deconstruct_Should_Return_A_Some_With_Left_Value_In_First_Position()
        {
            var value = 100;
            var defaultValue = -1;
            var ior = Ior.Left<int, int>(value);
            var (left, _) = ior;
            left.GetOrElse(defaultValue).Should().Be(value);
        }

        [Fact]
        public void Given_Left_Ior_Then_Deconstruct_Should_Return_A_None_In_Second_Position()
        {
            var value = 100;
            var defaultValue = -1;
            var ior = Ior.Left<int, int>(value);
            var (_, right) = ior;
            right.GetOrElse(defaultValue).Should().Be(defaultValue);
        }

        [Fact]
        public void Given_Right_Ior_Then_Deconstruct_Should_Return_A_None_In_First_Position()
        {
            var value = 100;
            var defaultValue = -1;
            var ior = Ior.Right<int, int>(value);
            var (left, _) = ior;
            left.GetOrElse(defaultValue).Should().Be(defaultValue);
        }

        [Fact]
        public void Given_Right_Ior_Then_Deconstruct_Should_Return_A_Some_With_Right_Value_In_Second_Position()
        {
            var value = 100;
            var defaultValue = -1;
            var ior = Ior.Right<int, int>(value);
            var (_, right) = ior;
            right.GetOrElse(defaultValue).Should().Be(value);
        }

        [Fact]
        public void Given_Both_Ior_Then_Deconstruct_Should_Return_A_Some_With_Left_Value_In_First_Position()
        {
            var lvalue = 100;
            var rvalue = 200;
            var defaultValue = -1;
            var ior = Ior.Both(lvalue, rvalue);
            var (left, _) = ior;
            left.GetOrElse(defaultValue).Should().Be(lvalue);
        }

        [Fact]
        public void Given_Both_Ior_Then_Deconstruct_Should_Return_A_Some_With_Right_Value_In_First_Position()
        {
            var lvalue = 100;
            var rvalue = 200;
            var defaultValue = -1;
            var ior = Ior.Both(lvalue, rvalue);
            var (_, right) = ior;
            right.GetOrElse(defaultValue).Should().Be(rvalue);
        }

    }
}
