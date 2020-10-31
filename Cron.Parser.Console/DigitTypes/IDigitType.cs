namespace Cron.Parser.Console.DigitTypes
{
    public interface IDigitType
    {
        int MinValue { get; }
        int MaxValue { get; }
        string Print(string cronText);
    }
}