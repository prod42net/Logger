namespace P42Log;
public class P42LogLevelSequence
{
    P42LogLevel _llAll;
    P42LogLevel _llOff;
    
    List<P42LogLevel> _logLevels;
    public P42LogLevel CurrentLogLevel { get; set; }

    public P42LogLevelSequence()
    {
        _logLevels = new List<P42LogLevel>();
        CreateDefaultLogLevels();
        Add(_llOff!);
        Add(_llAll!);
        CurrentLogLevel = _llAll!;
    }

    void CreateDefaultLogLevels()
    {
        _llAll = new P42LogLevel(P42LogLevelNaming.All,"Logging is completely unrestricted. Everything will be logged.");
        _llOff = new P42LogLevel(P42LogLevelNaming.Off,"Logging is completely restricted. Nothing will be logged.");
    }

    public bool SetLogLevel(string logLevel)
    {
        logLevel = string.IsNullOrWhiteSpace(logLevel) ? P42LogLevelNaming.All : logLevel;
        P42LogLevel? obj = GetLogLevel(logLevel);
        if (obj != null)
        {
            CurrentLogLevel = obj;
            return true;
        }
        return false;
    }

    public void Add(P42LogLevel logLevel, P42LogLevel? afterLogLevel = null)
    {
        if (afterLogLevel == null)
        {
            if (_logLevels.Count > 0)
            {
                _logLevels.Insert(_logLevels.Count - 1, logLevel);
            }
            else
            {
                _logLevels.Add(logLevel);
            }
        }
        else
        {
            _logLevels.Insert(IndexOf(afterLogLevel), logLevel);
        }
    }

    public void Remove(P42LogLevel logLevel)
    {
        if (logLevel != _llAll && logLevel != _llOff)
        {
            _logLevels.Remove(logLevel);
        }
    }

    public int IndexOf(P42LogLevel logLevel)
    {
        int index = _logLevels.IndexOf(logLevel);
        if (index == -1)
        {
            int lastPos = _logLevels.Count;
            index = (lastPos > 0) ? lastPos - 1 : 0;
        }
        return index;
    }

    P42LogLevel? GetLogLevel(string logLevel)
    {
        return _logLevels.Find(ll => ll.Name.Equals(logLevel, StringComparison.OrdinalIgnoreCase));
    }

    public bool IsLoggingActive(P42LogLevel logLevel)
    {
        return IndexOf(logLevel) >= IndexOf(CurrentLogLevel);
    }
    
    public bool IsLoggingActive(string logLevel)
    {
        P42LogLevel? llObj = _logLevels.Find(ll => ll.Name.Equals(logLevel, StringComparison.OrdinalIgnoreCase));
        
        return (llObj == null) || (IndexOf(llObj) >= IndexOf(CurrentLogLevel));
    }    
}