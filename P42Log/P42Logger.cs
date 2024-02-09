using P42Log.Interfaces;

using P42Log;

namespace P42Log;

public class P42Logger : IP42Logger
{
    Dictionary<string, List<IP42LogQueue>> _queues;

    public P42Logger(bool useDefaultQueue = true)
    {
        _queues = new Dictionary<string, List<IP42LogQueue>>(StringComparer.InvariantCultureIgnoreCase);
        if(useDefaultQueue) AddLogQueue(new P42NamedQueue(""));
    }
    public void Log(string logQueue, string text)
    {
        if (_queues.TryGetValue(logQueue, out var queues))
        {
            foreach (var queue in queues)
            {
                queue.Write(text);
            }
        }
    }
    
    public void AddLogQueue(IP42LogQueue logQueue)
    {
        if (_queues.TryGetValue(logQueue.Name, out var queues))
        {
            queues.Add(logQueue);
        }
        else
        {
            _queues[logQueue.Name] = new List<IP42LogQueue> {logQueue};
        }
    }
    
}