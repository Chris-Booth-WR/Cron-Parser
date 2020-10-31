using System;
using Cron.Parser.Console;
using Cron.Parser.Console.DigitTypes;
using NSubstitute;
using Shouldly;
using Xunit;

namespace Cron.Parser.Tests
{
    public class CronExpressionVisitorTests
    {
        [InlineData("1-2/3-4,5-6/7-8")]
        [InlineData("1-2-3-4,5-6-7-8")]
        [InlineData("12")]
        [InlineData("*2")]
        [InlineData("ab")]
        [InlineData("a")]
        [Theory]
        public void IsInvalidCharacterStatement(string invalidString)
        {
            var digitType = Substitute.For<BaseType>();
            digitType.MinValue.Returns(0);
            digitType.MaxValue.Returns(9);
            var cronExpressionVisitor = new CronExpressionVisitor(invalidString, digitType);
            cronExpressionVisitor.Validate().ShouldBeFalse();
            Assert.Throws<FormatException>(() => cronExpressionVisitor.Print());
        }
        
        [InlineData("1-2/3,4-5/6,7-8/12")]
        [InlineData("1-2/3,4-5/6,7-8/9")]
        [InlineData("1-2/3,4-5/6")]
        [InlineData("1/2,3/4")]
        [InlineData("1,2")]
        [InlineData("1")]
        [InlineData("*")]
        [InlineData("*/5")]
        [Theory]
        public void IsValidCharacterStatement(string validCron)
        {
            var digitType = Substitute.For<BaseType>();
            digitType.MinValue.Returns(0);
            digitType.MaxValue.Returns(12);
            var cronExpressionVisitor = new CronExpressionVisitor(validCron, digitType);
            cronExpressionVisitor.Validate().ShouldBeTrue();
            cronExpressionVisitor.Print().ShouldNotBeEmpty();
        }
    }
}