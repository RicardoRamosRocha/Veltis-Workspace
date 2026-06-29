using Veltis.Workspace.Application.Forms;
using Veltis.Workspace.Application.Forms.Services;

namespace Veltis.Workspace.Tests.Forms;

public sealed class ValidationEngineTests
{
    [Fact]
    public void Validate_ShouldReturnErrorsForRequiredEmailAndCpf()
    {
        DynamicFormSchema schema = new()
        {
            Fields =
            [
                new DynamicFieldDefinition { Id = "name", Name = "name", Label = "Name", Required = true },
                new DynamicFieldDefinition { Id = "email", Name = "email", Label = "Email", Type = DynamicFieldType.Email },
                new DynamicFieldDefinition { Id = "cpf", Name = "cpf", Label = "CPF", Type = DynamicFieldType.CPF }
            ]
        };

        Dictionary<string, string?> values = new()
        {
            ["email"] = "invalid",
            ["cpf"] = "123"
        };

        FormValidationResult result = new ValidationEngine().Validate(schema, values);

        Assert.False(result.IsValid);
        Assert.Contains("name", result.Errors.Keys);
        Assert.Contains("email", result.Errors.Keys);
        Assert.Contains("cpf", result.Errors.Keys);
    }

    [Fact]
    public void Validate_ShouldSupportRegexAndNumericRange()
    {
        DynamicFormSchema schema = new()
        {
            Fields =
            [
                new DynamicFieldDefinition { Id = "code", Name = "code", Label = "Code", Validation = "^[A-Z]{3}$" },
                new DynamicFieldDefinition { Id = "amount", Name = "amount", Label = "Amount", Type = DynamicFieldType.Decimal, Min = 10, Max = 20 }
            ]
        };

        Dictionary<string, string?> values = new()
        {
            ["code"] = "abc",
            ["amount"] = "25"
        };

        FormValidationResult result = new ValidationEngine().Validate(schema, values);

        Assert.False(result.IsValid);
        Assert.Contains("code", result.Errors.Keys);
        Assert.Contains("amount", result.Errors.Keys);
    }
}
