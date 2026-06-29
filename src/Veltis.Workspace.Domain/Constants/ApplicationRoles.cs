namespace Veltis.Workspace.Domain.Constants;

public static class ApplicationRoles
{
    public const string Administrator = "Administrator";
    public const string Gestor = "Gestor";
    public const string User = "User";
    public const string Professional = "Professional";
    public const string AdminAccess = Administrator + "," + Gestor;

    public static readonly string[] All =
    [
        Administrator,
        Gestor,
        User,
        Professional
    ];
}
