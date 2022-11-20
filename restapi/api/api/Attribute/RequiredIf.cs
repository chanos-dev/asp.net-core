using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace api.Attribute;

public class RequiredIf : RequiredAttribute
{
    private readonly string _dependencyProp;

    public RequiredIf(string dependencyProp)
    {
        _dependencyProp = dependencyProp;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var properties = validationContext.ObjectInstance.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
        Dictionary<string, object?> propValues = properties.ToDictionary(prop => prop.Name, prop => prop.GetValue(validationContext.ObjectInstance));

        var dependencyValue = propValues[_dependencyProp];
        
        if (dependencyValue is null)
            return ValidationResult.Success;
        
        if (dependencyValue is bool bValue && !bValue)
            return ValidationResult.Success;
        
        if (dependencyValue is string sValue && string.IsNullOrEmpty(sValue))
            return ValidationResult.Success;

        if (!base.IsValid(value))
            return new ValidationResult($"This {validationContext.DisplayName} field is required!"); 
        
        return ValidationResult.Success;
    }
}