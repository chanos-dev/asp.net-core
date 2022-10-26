using System.ComponentModel.DataAnnotations;

namespace api.Attribute;

public class SexAttribute : ValidationAttribute
{
    public string Sex { get; }
    public SexAttribute(string sex) => this.Sex = sex;

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var inputedSex = (string)value ?? string.Empty;

        if (!Sex.Contains(inputedSex))
            return new ValidationResult("Invalid.");
        
        return ValidationResult.Success;
    }
}