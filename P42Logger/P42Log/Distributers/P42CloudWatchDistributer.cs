
using Amazon;
using Amazon.CloudWatchLogs;
using Amazon.CloudWatchLogs.Model;
using P42Log.Interfaces;
using System;
using System.Threading.Tasks;

namespace P42Log;
public class CloudWatchDistributer : IP42Distributer
{
    private readonly string _logGroupName;
    private readonly string _logStreamName;
    private AmazonCloudWatchLogsClient _client;

    public CloudWatchDistributer(string logGroupName, string logStreamName)
    {
        _logGroupName = logGroupName ?? throw new ArgumentNullException(nameof(logGroupName));
        _logStreamName = logStreamName ?? throw new ArgumentNullException(nameof(logStreamName));
        _client = new AmazonCloudWatchLogsClient(RegionEndpoint.EUWest1);
    }

    public bool Send(string text)
    {
        var logEvent = new InputLogEvent
        {
            Message = text,
            Timestamp = DateTime.UtcNow
        };

        var request = new PutLogEventsRequest(_logGroupName, _logStreamName, new List<InputLogEvent> { logEvent });
        Console.WriteLine(request.ToString());
        try
        {
            var response = _client.PutLogEventsAsync(request).Result;
            return true;
        }
        catch (InvalidSequenceTokenException ex)
        {
            // If you get an InvalidSequenceTokenException, you must retrieve a new sequence token.
            request.SequenceToken = ex.ExpectedSequenceToken;
            var response = _client.PutLogEventsAsync(request).Result;
            return true;
        }
        catch (Exception e)
        {
            // capture all other exceptions
            Console.WriteLine(e.Message);
            return false;
        }
    }
}