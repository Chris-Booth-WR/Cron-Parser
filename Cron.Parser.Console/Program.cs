using System.Diagnostics.CodeAnalysis;

namespace Cron.Parser.Console
{
    [ExcludeFromCodeCoverage]
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var argument = args[0];
            var cronExpressionParser = new CronExpressionParser(argument, new ConsoleTextWriter());
            cronExpressionParser.PrintExpression();
            System.Console.ReadLine();
        }
        
        private class ConsoleTextWriter : ITextWriter
        {
            public void WriteLine(string content)
            {
                System.Console.WriteLine(content);
            }
        }
    }
}