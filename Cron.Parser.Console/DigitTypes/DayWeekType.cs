using System.Diagnostics.CodeAnalysis;

namespace Cron.Parser.Console.DigitTypes
{
    [ExcludeFromCodeCoverage]
    public class DayWeekType : BaseType
    {
        public override int MinValue => 0;
        public override int MaxValue => 6;
        public override string DigitType => "dayofweek";
    }
}