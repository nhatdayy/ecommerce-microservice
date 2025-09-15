using Catalog.Application.Features.Products;
using Catalog.Contracts.Dtos.Responses;
using Catalog.Core.Abstractions;
using Contract.Abstarctions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatalogController : ApiBaseController
{
    private readonly IMediator _mediator;

    public CatalogController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet("{id}", Name = "GetProductById")]
    [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<ProductResponse>> GetProductById(string id)
    {
        var product = await _mediator.Send(new GetProductByIdQuery(id));
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpGet("all", Name = "GetAllProducts")]
    [ProducesResponseType(typeof(IEnumerable<ProductResponse>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAll()
    {
        var products = await _mediator.Send(new GetAllProductsQuery());
        return Ok(products);
    }

    [HttpGet("pagination", Name = "GetAllProductsPagination")]
    [ProducesResponseType(typeof(PaginationResult<ProductResponse>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<PaginationResult<ProductResponse>>> GetAllPagination(
        [FromQuery] PaginationRequest request)
    {
        var query = new GetAllProductsPaginationQuery(request);
        var pagedProducts = await _mediator.Send(query);
        return Ok(pagedProducts);
    }

    [HttpPost("create", Name = "CreateProduct")]
    [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<ProductResponse>> Create([FromBody] CreateProductCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return CreatedAtRoute("GetProductById", new { id = result.Value.Id }, result.Value);
    }

    [HttpPut("update", Name = "UpdateProduct")]
    [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<ProductResponse>> Update([FromBody] UpdateProductCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }


    [HttpDelete("{id}", Name = "DeleteProduct")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _mediator.Send(new DeleteProductCommand(id));

        return NoContent();
    }
}
