using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ArrowSharp.Core.Tests
{
    public class UnitTests
    {
        [Fact]
        public void Given_Two_Functions_That_Return_Unit_Then_Both_Results_Should_Be_Equal()
        {
            Unit f1() => Unit.Value;
            Unit f2() => Unit.Value;

            var equal = f1() == f2();
            equal.Should().BeTrue();
        }

    }
}
