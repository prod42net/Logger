namespace P42Log.Interfaces;

public interface IP42LogQueue
{
    string Name { get; set; }
    void AddDistributer(IP42Distributer distributer);
    void AddFormatter(IP42Formatter formatter);
    void Write(string text);
}