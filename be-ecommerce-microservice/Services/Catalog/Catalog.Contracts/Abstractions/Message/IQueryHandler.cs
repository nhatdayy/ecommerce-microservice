﻿using MediatR;
using Catalog.Contracts.Abstractions.Shared;

namespace Catalog.Contracts.Abstractions.Message;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{ }
