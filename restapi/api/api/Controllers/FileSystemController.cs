using api.Model;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FileSystemController : ControllerBase
{
    private const string SAVE_PATH = @"./sample";
    private const int LIMIT_SIZE = 1 * 1024 * 1024; // 1MB
    static FileSystemController()
    {
        if (!Directory.Exists(SAVE_PATH))
            Directory.CreateDirectory(SAVE_PATH);
    }
    
    private readonly ILogger<FileSystemController> _logger;

    public FileSystemController(ILogger<FileSystemController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    [RequestSizeLimit(LIMIT_SIZE)]
    [Consumes("multipart/form-data")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [Route("uploadfile")]
    public async Task<ActionResult> Upload([FromForm] string ext, IFormFile file)
    { 
        var fileName = $"{Path.ChangeExtension(file.FileName, ext.Replace(".", ""))}";
        var absolutePath = Path.Combine(SAVE_PATH, fileName);

        using var stream = new FileStream(absolutePath, FileMode.Create, FileAccess.Write);
        await file.CopyToAsync(stream);
        
        return Ok(new
        {
            complete = true,
            result = new FileResponse()
            {
                FileName = file.FileName,
                Size = (int)file.Length
            }
        });
    }
}