using System.Diagnostics.CodeAnalysis;

namespace Cron.Parser.Console.DigitTypes
{
    [ExcludeFromCodeCoverage]
    public class MonthType : BaseType
    {
        public override int MinValue => 1;
        public override int MaxValue => 12;
        public override string DigitType => "month";
    }
}