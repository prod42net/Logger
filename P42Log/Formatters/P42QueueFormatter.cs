using P42Log.Interfaces;

namespace P42Log;

public class P42QueueFormatter : IP42Formatter
{
    string _queueName ;
    public P42QueueFormatter(string queueName)
    {
        _queueName = queueName;
    }
    public string Format(string logLevel, string text)
    {
        return $"[Q:{_queueName.ToString()}] {text}";
    }
}