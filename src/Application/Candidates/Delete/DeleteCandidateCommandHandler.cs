using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Canditates;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Candidates.Delete;

internal sealed class DeleteCandidateCommandHandler(IApplicationDbContext context)
    : ICommandHandler<DeleteCandidateCommand>
{
    public async Task<Result> Handle(DeleteCandidateCommand request, CancellationToken cancellationToken)
    {
        Candidate? candidate = await context.Candidates
            .SingleOrDefaultAsync(t => t.Id == request.CandidateId, cancellationToken);

        if (candidate is null)
        {
            return Result.Failure(CandidateErrors.NotFound(request.CandidateId));
        }

        context.Candidates.Remove(candidate);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
