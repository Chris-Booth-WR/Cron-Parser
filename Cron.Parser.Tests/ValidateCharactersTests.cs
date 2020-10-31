using System.Linq;
using System.Reflection;
using Cron.Parser.Console;
using Shouldly;
using Xunit;

namespace Cron.Parser.Tests
{
    public class ValidateCharacters
    {
        private readonly char[] _fields;

        public ValidateCharacters()
        {
            _fields = typeof(CronAllowedCharacters).GetFields(
                    BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly)
                .Select(fi =>
                {
                    var value = fi.GetValue(null);
                    if (value is char @char) return @char;
                    return null as char?;
                }).Where(item => item.HasValue).Select(item => item.Value).ToArray();
        }


        [InlineData('0')]
        [InlineData('1')]
        [InlineData('2')]
        [InlineData('3')]
        [InlineData('4')]
        [InlineData('5')]
        [InlineData('6')]
        [InlineData('7')]
        [InlineData('8')]
        [InlineData('9')]
        [InlineData('*')]
        [InlineData(',')]
        [InlineData('-')]
        [InlineData('/')]
        [Theory]
        public void EnsureAllCharsAreValid(char character)
        {
            _fields.ShouldContain(character);
        }

        [InlineData('a')]
        [InlineData('b')]
        [InlineData('c')]
        [InlineData('d')]
        [InlineData('e')]
        [InlineData('f')]
        [InlineData('g')]
        [InlineData('h')]
        [InlineData('i')]
        [InlineData('j')]
        [InlineData('k')]
        [InlineData('l')]
        [InlineData('m')]
        [InlineData('n')]
        [Theory]
        public void EnsureAllCharsAreInvalid(char character)
        {
            _fields.ShouldNotContain(character);
        }
    }
}