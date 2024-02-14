namespace P42Log;
public class P42LogLevel
{
    public P42LogLevel()
    {
        
    }

    public P42LogLevel(string name, string description = "")
    {
        Name = name;
        Description = description;
    }
    /// <summary>
    /// Name of the LogLevel
    /// </summary>
    public string Name { get; set; } = "";
    /// <summary>
    /// Short description if necessary 
    /// </summary>
    public string Description { get; set; } = "";
}