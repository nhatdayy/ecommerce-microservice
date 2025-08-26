namespace Catalog.Core.Abstractions;

public class DbOperationResult
{
    protected DbOperationResult(string? error)
    {
        Error = error;
    }

    public string? Error { get; protected set; }
    public bool IsSuccess
    {
        get
        {
            return string.IsNullOrEmpty(Error);
        }
    }

    public static DbOperationResult Failure(string? error) { return new DbOperationResult(error); }
    public static DbOperationResult Success() { return new DbOperationResult(null); }
}

public class DbOperationResult<TResult> : DbOperationResult where TResult : class
{
    private DbOperationResult(TResult? data, string? error) : base(error)
    {
        this.data = data;
    }

    private TResult? data;

    public TResult Data
    {
        get
        {
            if (data == null || !IsSuccess)
                throw new ArgumentException();

            return data;
        }
    }

    public static DbOperationResult<TResult> Success(TResult data) { return new DbOperationResult<TResult>(data, null); }
}
