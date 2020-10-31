using System.Diagnostics.CodeAnalysis;

namespace Cron.Parser.Console.DigitTypes
{
    [ExcludeFromCodeCoverage]
    public class DayMonthType : BaseType
    {
        public override int MinValue => 1;
        public override int MaxValue => 31;
        public override string DigitType => "day of month";
    }
}