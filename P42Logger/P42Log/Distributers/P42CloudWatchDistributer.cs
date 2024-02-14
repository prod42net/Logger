
using Amazon;
using Amazon.CloudWatchLogs;
using Amazon.CloudWatchLogs.Model;
using P42Log.Interfaces;
using System;
using System.Threading.Tasks;

namespace P42Log;
/// <summary>
/// Represents a cloud watch log distributer that sends log messages to an Amazon CloudWatch Logs log group and log stream.
/// </summary>
public class CloudWatchDistributer : IP42Distributer
{
    private readonly string _logGroupName;
    private readonly string _logStreamName;
    private AmazonCloudWatchLogsClient _client;

    /// <summary>
    /// Represents a cloud watch log distributer that sends log messages to an Amazon CloudWatch Logs log group and log stream.
    /// </summary>
    public CloudWatchDistributer(string logGroupName, string logStreamName)
    {
        _logGroupName = logGroupName ?? throw new ArgumentNullException(nameof(logGroupName));
        _logStreamName = logStreamName ?? throw new ArgumentNullException(nameof(logStreamName));
        _client = new AmazonCloudWatchLogsClient(RegionEndpoint.EUWest1);
    }

    /// <summary>
    /// Sends the given text to the specified Amazon CloudWatch Logs log group and log stream.
    /// </summary>
    /// <param name="text">The text to send.</param>
    /// <returns>Returns 'true' if the text was sent successfully; otherwise, 'false'.</returns>
    public bool Send(string text)
    {
        var logEvent = new InputLogEvent
        {
            Message = text,
            Timestamp = DateTime.UtcNow
        };

        PutLogEventsRequest request = new PutLogEventsRequest(_logGroupName, _logStreamName, new List<InputLogEvent> { logEvent });
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