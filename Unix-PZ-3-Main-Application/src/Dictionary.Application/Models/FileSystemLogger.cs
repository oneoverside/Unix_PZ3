using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Dictionary.Application.Models;

public class FileSystemLogger : IDisposable
{
    public readonly string _path;
    public string? ActionName { get; set; }
    private readonly DateTime _startDate;
    private readonly List<LoggerInfo> _logs = new();
    private LogType _logType = LogType.Success;

    public FileSystemLogger(IOptions<FileLoggerOptions> options, ILogger<FileSystemLogger> lg)
    {
        lg.LogInformation($"FilePath: {options.Value.LogFolderPath}");

        // Start logs
        _startDate = DateTime.Now.ToUniversalTime();
        _path = options.Value.LogFolderPath;
    }

    #region public methods

    public void LogSuccess(string message)
    {
        _logs.Add(new LoggerInfo(message, LogType.Success));
    }
    
    public void LogInfo(string message)
    {
        _logs.Add(new LoggerInfo(message, LogType.Information));
    }
    
    public void LogWarning(string message)
    {
        _logs.Add(new LoggerInfo(message, LogType.Warning));
        if (_logType is LogType.Success)
        {
            _logType = LogType.Warning;
        }
    }
    
    public void LogError(string message, Exception exception)
    {
        _logs.Add(new LoggerInfoWithException(message, LogType.Warning, exception));
        _logType = LogType.Error;
    }

    #endregion // public methods

    #region Disposability

    public void Dispose()
    {
        var stringBuilder = new StringBuilder();
        
        for (var i = 0; i < _logs.Count; i++)
        {
            var log = _logs[i];
            if (log is LoggerInfoWithException errorLog)
            {
                var infoForLog = $"{i + 1}) {log.LogType} \n" +
                                 $"     Message:      {log.Message} \n" +
                                 $"     ErrorMessage: {errorLog.Exception.Message} \n" +
                                 $"     StackTrace:   {errorLog.Exception.StackTrace} \n";
                stringBuilder.Append(infoForLog);
            }
            else
            {
                var infoForLog = $"{i + 1}) {log.LogType} \n" +
                                 $"     Message:      {log.Message} \n";
                stringBuilder.Append(infoForLog);
            }
        }

        var fileName = $"{_startDate}-{_logType}-{ActionName ?? "UnknownAction"}.txt".Replace('/', '.').Replace(':', '.');
        File.AppendAllText($"{_path}/{fileName}", stringBuilder.ToString());
    }

    #endregion // Disposability


    #region Nested Types

    public enum LogType
    {
        Error,
        Success,
        Warning,
        Information
    }

    public class LoggerInfo
    {
        public string Message { get; set; }
        public LogType LogType { get; set; }
        
        public LoggerInfo(string message, LogType logType)
        {
            Message = message;
            LogType = logType;
        }
    }
    
    public class LoggerInfoWithException : LoggerInfo
    {
        public Exception Exception { get; set; }

        public LoggerInfoWithException(string message, LogType logType, Exception exception) : base(message, logType)
        {
            Exception = exception;
        }
    }
    
    public class FileLoggerOptions
    {
        public string LogFolderPath { get; set; } = null!;
    }

    #endregion // Nested Types
}