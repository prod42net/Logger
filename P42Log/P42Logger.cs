using P42Log.Interfaces;
using P42Log;

namespace P42Log;
public class P42Logger : IP42Logger
{
    List<IP42LogQueue> _queues;

    public P42Logger(bool useDefaultQueue = true)
    {
        _queues = new List<IP42LogQueue>();
        if (useDefaultQueue) AddLogQueue(new P42NamedQueue(""));
    }

    public void Log(string logLevel, string text)
    {
        foreach (IP42LogQueue queue in _queues)
        {
            queue.Write(logLevel, text);
        }
    }

    public void Log(string text)
    {
        Log("",text);
    }

    public void AddLogQueue(IP42LogQueue logQueue)
    {
        _queues.Add(logQueue);
    }
    
    public void SetLogLevel(string logLevel)
    {
        foreach (IP42LogQueue queue in _queues)
        {
            queue.SetLogLevel(logLevel);
        }
    }
}