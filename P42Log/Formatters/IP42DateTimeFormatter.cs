using P42Log.Interfaces;

namespace P42Log;

public class IP42DateTimeFormatter : IP42Formatter
{
    readonly bool _useUtc;
    readonly bool _includeDate;
    readonly bool _includeTime;
    readonly bool _includeMilliseconds;
    string _datetimeFrmStr;

    public List<IP42Formatter>? Formatters { get; set; }

    public IP42DateTimeFormatter(bool useUtc, bool date, bool time, bool milliseconds)
    {
        _useUtc = useUtc;
        _includeDate = date;
        _includeTime = time;
        _includeMilliseconds = milliseconds;
        CreateDateTimeFrmStr();
    }

    void CreateDateTimeFrmStr()
    {
        if (_includeDate) _datetimeFrmStr += "yyyy-MM-dd";

        if (_includeTime) _datetimeFrmStr += string.IsNullOrEmpty(_datetimeFrmStr) ? "HH:mm:ss" : " HH:mm:ss";

        if (_includeMilliseconds) _datetimeFrmStr += string.IsNullOrEmpty(_datetimeFrmStr) ? "fff" : ".fff";
    }



    public string Format(string logLevel, string text)
    {
        DateTime now = _useUtc ? DateTime.UtcNow : DateTime.Now;
        return $"[{now.ToString(_datetimeFrmStr)}] {text}";
    }

 
}