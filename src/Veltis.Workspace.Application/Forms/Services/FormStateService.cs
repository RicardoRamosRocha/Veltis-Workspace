using Veltis.Workspace.Application.Forms.Interfaces;

namespace Veltis.Workspace.Application.Forms.Services;

public sealed class FormStateService : IFormStateService
{
    public FormState Create(DynamicFormSchema schema, IReadOnlyDictionary<string, string?>? values = null)
    {
        FormState state = new();

        foreach (DynamicFieldDefinition field in Flatten(schema.Fields))
        {
            string key = string.IsNullOrWhiteSpace(field.Name) ? field.Id : field.Name;
            state.Values[key] = values is not null && values.TryGetValue(key, out string? value)
                ? value
                : field.DefaultValue;

            if (field.ReadOnly)
            {
                state.ReadOnly.Add(key);
            }

            if (!field.Visible)
            {
                state.Hidden.Add(key);
            }
        }

        return state;
    }

    public FormState Touch(FormState state, string fieldName)
    {
        state.Touched.Add(fieldName);
        return state;
    }

    public FormState SetValue(FormState state, string fieldName, string? value)
    {
        state.Values[fieldName] = value;
        state.Dirty.Add(fieldName);
        state.Touched.Add(fieldName);
        return state;
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
}
