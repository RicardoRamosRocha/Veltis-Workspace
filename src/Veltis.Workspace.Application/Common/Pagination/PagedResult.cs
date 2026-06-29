namespace Veltis.Workspace.Application.Common.Pagination;

public sealed class PagedResult<T>
{
    private PagedResult(IReadOnlyCollection<T> items, PaginationMetadata metadata)
    {
        Items = items;
        Metadata = metadata;
    }

    public IReadOnlyCollection<T> Items { get; }
    public PaginationMetadata Metadata { get; }

    public int PageNumber => Metadata.PageNumber;
    public int PageSize => Metadata.PageSize;
    public int TotalItems => Metadata.TotalItems;
    public int TotalPages => Metadata.TotalPages;
    public bool HasPreviousPage => Metadata.HasPreviousPage;
    public bool HasNextPage => Metadata.HasNextPage;

    public static PagedResult<T> Create(IEnumerable<T> items, int totalItems, int pageNumber, int pageSize)
    {
        int safePageNumber = Math.Max(1, pageNumber);
        int safePageSize = Math.Clamp(pageSize, 1, 100);
        int totalPages = totalItems == 0 ? 0 : (int)Math.Ceiling(totalItems / (double)safePageSize);

        var metadata = new PaginationMetadata(
            safePageNumber,
            safePageSize,
            totalItems,
            totalPages,
            safePageNumber > 1,
            totalPages > 0 && safePageNumber < totalPages);

        return new PagedResult<T>(items.ToArray(), metadata);
    }
}
