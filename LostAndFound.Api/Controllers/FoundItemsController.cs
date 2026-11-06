using LostAndFound.Application.DTOs;
using LostAndFound.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Api.Controllers;

[ApiController]
[Route("api/found-items")]
public sealed class FoundItemsController(
    FoundItemService service,
    ReturnRequestService returnRequestService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(FoundItemResponse), StatusCodes.Status201Created)]
    public async Task<ActionResult<FoundItemResponse>> Register(
        RegisterFoundItemRequest request,
        CancellationToken cancellationToken)
    {
        var response = await service.RegisterAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<FoundItemResponse>>> Search(
        [FromQuery] SearchFoundItemsRequest request,
        CancellationToken cancellationToken)
    {
        return Ok(await service.SearchAsync(request, cancellationToken));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<FoundItemResponse>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        return Ok(await service.GetByIdAsync(id, cancellationToken));
    }

    [HttpPost("{foundItemId:guid}/return-requests")]
    [ProducesResponseType(typeof(ReturnRequestResponse), StatusCodes.Status201Created)]
    public async Task<ActionResult<ReturnRequestResponse>> SubmitReturnRequest(
        Guid foundItemId,
        SubmitReturnRequest request,
        CancellationToken cancellationToken)
    {
        var response = await returnRequestService.SubmitAsync(
            foundItemId,
            request,
            cancellationToken);

        return StatusCode(StatusCodes.Status201Created, response);
    }
}
