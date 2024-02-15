using System.Diagnostics;
using P42Log;
using P42Log.Interfaces;

namespace P42LoggerTestProgram;
/// <summary>
/// Represents the entry point class of the program.
/// </summary>
class Program
{
    /// <summary>
    /// Executes the main function of the P42LoggerTestProgram.
    /// </summary>
    /// <param name="args">Array of command-line arguments</param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, to the P42Logger");
        Stopwatch stopwatch = new Stopwatch();
        IP42Logger logger = new P42Logger();
        
        // creating a extra error queue with a specific LogDistributer - in this case distributing logs to aws cloudwatch
        P42NamedQueue errorQueue = 
            new P42NamedQueue("AWS",
                new CloudWatchDistributer("P42LGroup","ErrorStream"));
        errorQueue.SetLogLevel(P42LogLevelNaming.Error);
        logger.AddLogQueue(errorQueue);
        
        //errorQueue.AddDistributer(new P42FileDistributer("./error.log"));
        //logger.AddLogQueue(errorQueue);
        
        // set log level to all
        logger.SetLogLevel("");
        logger.Log("------------------------------");
        
        // trying to get a specific logQueue based on type
        IP42LogQueue awsQueue = logger.LogQueues.Find(q => q.GetType() == typeof(CloudWatchDistributer));
        // setting the loglevel on a specific queue
        if (awsQueue != null)
        {
            awsQueue.SetLogLevel("error");
        }
        
        // just writing demo entries
        WriteLogEntries(logger, 10,stopwatch);
        
        
        


        Console.WriteLine("Press any key to exit");
        Console.ReadKey();
    }

    /// <summary>
    /// Writes a specified number of log entries using the given logger and stopwatch.
    /// </summary>
    /// <param name="logger">The logger object used to write log entries</param>
    /// <param name="repeats">The number of log entries to write</param>
    /// <param name="watch">The stopwatch object used to measure elapsed time</param>
    static void WriteLogEntries(IP42Logger logger, int repeats,Stopwatch watch)
    {
        // start of timer
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
        // stop timer
        watch.Stop();
        // write execution time
        WriteElapsedTime(logger, watch);
    }

    /// <summary>
    /// Writes the elapsed time in the format "HH:MM:SS.TT" to the logger.
    /// </summary>
    /// <param name="logger">The logger to write the elapsed time to</param>
    /// <param name="watch">A Stopwatch instance used to measure the elapsed time</param>
    static void WriteElapsedTime(IP42Logger logger, Stopwatch watch)
    {
        TimeSpan ts = watch.Elapsed;
        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
        logger.Log($"RunTime [{elapsedTime}]");
    }
}

