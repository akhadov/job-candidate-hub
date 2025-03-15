using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Canditates;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Candidates.Get;

internal sealed class GetCandidateQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetCandidateQuery, CandidateResponse>
{
    public async Task<Result<CandidateResponse>> Handle(GetCandidateQuery request, CancellationToken cancellationToken)
    {
        CandidateResponse candidate = await context.Candidates
            .Where(x => x.Id == request.CandidateId)
            .Select(x => new CandidateResponse
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
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (candidate is null)
        {
            return Result.Failure<CandidateResponse>(CandidateErrors.NotFound(request.CandidateId));
        }

        return candidate;
    }
}
