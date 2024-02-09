namespace P42Log.Interfaces;

public interface IP42Logger
{
    void Log(string loglevel, string text);
    void AddLogQueue(IP42LogQueue logQueue);
}