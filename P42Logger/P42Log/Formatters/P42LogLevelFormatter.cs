using P42Log.Interfaces;

namespace P42Log;
public class P42LogLevelFormatter : IP42Formatter
{
    public string Format(string logLevel, string text)
    {
        return $"[LL:{logLevel.ToString()}] {text}";
    }
}