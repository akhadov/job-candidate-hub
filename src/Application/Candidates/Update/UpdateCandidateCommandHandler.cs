using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Canditates;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Candidates.Update;

internal sealed class UpdateCandidateCommandHandler(
    IApplicationDbContext context,
    IDateTimeProvider dateTimeProvider)
    : ICommandHandler<UpdateCandidateCommand, Guid>
{
    public async Task<Result<Guid>> Handle(UpdateCandidateCommand request, CancellationToken cancellationToken)
    {
        Candidate? candidate = await context.Candidates
            .SingleOrDefaultAsync(t => t.Id == request.CandidateId, cancellationToken);

        if (candidate is null)
        {
            return Result.Failure<Guid>(CandidateErrors.NotFound(request.CandidateId));
        }

        candidate.FirstName = request.FirstName;
        candidate.LastName = request.LastName;
        candidate.PhoneNumber = request.PhoneNumber;
        candidate.Email = request.Email;
        candidate.PreferredCallStart = request.PreferredCallStart;
        candidate.PreferredCallEnd = request.PreferredCallEnd;
        candidate.LinkedIn = request.LinkedIn;
        candidate.GitHub = request.GitHub;
        candidate.Notes = request.Notes;
        candidate.UpdatedAt = dateTimeProvider.UtcNow;

        context.Candidates.Update(candidate);

        await context.SaveChangesAsync(cancellationToken);

        return candidate.Id;
    }
}
