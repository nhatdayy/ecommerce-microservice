using MediatR;
using Catalog.Contracts.Abstractions.Shared;

namespace Catalog.Contracts.Abstractions.Message;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
