using System;
using System.Linq;
using System.Text;

namespace Cron.Parser.Console.DigitTypes
{
    public abstract class BaseType : IDigitType
    {
        public abstract int MinValue { get; }
        public abstract int MaxValue { get; }
        public abstract string DigitType { get; }

        public string Print(string cronText)
        {
            var partBuilder = new StringBuilder(DigitType.PadRight(14));

            foreach (var part in cronText.Split(CronAllowedCharacters.Comma))
            {
                partBuilder.Append(BuildCommaPart(part));
            }

            return partBuilder.ToString().Trim();
        }

        private StringBuilder BuildCommaPart(string part)
        {
            //Next we look at the slash parts '/'
            var partBuilder = new StringBuilder();
            var slashParts = part.Split(CronAllowedCharacters.Slash);
            var firstPart = slashParts[0];

            if (this is DayWeekType dayOfTheWeek
                && dayOfTheWeek.ValidDays.Keys.Contains(part))
            {
                //Sun, Tue, Fri
                partBuilder.Append($"{dayOfTheWeek.ValidDays[part]} ");
            }
            else
            {
                int? i = null, j = null;
                if (this is DayWeekType dayOfTheWeekWithDash
                    && part.Contains("-")
                    && part.Split("-").All(item => dayOfTheWeekWithDash.ValidDays.Keys.Contains(item)))
                {
                    //Sun-Wed
                    var parts = part.Split("-");
                    partBuilder.Append($"{dayOfTheWeekWithDash.ValidDays[parts[0]]} ");
                    foreach (var currentDay in dayOfTheWeekWithDash.ValidDays)
                    {
                        if (currentDay.Key == parts[0])
                        {
                            i = currentDay.Value;
                        }

                        if (!string.Equals(currentDay.Key, parts[1], StringComparison.Ordinal)) continue;
                        
                        j = currentDay.Value;
                        break;
                    }
                }
                else if (firstPart.Length == 1 && firstPart[0] == CronAllowedCharacters.Star)
                {
                    i = MinValue;
                    j = MaxValue;
                }
                else if (slashParts.Length == 1 && int.TryParse(firstPart, out  var intValue))
                {
                    i = j = intValue;
                }
                else if (firstPart.Contains(CronAllowedCharacters.Dash))
                {
                    var timeParts = firstPart.Split(CronAllowedCharacters.Dash);
                    if (timeParts.Length != 2)
                    {
                        throw new FormatException($"Cron {DigitType} part not in correct format {part}");
                    }

                    i = int.Parse(timeParts[0]);
                    j = int.Parse(timeParts[1]);
                }
                else
                {
                    i = int.Parse(firstPart);
                    j = MaxValue;
                }

                if (slashParts.Length == 2)
                {
                    var offset = int.Parse(slashParts[1]);
                    while (i < j)
                    {
                        partBuilder.Append($"{i} ");
                        i += offset;
                    }

                    partBuilder.Remove(partBuilder.Length - 1, 1);
                }
                else if (i == j)
                {
                    partBuilder.Append($"{i} ");
                }
                else if(i.HasValue && j.HasValue)
                {
                    //Every minute i - 59
                    partBuilder.Append(string.Join(" ", Enumerable.Range(i.Value, j.Value - (i.Value - 1))));
                }
            }

            return partBuilder;
        }
    }
}