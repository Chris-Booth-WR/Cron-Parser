using System;
using Cron.Parser.Console.DigitTypes;

namespace Cron.Parser.Console
{
    public class CronExpressionParser
    {
        private readonly ITextWriter _writer;
        private readonly string[] _parts;

        public CronExpressionParser(string cronExpression, ITextWriter writer)
        {
            _writer = writer;
            _parts = cronExpression.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        }

        public void PrintExpression()
        {
            for (var i = 0; i < _parts.Length; i++)
            {
                IDigitType digitType;
                switch (i)
                {
                    case 0:
                        digitType = new MinuteType();
                        break;
                    case 1:
                        digitType = new HourType();
                        break;
                    case 2:
                        digitType = new DayMonthType();
                        break;
                    case 3:
                        digitType = new MonthType();
                        break;
                    case 4:
                        digitType = new DayWeekType();
                        break;
                    default:
                        digitType = default;
                        break;
                }

                if (digitType == default)
                {
                    _writer.WriteLine($"{"command".PadRight(14)}{_parts[i]}");
                    continue;
                }

                var cronExpressionVisitor = new CronExpressionVisitor(_parts[i], digitType);
                if (cronExpressionVisitor.Validate())
                {
                   _writer.WriteLine(cronExpressionVisitor.Print());
                }
            }
        }
    }
}