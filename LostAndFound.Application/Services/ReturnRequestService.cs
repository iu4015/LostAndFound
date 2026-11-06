using LostAndFound.Application.DTOs;
using LostAndFound.Application.Errors;
using LostAndFound.Application.Repositories;
using LostAndFound.Domain;

namespace LostAndFound.Application.Services;

public sealed class ReturnRequestService(
    IFoundItemRepository itemRepository,
    IReturnRequestRepository requestRepository)
{
    public async Task<ReturnRequestResponse> SubmitAsync(
        Guid foundItemId,
        SubmitReturnRequest request,
        CancellationToken cancellationToken)
    {
        var item = await itemRepository.GetByIdAsync(foundItemId, cancellationToken)
            ?? throw new ResourceNotFoundException($"Found item {foundItemId} was not found.");

        try
        {
            item.EnsureAvailable();
        }
        catch (DomainRuleException exception)
        {
            throw new ConflictException(exception.Message);
        }

        var returnRequest = new ReturnRequest(
            foundItemId,
            request.ContactDetails,
            request.OwnerExplanation);

        await requestRepository.AddAsync(returnRequest, cancellationToken);
        await requestRepository.SaveChangesAsync(cancellationToken);
        return Map(returnRequest);
    }

    public static ReturnRequestResponse Map(ReturnRequest request) => new(
        request.Id,
        request.FoundItemId,
        request.ContactDetails,
        request.OwnerExplanation,
        request.Status,
        request.CreatedAt);

    public Task<ReturnRequestResponse> ApproveAsync(
        Guid requestId,
        CancellationToken cancellationToken) => ReviewAsync(requestId, approve: true, cancellationToken);

    public Task<ReturnRequestResponse> RejectAsync(
        Guid requestId,
        CancellationToken cancellationToken) => ReviewAsync(requestId, approve: false, cancellationToken);

    private async Task<ReturnRequestResponse> ReviewAsync(
        Guid requestId,
        bool approve,
        CancellationToken cancellationToken)
    {
        var request = await requestRepository.GetByIdWithItemAsync(requestId, cancellationToken)
            ?? throw new ResourceNotFoundException($"Return request {requestId} was not found.");

        try
        {
            if (approve)
            {
                request.FoundItem.EnsureAvailable();
                request.Approve();
                request.FoundItem.MarkReturned();
            }
            else
            {
                request.Reject();
            }
        }
        catch (DomainRuleException exception)
        {
            throw new ConflictException(exception.Message);
        }

        await requestRepository.SaveChangesAsync(cancellationToken);
        return Map(request);
    }
}
