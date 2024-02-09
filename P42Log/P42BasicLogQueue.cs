using System.Collections.Concurrent;
using System.Threading;
using P42Log.Interfaces;

namespace P42Log;

public class P42BasicLogQueue : IP42LogQueue
{
    Thread _worker;
    BlockingCollection<string> _queue;
    List<IP42Distributer> _distributers;
    readonly List<IP42Formatter> _formatters;
    public string Name { get; set; }
    bool _queueActive = false;

    public P42BasicLogQueue()
    {
        Name = "noname";
        _queue = new BlockingCollection<string>();
        _distributers = new List<IP42Distributer>();
        _formatters = new List<IP42Formatter>();
        _worker = new Thread(ProcessQueue);
        //_worker.IsBackground = true;
        _worker.Start();
    }

    ~P42BasicLogQueue()
    {
        _queueActive = false;
        _worker.Join(1000);
    }
    
   

    public void AddDistributer(IP42Distributer distributer)
    {
        _distributers.Add(distributer);
    }

    public void AddFormatter(IP42Formatter formatter)
    {
        _formatters.Add(formatter);
    }

    public void Write(string text)
    {
        _queue.Add(text);
    }

     string ApplyFormatting(string text)
    {
        foreach (IP42Formatter formatter in _formatters)
        {
            text = formatter.Format(text);
        }
        return text;
    }
    void DistributeText(string text)
    {
        foreach (IP42Distributer distributer in _distributers)
        {
            distributer.Send(text);
        }
    }

    void ProcessQueue()
    {
        string msg = "";
        foreach (string rawMsg in _queue.GetConsumingEnumerable())
        {
            msg = ApplyFormatting(rawMsg);
            DistributeText(msg);
        }
    }
}