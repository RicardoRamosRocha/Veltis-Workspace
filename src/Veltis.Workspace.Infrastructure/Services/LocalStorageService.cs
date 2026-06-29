using Microsoft.Extensions.Configuration;
using Veltis.Workspace.Application.Common.Interfaces;

namespace Veltis.Workspace.Infrastructure.Services;

public sealed class LocalStorageService : IStorageService, IFileStorageService
{
    private readonly string _rootPath;

    public LocalStorageService(IConfiguration configuration)
    {
        _rootPath = configuration["Storage:LocalPath"] ?? Path.Combine(AppContext.BaseDirectory, "storage");
    }

    public async Task<string> SaveAsync(Stream content, string fileName, string contentType, CancellationToken cancellationToken = default)
    {
        Directory.CreateDirectory(_rootPath);
        string safeName = $"{Guid.NewGuid():N}_{Path.GetFileName(fileName)}";
        string fullPath = Path.Combine(_rootPath, safeName);

        await using FileStream output = File.Create(fullPath);
        await content.CopyToAsync(output, cancellationToken);
        return safeName;
    }

    public Task<Stream?> OpenReadAsync(string storageKey, CancellationToken cancellationToken = default)
    {
        string fullPath = Path.Combine(_rootPath, storageKey);
        Stream? stream = File.Exists(fullPath) ? File.OpenRead(fullPath) : null;
        return Task.FromResult(stream);
    }

    public Task DeleteAsync(string storageKey, CancellationToken cancellationToken = default)
    {
        string fullPath = Path.Combine(_rootPath, storageKey);
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }

        return Task.CompletedTask;
    }
}
