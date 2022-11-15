namespace api.Model;

public class ErrorResponse
{
    public IEnumerable<string> Errors { get; set; }
    public string TraceID { get; set; }
}