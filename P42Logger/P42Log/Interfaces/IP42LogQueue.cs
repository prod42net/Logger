namespace P42Log.Interfaces;

public interface IP42LogQueue
{
    string Name { get; set; }
    void AddDistributer(IP42Distributer distributer);
    void AddFormatter(IP42Formatter formatter);
//    string ApplyFormatting(string logLevel,string msg);
//    void DistributeLog(string msg);
    void Write(string logLevel,string msg);
    bool SetLogLevel(string logLevel);
}