using Veltis.Workspace.Application.Common.Pagination;

namespace Veltis.Workspace.Tests.Common;

public sealed class PaginationTests
{
    [Fact]
    public void PagedRequest_Should_Clamp_Invalid_Values()
    {
        var request = new PagedRequest
        {
            PageNumber = -10,
            PageSize = 500
        };

        Assert.Equal(1, request.PageNumber);
        Assert.Equal(100, request.PageSize);
    }

    [Fact]
    public void PagedResult_Should_Calculate_Metadata()
    {
        PagedResult<int> result = PagedResult<int>.Create([1, 2, 3], 25, 2, 10);

        Assert.Equal(2, result.PageNumber);
        Assert.Equal(3, result.TotalPages);
        Assert.True(result.HasPreviousPage);
        Assert.True(result.HasNextPage);
    }
}
