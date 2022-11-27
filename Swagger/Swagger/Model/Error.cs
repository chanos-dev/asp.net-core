using Swashbuckle.AspNetCore.Annotations;

namespace Swagger.Model;

public class Error
{
    [SwaggerSchema("실패 코드")]
    public int Code { get; set; }
    [SwaggerSchema("실패 메세지")]
    public string Message { get; set; }
}