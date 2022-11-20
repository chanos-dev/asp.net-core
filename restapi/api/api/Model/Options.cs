using System.ComponentModel.DataAnnotations;
using api.Attribute;

namespace api.Model;

public class Options
{
    [Required]
    public bool Use { get; set; }
    
    [RequiredIf(nameof(Use))]
    public string Url { get; set; }
    
    [Required]
    public string Path { get; set; }
    
    [RequiredIf(nameof(Path))]
    public string FileName { get; set; }
}