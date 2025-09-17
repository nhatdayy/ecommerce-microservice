using Catalog.Application.Features.Products;
using Catalog.Application.Features.Types;
using Catalog.Contracts.Dtos.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers;


public class TypeController : ApiBaseController
{
    private readonly IMediator _mediator;

    public TypeController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    [HttpGet("{id}", Name = "GetTypeById")]
    [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<ProductTypeResponse>> GetTypeById(string id)
    {
        var type = await _mediator.Send(new GetTypeByIdQuery(id));
        if (type == null) return NotFound();
        return Ok(type);
    }
    [HttpDelete("{id}", Name = "DeleteType")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<bool>> DeleteType(string id)
    {
        var result = await _mediator.Send(new DeleteTypeCommand(id));
        if (result == null) return NotFound();
        return Ok(result);
    }
    [HttpGet("all", Name = "GetAllTypes")]
    [ProducesResponseType(typeof(List<ProductTypeResponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<List<ProductTypeResponse>>> GetAllTypes()
    {
        var types = await _mediator.Send(new GetAllTypesQuery());
        if (types == null) return NotFound();
        return Ok(types);
    }
    [HttpPost("create", Name = "CreateType")]
    [ProducesResponseType(typeof(ProductTypeResponse), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<ProductTypeResponse>> Create([FromBody] CreateTypeCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return CreatedAtRoute("GetTypeById", new { id = result.Value.Id }, result.Value);
    }
    [HttpPut("update", Name = "UpdateType")]
    [ProducesResponseType(typeof(ProductTypeResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<ProductTypeResponse>> Update([FromBody] UpdateTypeCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
}
