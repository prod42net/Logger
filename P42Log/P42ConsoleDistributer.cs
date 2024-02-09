using P42Log.Interfaces;

namespace P42Log;

public class P42ConsoleDistributer : IP42Distributer
{
    /// <summary>
    ///     This is kind of most simple distribution of an log information
    /// </summary>
    /// <param name="text">the information</param>
    /// <returns></returns>
    public bool Send(string text)
    {
        try
        {
            Console.WriteLine(text);
        }
        catch // I don't need it here - (Exception e)
        {
            //without being able to log ...it's difficult to tell someone except 
            return false;
        }

        return true;
    }
}