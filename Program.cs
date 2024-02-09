using System.Diagnostics;

using P42Log;
using P42Log.Interfaces;

namespace Logger;
class Program
{
    /// <summary>
    /// Just a little test program for the logger functions
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, to the P42Logger");
        Stopwatch stopwatch = new Stopwatch();
        IP42Logger logger = new P42Logger();
        logger.Log("------------------------------");
        logger.SetLogLevel("");
        WriteLogEntries(logger, 50,stopwatch);
        WriteElapsedTime(logger, stopwatch);
        

        logger.Log("------------------------------");
        logger.SetLogLevel(P42LogLevelNaming.Debug);
        WriteLogEntries(logger, 50,stopwatch);
        WriteElapsedTime(logger, stopwatch);
        
        logger.Log("------------------------------");
        logger.SetLogLevel(P42LogLevelNaming.Info);
        WriteLogEntries(logger, 50,stopwatch);
        WriteElapsedTime(logger, stopwatch);
        
        logger.Log("------------------------------");
        logger.SetLogLevel(P42LogLevelNaming.Error);
        WriteLogEntries(logger, 50,stopwatch);
        WriteElapsedTime(logger, stopwatch);
        
        logger.Log("------------------------------");
        logger.SetLogLevel(P42LogLevelNaming.Fatal);
        WriteLogEntries(logger, 50,stopwatch);
        WriteElapsedTime(logger, stopwatch);
        
        logger.Log("------------------------------");
        logger.SetLogLevel(P42LogLevelNaming.Off);
        WriteLogEntries(logger, 50,stopwatch);
        WriteElapsedTime(logger, stopwatch);

        Console.WriteLine("Press any key to exit");
        Console.ReadKey();
    }

    static void WriteLogEntries(IP42Logger logger, int repeats,Stopwatch watch)
    {
        watch.Start();
        for (int i = 0; i < repeats; i++)
        {
            logger.Log(P42LogLevelNaming.Trace, $"log entry [{i}] for level {P42LogLevelNaming.Trace}");
            logger.Log(P42LogLevelNaming.Debug, $"log entry [{i}] for level {P42LogLevelNaming.Debug}");
            logger.Log(P42LogLevelNaming.Info, $"log entry [{i}] for level {P42LogLevelNaming.Info}");
            logger.Log(P42LogLevelNaming.Warning, $"log entry [{i}] for level {P42LogLevelNaming.Warning}");
            logger.Log(P42LogLevelNaming.Error, $"log entry [{i}] for level {P42LogLevelNaming.Error}");
            logger.Log(P42LogLevelNaming.Fatal, $"log entry [{i}] for level {P42LogLevelNaming.Fatal}");
            logger.Log(P42LogLevelNaming.All, $"log entry [{i}] for level {P42LogLevelNaming.All}");
        }
        watch.Stop();
    }

    static void WriteElapsedTime(IP42Logger logger, Stopwatch watch)
    {
        TimeSpan ts = watch.Elapsed;
        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
        logger.Log($"RunTime [{elapsedTime}]");
    }
}


        /*
        for (int i = 0; i < 100; i++)
        {
            logger.Log(P42LogLevelNaming.Trace, $"log entry [{i}] for level {P42LogLevelNaming.Trace}");
            logger.Log(P42LogLevelNaming.Debug, $"log entry [{i}] for level {P42LogLevelNaming.Trace}");
            logger.Log(P42LogLevelNaming.Info, $"log entry [{i}] for level {P42LogLevelNaming.Trace}");
            logger.Log(P42LogLevelNaming.Warning, $"log entry [{i}] for level {P42LogLevelNaming.Trace}");
            logger.Log(P42LogLevelNaming.Error, $"log entry [{i}] for level {P42LogLevelNaming.Trace}");
            logger.Log(P42LogLevelNaming.Fatal, $"log entry [{i}] for level {P42LogLevelNaming.Trace}");
            logger.Log(P42LogLevelNaming.Trace, $"log entry [{i}] for level {P42LogLevelNaming.Trace}");
            
            _logLevelSequence.Add(new P42LogLevel(){Name = P42LogLevelNaming.Trace,Description = "Trace log information"});
           _logLevelSequence.Add(new P42LogLevel(){Name = P42LogLevelNaming.Debug,Description = "Debug log information"});
        }
        
        */
        /*
        logger.Log("info", "test info");
        logger.Log("error", "test error 1");
        logger.Log("special", "test special");
        logger.Log("error", "test error 2");
*/
        /*
        Thread.Sleep(100);
        logger.AddLogQueue(new P42NamedQueue(P42LogLevelNaming.Debug));

        logger.AddLogQueue(new P42NamedQueue(P42LogLevelNaming.Info));

        // create standard console queue and in addition an error file queue
        P42NamedQueue errorQueue = new P42NamedQueue(P42LogLevelNaming.Error);
        errorQueue.AddDistributer(new P42FileDistributer("./error.log"));
        logger.AddLogQueue(errorQueue);

        logger.AddLogQueue(new P42NamedQueue(P42LogLevelNaming.Fatal));
        for (int i = 0; i < 1000; i++)
        {
            logger.Log(P42LogLevelNaming.Debug,$"This is a debug log entry #[{i}]");

        }
        logger.Log(P42LogLevelNaming.Info,"This is a info log entry");
        logger.Log(P42LogLevelNaming.Error,"This is a error log entry");
        logger.Log(P42LogLevelNaming.Fatal,"This is a fatal log entry");

        */
