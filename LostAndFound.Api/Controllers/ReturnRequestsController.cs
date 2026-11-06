using LostAndFound.Application.DTOs;
using LostAndFound.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Api.Controllers;

[ApiController]
[Route("api/return-requests")]
public sealed class ReturnRequestsController(ReturnRequestService service) : ControllerBase
{
    [HttpPost("{requestId:guid}/approve")]
    public async Task<ActionResult<ReturnRequestResponse>> Approve(
        Guid requestId,
        CancellationToken cancellationToken)
    {
        return Ok(await service.ApproveAsync(requestId, cancellationToken));
    }

    [HttpPost("{requestId:guid}/reject")]
    public async Task<ActionResult<ReturnRequestResponse>> Reject(
        Guid requestId,
        CancellationToken cancellationToken)
    {
        return Ok(await service.RejectAsync(requestId, cancellationToken));
    }
}
