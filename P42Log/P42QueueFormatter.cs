using P42Log.Interfaces;

namespace P42Log;

public class P42QueueFormatter : P42BaseFormatter
{
    string _queueName = "";
    public P42QueueFormatter(string queueName)
    {
        _queueName = queueName;
    }
    public override string Format(string text)
    {
        // I call it here - it's not necessary but I wanna get all the formatting 
        text = base.Format(text);
        return $"[{_queueName.ToString()}] {text}";
    }
}