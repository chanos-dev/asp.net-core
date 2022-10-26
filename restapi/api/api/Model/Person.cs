using System.ComponentModel.DataAnnotations;
using api.Attribute;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace api.Model;

public class Person : IValidatableObject
{
    [ValidateNever]
    public int Id { get; set; }
    [Required]
    [Range(1, 120)]
    public int Age { get; set; } 
    
    // required
    public string Name { get; set; }
    
    [Sex("M,F")]
    [StringLength(1)]
    public string? SexType { get; set; }
    
    [EmailAddress]
    public string Email { get; set; }
    
    [Phone]
    public string Phone { get; set; }
    
    [Url]
    public string Blog { get; set; }
    
    [RegularExpression("^(?:[0-9]{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[1,2][0-9]|3[0,1]))-[1-4][0-9]{6}$")]
    public string RRN { get; set; }
    
    [Compare(otherProperty:"RRN")]
    public string ConfirmRRN { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (this.Id < 3)
        {
            yield return new ValidationResult("Invalid!.", new[] { nameof(Id) });
        }

        if (this.Age < 10)
        {
            yield return new ValidationResult("Invalid!!!.", new[] { nameof(Age) });
        }
    }
}