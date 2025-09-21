using Catalog.Application.Features.Brands;
using Catalog.Application.Features.Products;
using Catalog.Contracts.Dtos.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers;
public class BrandController : ApiBaseController
{
    private readonly IMediator _mediator;
    public BrandController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("all", Name = "GetAllBrands")]
    [ProducesResponseType(typeof(IEnumerable<BrandResponse>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<BrandResponse>>> GetAll()
    {
        var products = await _mediator.Send(new GetAllBrandsQuery());
        return Ok(products);
    }
    [HttpPost("create", Name = "CreateBrand")]
    [ProducesResponseType(typeof(BrandResponse), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<BrandResponse>> Create(string name)
    {
        var result = await _mediator.Send(new CreateBrandCommand(name));
        if (result.IsFailure)
            return BadRequest(result.Error);
        return CreatedAtRoute("GetAllBrands", null, result.Value);
    }
    [HttpGet("{id}", Name = "GetBrandById")]
    [ProducesResponseType(typeof(BrandResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<BrandResponse>> GetBrandById(string id)
    {
        var result = await _mediator.Send(new GetBrandByIdQuery(id));
        if (result.IsFailure)
            return NotFound(result.Error);
        return Ok(result.Value);
    }
    [HttpDelete("{id}", Name = "DeleteBrand")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> DeleteBrand(string id)
    {
        var result = await _mediator.Send(new DeleteBrandCommand(id));
        if (result.IsFailure)
            return NotFound(result.Error);
        return NoContent();
    }
    [HttpPut("{id}", Name = "UpdateBrand")]
    [ProducesResponseType(typeof(BrandResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<BrandResponse>> UpdateBrand(string id, string name)
    {
        var result = await _mediator.Send(new UpdateBrandCommand(id, name));
        if (result.IsFailure)
        {
            if (result.Error == Catalog.Contracts.Abstractions.Shared.Error.NotFound)
                return NotFound(result.Error);
            return BadRequest(result.Error);
        }
        return Ok(result.Value);
    }
}
