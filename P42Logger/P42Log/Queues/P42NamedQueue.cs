using P42Log.Interfaces;

namespace P42Log;

public class P42NamedQueue : P42BasicLogQueue
{
    public P42NamedQueue(string queueName,IP42Distributer? distributer = null):base()
    {
        Name = queueName;
        AddDistributer(distributer ?? new P42ConsoleDistributer());
        AddFormatter(new P42QueueFormatter(Name));
        AddFormatter(new P42LogLevelFormatter());
        AddFormatter(new IP42DateTimeFormatter(false,true,true,true));
    }
}