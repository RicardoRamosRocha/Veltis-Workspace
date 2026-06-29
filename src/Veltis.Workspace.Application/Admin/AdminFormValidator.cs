using System.Text.Json;
using System.Text.RegularExpressions;

namespace Veltis.Workspace.Application.Admin;

public sealed class AdminFormValidator
{
    public AdminValidationResult Validate(
        IReadOnlyCollection<AdminFieldSpec> fields,
        IReadOnlyDictionary<string, string?> values)
    {
        AdminValidationResult result = new();

        foreach (AdminFieldSpec field in fields)
        {
            values.TryGetValue(field.Name, out string? rawValue);
            string value = rawValue?.Trim() ?? string.Empty;

            if (field.Required && string.IsNullOrWhiteSpace(value))
            {
                result.Add(field.Name, $"Informe {field.Label.ToLowerInvariant()}.");
                continue;
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                continue;
            }

            if (field.IsSlug && !Regex.IsMatch(value, "^[a-z0-9]+(?:-[a-z0-9]+)*$"))
            {
                result.Add(field.Name, "Informe um slug válido usando letras minúsculas, números e hífens.");
            }

            if (field.IsJson && !IsValidJson(value))
            {
                result.Add(field.Name, "Informe um JSON válido.");
            }

            if (field.Kind == AdminFieldKind.Decimal && decimal.TryParse(value, out decimal decimalValue))
            {
                if (field.MinDecimal.HasValue && decimalValue < field.MinDecimal.Value)
                {
                    result.Add(field.Name, $"{field.Label} deve ser maior ou igual a {field.MinDecimal.Value}.");
                }

                if (field.MaxDecimal.HasValue && decimalValue > field.MaxDecimal.Value)
                {
                    result.Add(field.Name, $"{field.Label} deve ser menor ou igual a {field.MaxDecimal.Value}.");
                }
            }

            if (field.Kind == AdminFieldKind.Number && int.TryParse(value, out int intValue) &&
                field.MinInt.HasValue && intValue < field.MinInt.Value)
            {
                result.Add(field.Name, $"{field.Label} deve ser maior ou igual a {field.MinInt.Value}.");
            }
        }

        return result;
    }

    private static bool IsValidJson(string value)
    {
        try
        {
            using JsonDocument document = JsonDocument.Parse(value);
            return true;
        }
        catch (JsonException)
        {
            return false;
        }
    }
}
