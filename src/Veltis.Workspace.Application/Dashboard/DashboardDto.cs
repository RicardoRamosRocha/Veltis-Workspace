namespace Veltis.Workspace.Application.Dashboard;

public sealed record DashboardDto(
    string UserName,
    string ProfessionName,
    string WorkspaceName,
    int DocumentsCount,
    int AgentsCount,
    int TemplatesCount,
    int HistoryCount);
