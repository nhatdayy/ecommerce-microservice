namespace Contract.Abstarctions;

public class ResponseViewModel<T>
{
    public T Data { get; set; }
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public string Code { get; set; }

    public ResponseViewModel()
    {
    }

    public ResponseViewModel(string message)
    {
        IsSuccess = false;
        Message = message;
    }

    public ResponseViewModel(T data, string message = null)
    {
        IsSuccess = true;
        Data = data;
        Message = message;
        Code = "01";
    }
}
public class PaginationRequest
{
    public int PageSize { get; set; }
    public int PageIndex { get; set; }
    public string? SearchTerm { get; set; }
    public int Total { get; set; }
}
