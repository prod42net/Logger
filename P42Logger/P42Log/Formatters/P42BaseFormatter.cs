using P42Log.Interfaces;

namespace P42Log;
public class P42BaseFormatter : IP42Formatter
{
    public string Format(string logLevel, string text)
    {
        return text.Trim();
    }
}