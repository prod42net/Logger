namespace P42Log.Interfaces;

public interface IP42Logger
{
    void Log(string logLevel, string text);
    void Log(string text);    
    void AddLogQueue(IP42LogQueue logQueue);
    void SetLogLevel(string logLevel);
    public List<IP42LogQueue> LogQueues { get; }
}