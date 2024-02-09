using P42Log;
using P42Log.Interfaces;

namespace Logger;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, to the P42Logger");
        
        IP42Logger logger = new P42Logger();
        logger.Log("info","test info");
        logger.Log("error","test error");
        logger.Log("","test empty");
        logger.Log("error","test error");
        
        
        logger.AddLogQueue(new P42NamedQueue(P42LogLevel.Debug));
        
        logger.AddLogQueue(new P42NamedQueue(P42LogLevel.Info));

        // create standard console queue and in addition an error file queue 
        P42NamedQueue errorQueue = new P42NamedQueue(P42LogLevel.Error);
        errorQueue.AddDistributer(new P42FileDistributer("./error.log"));
        logger.AddLogQueue(errorQueue);
        
        logger.AddLogQueue(new P42NamedQueue(P42LogLevel.Fatal));
        for (int i = 0; i < 1000; i++)
        {
            logger.Log(P42LogLevel.Debug,$"This is a debug log entry #[{i}]");
            
        }
        logger.Log(P42LogLevel.Info,"This is a info log entry");
        logger.Log(P42LogLevel.Error,"This is a error log entry");
        logger.Log(P42LogLevel.Fatal,"This is a fatal log entry");

        
        
        Console.WriteLine("Press any key to exit");
        Console.ReadKey();
    }
}