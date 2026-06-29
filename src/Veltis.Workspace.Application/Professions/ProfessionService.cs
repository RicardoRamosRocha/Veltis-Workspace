using Microsoft.EntityFrameworkCore;
using Veltis.Workspace.Application.Common.Interfaces;
using Veltis.Workspace.Application.Common.Results;
using Veltis.Workspace.Domain.Entities;

namespace Veltis.Workspace.Application.Professions;

public sealed class ProfessionService : IProfessionService
{
    private readonly IApplicationDbContext _context;

    public ProfessionService(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<ProfessionDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Professions
            .Where(profession => !profession.IsDeleted)
            .OrderBy(profession => profession.Name)
            .Select(profession => new ProfessionDto(
                profession.Id,
                profession.Name,
                profession.Description,
                profession.Icon,
                profession.Color,
                profession.Slug,
                profession.Active))
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<ProfessionDto>> GetActiveAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Professions
            .Where(profession => !profession.IsDeleted && profession.Active)
            .OrderBy(profession => profession.Name)
            .Select(profession => new ProfessionDto(
                profession.Id,
                profession.Name,
                profession.Description,
                profession.Icon,
                profession.Color,
                profession.Slug,
                profession.Active))
            .ToListAsync(cancellationToken);
    }

    public async Task<ProfessionDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Profession? profession = await _context.Professions
            .FirstOrDefaultAsync(item => item.Id == id && !item.IsDeleted, cancellationToken);

        return profession?.ToDto();
    }

    public async Task<Result<Guid>> CreateAsync(ProfessionInputDto input, CancellationToken cancellationToken = default)
    {
        bool exists = await _context.Professions.AnyAsync(
            profession => profession.Slug == input.Slug && !profession.IsDeleted,
            cancellationToken);

        if (exists)
        {
            return Result<Guid>.Failure("Ja existe uma profissao com este slug.");
        }

        var profession = new Profession
        {
            Name = input.Name.Trim(),
            Description = input.Description?.Trim(),
            Icon = input.Icon?.Trim(),
            Color = input.Color?.Trim(),
            Slug = input.Slug.Trim().ToLowerInvariant(),
            Active = input.Active
        };

        _context.Professions.Add(profession);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(profession.Id);
    }

    public async Task<Result> UpdateAsync(Guid id, ProfessionInputDto input, CancellationToken cancellationToken = default)
    {
        Profession? profession = await _context.Professions.FirstOrDefaultAsync(
            item => item.Id == id && !item.IsDeleted,
            cancellationToken);

        if (profession is null)
        {
            return Result.Failure("Profissao nao encontrada.");
        }

        bool slugInUse = await _context.Professions.AnyAsync(
            item => item.Id != id && item.Slug == input.Slug && !item.IsDeleted,
            cancellationToken);

        if (slugInUse)
        {
            return Result.Failure("Ja existe uma profissao com este slug.");
        }

        profession.Name = input.Name.Trim();
        profession.Description = input.Description?.Trim();
        profession.Icon = input.Icon?.Trim();
        profession.Color = input.Color?.Trim();
        profession.Slug = input.Slug.Trim().ToLowerInvariant();
        profession.Active = input.Active;

        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }

    public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Profession? profession = await _context.Professions.FirstOrDefaultAsync(
            item => item.Id == id && !item.IsDeleted,
            cancellationToken);

        if (profession is null)
        {
            return Result.Failure("Profissao nao encontrada.");
        }

        profession.MarkAsDeleted(DateTime.UtcNow);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
