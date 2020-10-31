using System.Diagnostics.CodeAnalysis;

namespace Cron.Parser.Console.DigitTypes
{
    [ExcludeFromCodeCoverage]
    public class MinuteType : BaseType
    {
        public override int MinValue => 0;
        public override int MaxValue => 59;
        public override string DigitType => "minutes";
    }
}