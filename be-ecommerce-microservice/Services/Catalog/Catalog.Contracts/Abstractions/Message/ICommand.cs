using MediatR;
using Catalog.Contracts.Abstractions.Shared;

namespace Catalog.Contracts.Abstractions.Message;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
