using Veltis.Workspace.Application.Forms.Interfaces;

namespace Veltis.Workspace.Application.Forms.Services;

public sealed class FieldFactory : IFieldFactory
{
    public DynamicFieldRenderModel Create(DynamicFieldDefinition field)
    {
        string name = string.IsNullOrWhiteSpace(field.Name) ? field.Id : field.Name;

        return new DynamicFieldRenderModel
        {
            Id = field.Id,
            Name = name,
            Type = field.Type,
            Label = string.IsNullOrWhiteSpace(field.Label) ? name : field.Label,
            Placeholder = field.Placeholder,
            HelpText = field.HelpText,
            Required = field.Required,
            Visible = field.Visible,
            ReadOnly = field.ReadOnly,
            DefaultValue = field.DefaultValue,
            CssClass = field.CssClass ?? string.Empty,
            Width = string.IsNullOrWhiteSpace(field.Width) ? "full" : field.Width,
            Options = field.Options,
            Children = field.Children
                .OrderBy(child => child.Order)
                .Select(Create)
                .ToArray()
        };
    }
}
