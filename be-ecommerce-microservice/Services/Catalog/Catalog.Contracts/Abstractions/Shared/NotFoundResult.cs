

namespace Catalog.Contracts.Abstractions.Shared;

public class NotFoundResult : Result
{
    public NotFoundResult(string identifier) : base(false, new Error("Error.NotFound", $"The resource with identifier '{identifier}' not found."))
    {

    }
}

public class NotFoundResult<TValue> : Result<TValue>
{
    public NotFoundResult(string identifier) : base(default, false, new Error("Error.NotFound", $"The resource with identifier '{identifier}' not found."))
    {

    }
}

