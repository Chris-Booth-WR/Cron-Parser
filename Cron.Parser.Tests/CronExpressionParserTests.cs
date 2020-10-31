using System.Text;
using Cron.Parser.Console;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Cron.Parser.Tests
{
    public class CronExpressionParserTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public CronExpressionParserTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [InlineData("*/15 * * * *")]
        [InlineData("10/15 * * * *")]
        [InlineData("10-20/15 * * * *")]
        [InlineData("*/15 0 1,15 * 1-5")]
        [Theory]
        public void TestValidCrons(string content)
        {
            var textWriter = new TestTextWriter(_testOutputHelper);
            var cronExpressionParser = new CronExpressionParser(content, textWriter);
            cronExpressionParser.PrintExpression();
            textWriter.GetContent().ShouldNotBeEmpty();
        }

        private class TestTextWriter : ITextWriter
        {
            private readonly ITestOutputHelper _testOutputHelper;
            private readonly StringBuilder _builder;

            public TestTextWriter(ITestOutputHelper testOutputHelper)
            {
                _testOutputHelper = testOutputHelper;
                _builder = new StringBuilder();
            }

            public void WriteLine(string content)
            {
                _testOutputHelper.WriteLine(content);
                _builder.AppendLine(content);
            }

            public string GetContent()
            {
                return _builder.ToString();
            }
        }
    }
}