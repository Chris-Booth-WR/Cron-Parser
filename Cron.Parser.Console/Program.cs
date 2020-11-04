using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Cron.Parser.Console
{
    [ExcludeFromCodeCoverage]
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var textWriter = new ConsoleTextWriter();
            var argument = args.FirstOrDefault();
            var cronExpressionParser = new CronExpressionParser(argument, textWriter);
            cronExpressionParser.PrintExpression();
        }

        private class ConsoleTextWriter : ITextWriter
        {
            public void WriteLine(string content)
            {
                System.Console.WriteLine(content);
            }

            public void ReadLine() => System.Console.ReadLine();
        }
    }
}