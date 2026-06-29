using Veltis.Workspace.Application.Forms;
using Veltis.Workspace.Application.Forms.Services;

namespace Veltis.Workspace.Tests.Forms;

public sealed class FormStateTests
{
    [Fact]
    public void Create_ShouldTrackReadonlyHiddenAndDefaultValues()
    {
        DynamicFormSchema schema = new()
        {
            Fields =
            [
                new DynamicFieldDefinition { Id = "name", Name = "name", DefaultValue = "Default" },
                new DynamicFieldDefinition { Id = "secret", Name = "secret", Visible = false },
                new DynamicFieldDefinition { Id = "locked", Name = "locked", ReadOnly = true }
            ]
        };

        FormState state = new FormStateService().Create(schema);

        Assert.Equal("Default", state.Values["name"]);
        Assert.Contains("secret", state.Hidden);
        Assert.Contains("locked", state.ReadOnly);
    }

    [Fact]
    public void SetValue_ShouldMarkFieldAsDirtyAndTouched()
    {
        FormState state = new();

        new FormStateService().SetValue(state, "name", "Updated");

        Assert.Equal("Updated", state.Values["name"]);
        Assert.Contains("name", state.Dirty);
        Assert.Contains("name", state.Touched);
    }
}
