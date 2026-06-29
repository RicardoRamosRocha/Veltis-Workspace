namespace Veltis.Workspace.Application.Forms.Interfaces;

public interface IFieldFactory
{
    DynamicFieldRenderModel Create(DynamicFieldDefinition field);
}
