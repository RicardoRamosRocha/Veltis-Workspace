using System.Globalization;
using System.Text.RegularExpressions;
using Veltis.Workspace.Application.Forms.Interfaces;

namespace Veltis.Workspace.Application.Forms.Services;

public sealed class ValidationEngine : IValidationEngine
{
    public FormValidationResult Validate(DynamicFormSchema schema, IReadOnlyDictionary<string, string?> values)
    {
        FormValidationResult result = new();

        foreach (DynamicFieldDefinition field in Flatten(schema.Fields).Where(field => field.Visible))
        {
            string key = string.IsNullOrWhiteSpace(field.Name) ? field.Id : field.Name;
            values.TryGetValue(key, out string? value);
            string normalized = value?.Trim() ?? string.Empty;

            if (field.Required && string.IsNullOrWhiteSpace(normalized))
            {
                result.AddError(key, $"O campo '{field.Label}' é obrigatório.");
                continue;
            }

            if (string.IsNullOrWhiteSpace(normalized))
            {
                continue;
            }

            ValidateLength(field, key, normalized, result);
            ValidateRange(field, key, normalized, result);
            ValidateFormat(field, key, normalized, result);
        }

        return result;
    }

    private static IEnumerable<DynamicFieldDefinition> Flatten(IEnumerable<DynamicFieldDefinition> fields)
    {
        foreach (DynamicFieldDefinition field in fields)
        {
            yield return field;

            foreach (DynamicFieldDefinition child in Flatten(field.Children))
            {
                yield return child;
            }
        }
    }

    private static void ValidateLength(DynamicFieldDefinition field, string key, string value, FormValidationResult result)
    {
        if (field.MinLength.HasValue && value.Length < field.MinLength.Value)
        {
            result.AddError(key, $"O campo '{field.Label}' deve ter pelo menos {field.MinLength.Value} caracteres.");
        }

        if (field.MaxLength.HasValue && value.Length > field.MaxLength.Value)
        {
            result.AddError(key, $"O campo '{field.Label}' deve ter no máximo {field.MaxLength.Value} caracteres.");
        }
    }

    private static void ValidateRange(DynamicFieldDefinition field, string key, string value, FormValidationResult result)
    {
        if ((field.Min.HasValue || field.Max.HasValue) &&
            decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal decimalValue))
        {
            if (field.Min.HasValue && decimalValue < field.Min.Value)
            {
                result.AddError(key, $"O campo '{field.Label}' deve ser maior ou igual a {field.Min.Value}.");
            }

            if (field.Max.HasValue && decimalValue > field.Max.Value)
            {
                result.AddError(key, $"O campo '{field.Label}' deve ser menor ou igual a {field.Max.Value}.");
            }
        }
    }

    private static void ValidateFormat(DynamicFieldDefinition field, string key, string value, FormValidationResult result)
    {
        if (field.Type == DynamicFieldType.Email && !Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            result.AddError(key, $"O campo '{field.Label}' deve conter um e-mail válido.");
        }

        if (field.Type == DynamicFieldType.Url && !Uri.TryCreate(value, UriKind.Absolute, out _))
        {
            result.AddError(key, $"O campo '{field.Label}' deve conter uma URL válida.");
        }

        if (field.Type == DynamicFieldType.CPF && DigitsOnly(value).Length != 11)
        {
            result.AddError(key, $"O campo '{field.Label}' deve conter um CPF válido.");
        }

        if (field.Type == DynamicFieldType.CNPJ && DigitsOnly(value).Length != 14)
        {
            result.AddError(key, $"O campo '{field.Label}' deve conter um CNPJ válido.");
        }

        if (!string.IsNullOrWhiteSpace(field.Validation) && !Regex.IsMatch(value, field.Validation))
        {
            result.AddError(key, $"O campo '{field.Label}' não atende à validação configurada.");
        }
    }

    private static string DigitsOnly(string value)
    {
        return new string(value.Where(char.IsDigit).ToArray());
    }
}
