using System.IO;
using P42Log.Interfaces;

namespace P42Log
{
    /// <summary>
    /// This distributer logs the text into a file in a given folder
    /// </summary>
    public class P42FileDistributer : IP42Distributer
    {
        private readonly string _path;

        public P42FileDistributer(string path)
        {
            _path = path;
        }
        
        public bool Send(string text)
        {
            try
            {
                File.AppendAllText(_path, $"{text}{Environment.NewLine}");
                return true;
            }
            catch
            {
                // Log error or handle exception as needed
                return false;
            }
        }
    }
}