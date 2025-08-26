namespace Catalog.Core.Abstractions;

public class PaginationResult<T> where T : class
{
    public PaginationResult(int pageNumber, int pageSize, int total, IEnumerable<T> data)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        Total = total;
        Data = data;
    }

    public int PageNumber { get; private set; }
    public int PageSize { get; private set; }
    public int TotalPages => (int)Math.Ceiling((double)Total / PageSize);
    public int Total { get; private set; }
    public IEnumerable<T> Data { get; private set; } = [];

    public PaginationResult<TDto> ToDto<TDto>(Func<T, TDto> mapFunc) where TDto : class
    {
        var dtoData = Data.Select(mapFunc);
        return new PaginationResult<TDto>(PageNumber, PageSize, Total, dtoData);
    }
}