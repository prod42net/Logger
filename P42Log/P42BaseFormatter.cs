using P42Log.Interfaces;

namespace P42Log;
public class P42BaseFormatter : IP42Formatter
{
    public virtual string Format(string text)
    {
        return text.Trim();
    }
}