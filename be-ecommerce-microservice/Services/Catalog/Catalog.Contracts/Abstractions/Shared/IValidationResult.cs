namespace Catalog.Contracts.Abstractions.Shared;

public interface IValidationResult
{
    public static readonly Error ValidationError = new("Error.Validation", "A validation problem occured. ");

    Error[] Errors { get; }
}
