using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Canditates;
using SharedKernel;

namespace Application.Candidates.Create;

internal sealed class CreateCandidateCommandHandler(
    IApplicationDbContext context,
    IDateTimeProvider dateTimeProvider)
    : ICommandHandler<CreateCandidateCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
    {
        ///Candidate? candidate = await context.Candidates.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        var candidate = new Candidate
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            PreferredCallStart = request.PreferredCallStart,
            PreferredCallEnd = request.PreferredCallEnd,
            LinkedIn = request.LinkedIn,
            GitHub = request.GitHub,
            Notes = request.Notes,
            CreatedAt = dateTimeProvider.UtcNow,
            UpdatedAt = dateTimeProvider.UtcNow
        };

        context.Candidates.Add(candidate);

        await context.SaveChangesAsync(cancellationToken);

        return candidate.Id;
    }
}
