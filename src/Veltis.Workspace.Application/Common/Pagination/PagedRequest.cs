using Veltis.Workspace.Domain.Constants;

namespace Veltis.Workspace.Application.Common.Pagination;

public sealed class PagedRequest
{
    private int _pageNumber = ValidationConstants.MinimumPageNumber;
    private int _pageSize = ValidationConstants.DefaultPageSize;

    public int PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = Math.Max(ValidationConstants.MinimumPageNumber, value);
    }

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = Math.Clamp(value, 1, ValidationConstants.MaxPageSize);
    }
}
