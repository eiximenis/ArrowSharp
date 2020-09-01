using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ArrowSharp.Core.Extensions;
using System.Globalization;
using FluentAssertions;

namespace ArrowSharp.Core.Tests
{
    public class FuncTests
    {
        [Fact]
        public void Given_A_None_Then_FuncInvoke_Should_Return_Value_Specified()
        {
            var defaultValue = "no number entered";
            Func<int, string> str = i => i.ToString(CultureInfo.InvariantCulture);
            var none = Option.None<int>();
            str.Invoke(none, defaultValue).Should().Be(defaultValue);
        }
        [Fact]
        public void Given_A_Some_Then_FuncInvoke_Should_Return_Same_Value_Than_Func_Extended()
        {
            var defaultValue = "no number entered";
            var value = 42;
            Func<int, string> str = i => i.ToString(CultureInfo.InvariantCulture);
            var expectedValue = str.Invoke(value);
            var someValue = Option.Some(42);
            str.Invoke(someValue, defaultValue).Should().Be(expectedValue);
        }

        [Fact]
        public void Given_Two_Funcs_Then_Compose_Should_Return_Both_Functions_Composed()
        {
            Func<int, double> add = x => x * 2.0;
            Func<double, string> str = x => x.ToString(CultureInfo.InvariantCulture);

            var initialValue = 40;
            var expectedValue = str(add(initialValue));
            str.Compose(add).Invoke(initialValue).Should().Be(expectedValue);
        }

        [Fact]
        public void Given_Func_That_Returns_None_Then_FuncFold_Should_Return_Func_That_Returns_Specified_Value()
        {
            var defaultValue = 0.0;
            Func<int, double, Option<double>> divide = (x, y) => y != 0.0 ? Option.Some(x / y) : Option.None<double>();
            Func<int, double, double> divideF = divide.Fold(defaultValue);
            divideF(2, 0.0).Should().Be(defaultValue);
        }
    }
}
