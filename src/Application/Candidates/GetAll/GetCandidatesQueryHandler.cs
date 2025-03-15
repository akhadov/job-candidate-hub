using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Abstractions.Models;
using SharedKernel;

namespace Application.Candidates.GetAll;

internal sealed class GetCandidatesQueryHandler(
    IApplicationDbContext context)
    : IQueryHandler<GetCandidatesQuery, PaginatedList<CandidatesResponse>>
{
    public async Task<Result<PaginatedList<CandidatesResponse>>> Handle(GetCandidatesQuery request, CancellationToken cancellationToken)
    {
        IQueryable<CandidatesResponse> query = context.Candidates
            .OrderBy(x => x.PreferredCallStart)
            .Select(x => new CandidatesResponse
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber,
                Email = x.Email,
                PreferredCallStart = x.PreferredCallStart,
                PreferredCallEnd = x.PreferredCallEnd,
                LinkedIn = x.LinkedIn,
                GitHub = x.GitHub,
                Notes = x.Notes,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            });

        PaginatedList<CandidatesResponse> paginatedList = await PaginatedList<CandidatesResponse>.CreateAsync(
            query,
            request.PageNumber,
            request.PageSize);

        return Result.Success(paginatedList);
    }
}
