using System.Collections.Concurrent;
using System.Threading;
using P42Log.Interfaces;

namespace P42Log;

public class P42BasicLogQueue : IP42LogQueue
{
    Thread _worker;
    BlockingCollection<string> _queue;
    List<IP42Distributer> _distributers;
    P42LogLevelSequence _logLevelSequence;
    readonly List<IP42Formatter> _formatters;
    public string Name { get; set; }
    //bool _queueActive = false;

    public P42BasicLogQueue()
    {
        Name = "noname";
        _queue = new BlockingCollection<string>();
        _distributers = new List<IP42Distributer>();
        _formatters = new List<IP42Formatter>();
        _logLevelSequence = new P42LogLevelSequence();
        AddDefaultLogLevels();
        _worker = new Thread(ProcessQueue);
        //_worker.IsBackground = true;
        _worker.Start();
    }

    ~P42BasicLogQueue()
    {
        //_queueActive = false;
        _worker.Join(1000);
    }

    void AddDefaultLogLevels()
    {
        AddLogLevel(P42LogLevelNaming.Trace, "Trace log information");
        AddLogLevel(P42LogLevelNaming.Debug, "Debug log information");
        AddLogLevel(P42LogLevelNaming.Info, "Info log information");
        AddLogLevel(P42LogLevelNaming.Warning, "Warning log information");
        AddLogLevel(P42LogLevelNaming.Error, "Error log information");
        AddLogLevel(P42LogLevelNaming.Fatal, "Fatal log information");
    }
    void AddLogLevel(string name, string description)
    {
        _logLevelSequence.Add(new P42LogLevel(name, description));
    }
    
    public void AddDistributer(IP42Distributer distributer)
    {
        _distributers.Add(distributer);
    }

    public void AddFormatter(IP42Formatter formatter)
    {
        _formatters.Add(formatter);
    }

    public bool SetLogLevel(string logLevel)
    {
        Write("",$"Change logLevel from [{_logLevelSequence.CurrentLogLevel.Name}] to [{logLevel}]");
        return _logLevelSequence.SetLogLevel(logLevel);
    }
    
    public void Write(string logLevel,string text)
    {
        if (_logLevelSequence.IsLoggingActive(logLevel))
        {
            text = ApplyFormatting(logLevel, text);
            _queue.Add(text);
        }
        else if (System.Diagnostics.Debugger.IsAttached)
        {
            Console.WriteLine($"log suppressed loglevel[{logLevel}] text[{text}]");
        }
    }

    public string ApplyFormatting(string logLevel,string msg)
    {
        foreach (IP42Formatter formatter in _formatters)
        {
            msg = formatter.Format(logLevel,msg);
        }
        return msg;
    }
    public void DistributeLog(string msg)
    {
        foreach (IP42Distributer distributer in _distributers)
        {
            distributer.Send(msg);
        }
    }


    void ProcessQueue()
    {
        foreach (string msg in _queue.GetConsumingEnumerable())
        {
            DistributeLog(msg);
        }
    }
}