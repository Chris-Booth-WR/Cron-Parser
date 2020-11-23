using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Cron.Parser.Console.DigitTypes
{
    [ExcludeFromCodeCoverage]
    public class DayWeekType : BaseType
    {
        public override int MinValue => 0;
        public override int MaxValue => 6;
        public override string DigitType => "dayofweek";

        public Dictionary<string, int> ValidDays => new Dictionary<string, int>
        {
            ["Sun"] = 1,
            ["Mon"] = 2,
            ["Tue"] = 3,
            ["Wed"] = 4, 
            ["Thu"] = 4, 
            ["Fri"] = 5, 
            ["Sat"] = 6
        };
    }
}