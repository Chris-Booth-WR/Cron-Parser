namespace Cron.Parser.Console.DigitTypes
{
    public class YearType : BaseType
    {
        public override int MinValue => 1900;
        public override int MaxValue => 2100;
        public override string DigitType => "year";
    }
}