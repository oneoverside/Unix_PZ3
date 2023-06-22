using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LogAnalizer.Controllers;

public class LogsController : ControllerBase
{
    private readonly ILogger<LogsController> _logger;
    private readonly string _filePath;
    
    public LogsController(IOptions<FileSystemOptions> fileSystemOptions, ILogger<LogsController> logger)
    {
        _logger = logger;
        _filePath = fileSystemOptions.Value.Path;
        logger.LogInformation($"Will use address: {_filePath}");
    }
    
    [HttpPost("GetAllLogsUnits")]
    public IActionResult GetAllLogsUnits([FromBody]GetAllLogsUnitsRequest test, [FromServices]IOptions<FileSystemOptions> fileSystemOptions)
    {
        try
        {
            var filePaths = Directory.GetFiles(_filePath);
            _logger.LogInformation($"Get files: {_filePath}");

            foreach (var s in filePaths)
            {
                _logger.LogInformation(s);
            }
            var filteredFiles = filePaths
                .Where(fp =>
                    System.IO.File.GetLastWriteTime(fp) >= test.MinDate &&
                    System.IO.File.GetLastWriteTime(fp) <= test.MaxDate)
                .ToList();
            
            return Ok(filteredFiles);
        }
        catch(Exception ex)
        {
            _logger.LogError("Can't get files", ex);
            return Ok();
        }
    }

    [HttpPost("GetLogByUnitName")]
    public IActionResult GetLogByUnitName([FromBody]GetLogByUnitNameRequest getLogByUnitNameRequest, [FromServices]IOptions<FileSystemOptions> fileSystemOptions)
    {
        return Ok(System.IO.File.ReadAllText($"{_filePath}/{getLogByUnitNameRequest.LogUnitName}"));
    }

    #region sealed classes

    public class GetAllLogsUnitsRequest
    {
        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }
    }
    
    public class GetLogByUnitNameRequest
    {
        public string LogUnitName { get; set; } = null!;
    }
    
    public class FileSystemOptions
    {
        public string Path { get; set; } = null!;
    }

    #endregion // sealed classes
}