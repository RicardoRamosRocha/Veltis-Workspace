namespace Veltis.Workspace.Web.Models.Dashboard;

public sealed class DashboardViewModel
{
    public string UserName { get; set; } = string.Empty;
    public string ProfessionName { get; set; } = string.Empty;
    public string WorkspaceName { get; set; } = string.Empty;
    public int DocumentsCount { get; set; }
    public int AgentsCount { get; set; }
    public int TemplatesCount { get; set; }
    public int HistoryCount { get; set; }
}
