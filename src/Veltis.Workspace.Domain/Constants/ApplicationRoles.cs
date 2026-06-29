namespace Veltis.Workspace.Domain.Constants;

public static class ApplicationRoles
{
    public const string Administrator = "Administrator";
    public const string User = "User";
    public const string Professional = "Professional";

    public static readonly string[] All =
    [
        Administrator,
        User,
        Professional
    ];
}
