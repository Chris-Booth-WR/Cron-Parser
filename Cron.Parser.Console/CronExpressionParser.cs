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
            _parts = cronExpression?.Split(' ', StringSplitOptions.RemoveEmptyEntries) ?? new string[0];
        }

        public void PrintExpression()
        {
            if (_parts.Length == 0)
            {
                _writer.WriteLine("Invalid arguments supplied");
                _writer.ReadLine();
                return;
            }
            for (var i = 0; i < _parts.Length; i++)
            {
                IDigitType digitType = i switch
                {
                    0 => new MinuteType(),
                    1 => new HourType(),
                    2 => new DayMonthType(),
                    3 => new MonthType(),
                    4 => new DayWeekType(),
                    _ => default
                };

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
                _writer.ReadLine();
            }
        }
    }
}