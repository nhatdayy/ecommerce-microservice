using MediatR;
using Catalog.Contracts.Abstractions.Shared;

namespace Catalog.Contracts.Abstractions.Message;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result> where TCommand : ICommand { }

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>> where TCommand : ICommand<TResponse> { }
