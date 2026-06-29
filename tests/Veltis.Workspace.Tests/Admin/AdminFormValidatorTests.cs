using Veltis.Workspace.Application.Admin;

namespace Veltis.Workspace.Tests.Admin;

public sealed class AdminFormValidatorTests
{
    [Fact]
    public void Validate_ShouldRejectInvalidJsonAndSlug()
    {
        AdminFieldSpec[] fields =
        [
            new() { Name = "Slug", Label = "Slug", Required = true, IsSlug = true },
            new() { Name = "SchemaJson", Label = "Schema JSON", Kind = AdminFieldKind.Json, IsJson = true }
        ];

        Dictionary<string, string?> values = new()
        {
            ["Slug"] = "Slug Inválido",
            ["SchemaJson"] = "{ invalid"
        };

        AdminValidationResult result = new AdminFormValidator().Validate(fields, values);

        Assert.False(result.IsValid);
        Assert.Contains("Slug", result.Errors.Keys);
        Assert.Contains("SchemaJson", result.Errors.Keys);
    }

    [Fact]
    public void Validate_ShouldRejectOutOfRangeTemperatureAndNegativePrice()
    {
        AdminFieldSpec[] fields =
        [
            new() { Name = "Temperature", Label = "Temperature", Kind = AdminFieldKind.Decimal, MinDecimal = 0, MaxDecimal = 2 },
            new() { Name = "MonthlyPrice", Label = "Preço mensal", Kind = AdminFieldKind.Decimal, MinDecimal = 0 }
        ];

        Dictionary<string, string?> values = new()
        {
            ["Temperature"] = "3",
            ["MonthlyPrice"] = "-1"
        };

        AdminValidationResult result = new AdminFormValidator().Validate(fields, values);

        Assert.False(result.IsValid);
        Assert.Contains("Temperature", result.Errors.Keys);
        Assert.Contains("MonthlyPrice", result.Errors.Keys);
    }

    [Fact]
    public void Validate_ShouldAcceptValidValues()
    {
        AdminFieldSpec[] fields =
        [
            new() { Name = "Slug", Label = "Slug", Required = true, IsSlug = true },
            new() { Name = "SchemaJson", Label = "Schema JSON", Kind = AdminFieldKind.Json, IsJson = true },
            new() { Name = "MaxTokens", Label = "MaxTokens", Kind = AdminFieldKind.Number, MinInt = 1 }
        ];

        Dictionary<string, string?> values = new()
        {
            ["Slug"] = "formulario-inicial",
            ["SchemaJson"] = """{"fields":[]}""",
            ["MaxTokens"] = "1000"
        };

        AdminValidationResult result = new AdminFormValidator().Validate(fields, values);

        Assert.True(result.IsValid);
    }
}
