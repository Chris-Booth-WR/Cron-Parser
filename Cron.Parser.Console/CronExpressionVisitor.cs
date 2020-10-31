using System;
using System.Linq;
using Cron.Parser.Console.DigitTypes;

namespace Cron.Parser.Console
{
    public class CronExpressionVisitor
    {
        private readonly string _cronExpression;
        private readonly IDigitType _digitType;
        private bool? _valid;

        public CronExpressionVisitor(string cronExpression, IDigitType digitType)
        {
            _cronExpression = cronExpression;
            _digitType = digitType;
        }

        public bool Validate(string part = null)
        {
            if (string.IsNullOrWhiteSpace(part) && _valid.HasValue) return _valid.Value;
            part ??= _cronExpression;
            switch (part.Length)
            {
                case 0:
                    _valid = false;
                    break;
                case 1:
                case 2:
                    if (!int.TryParse(part, out var value))
                    {
                        _valid = string.Equals(part, CronAllowedCharacters.Star.ToString(), StringComparison.InvariantCultureIgnoreCase);
                        break;
                    }

                    _valid = value >= _digitType.MinValue && value <= _digitType.MaxValue;
                    break;
                default:

                    if (!part.Contains(CronAllowedCharacters.Comma)
                        && !part.Contains(CronAllowedCharacters.Dash)
                        && !part.Contains(CronAllowedCharacters.Slash))
                    {
                        _valid = false;
                        break;
                    }

                    string[] parts;
                    if (part.Contains(CronAllowedCharacters.Comma))
                    {
                        parts = part.Split(CronAllowedCharacters.Comma, StringSplitOptions.RemoveEmptyEntries);
                        _valid = parts.Length != 1 && parts.All(Validate);
                        break;
                    }

                    if (part.Contains(CronAllowedCharacters.Slash))
                    {
                        parts = part.Split(CronAllowedCharacters.Slash, StringSplitOptions.RemoveEmptyEntries);
                        _valid = parts.Length == 2 && Validate(parts[0]) && !parts[1].Contains(CronAllowedCharacters.Dash) && Validate(parts[1]);

                        break;
                    }

                    if (!part.Contains(CronAllowedCharacters.Dash)) return false;

                    parts = part.Split(CronAllowedCharacters.Dash, StringSplitOptions.RemoveEmptyEntries);
                    _valid = parts.Length == 2 && parts.All(Validate);
                    break;
            }

            return _valid.GetValueOrDefault();
        }

        public string Print()
        {
            if (!Validate()) throw new FormatException();

            return _digitType.Print(_cronExpression);
        }
    }
}