using System.Diagnostics.CodeAnalysis;

namespace Cron.Parser.Console.DigitTypes
{
    [ExcludeFromCodeCoverage]
    public class HourType : BaseType
    {
        public override int MinValue => 0;
        public override int MaxValue => 23;
        public override string DigitType => "hour";
    }
}