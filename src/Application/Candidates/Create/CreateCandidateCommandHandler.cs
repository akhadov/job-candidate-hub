using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Canditates;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Candidates.Create;

internal sealed class CreateCandidateCommandHandler(
    IApplicationDbContext context,
    IDateTimeProvider dateTimeProvider)
    : ICommandHandler<CreateCandidateCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
    {
        Candidate? existingCandidate = await context.Candidates
            .FirstOrDefaultAsync(c => c.Email == request.Email, cancellationToken);

        if (existingCandidate != null)
        {
            existingCandidate.FirstName = request.FirstName;
            existingCandidate.LastName = request.LastName;
            existingCandidate.PhoneNumber = request.PhoneNumber;
            existingCandidate.PreferredCallStart = request.PreferredCallStart;
            existingCandidate.PreferredCallEnd = request.PreferredCallEnd;
            existingCandidate.LinkedIn = request.LinkedIn;
            existingCandidate.GitHub = request.GitHub;
            existingCandidate.Notes = request.Notes;
            existingCandidate.UpdatedAt = dateTimeProvider.UtcNow;

            context.Candidates.Update(existingCandidate);

            await context.SaveChangesAsync(cancellationToken);

            return existingCandidate.Id;
        }

        var candidate = new Candidate
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            PreferredCallStart = request.PreferredCallStart,
            PreferredCallEnd = request.PreferredCallEnd,
            LinkedIn = request.LinkedIn,
            GitHub = request.GitHub,
            Notes = request.Notes,
            CreatedAt = dateTimeProvider.UtcNow
        };

        context.Candidates.Add(candidate);

        await context.SaveChangesAsync(cancellationToken);

        return candidate.Id;
    }
}
