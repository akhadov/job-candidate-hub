using FluentValidation;

namespace Application.Candidates.Delete;

internal sealed class DeleteCandidateCommandValidator : AbstractValidator<DeleteCandidateCommand>
{
    public DeleteCandidateCommandValidator()
    {
        RuleFor(x => x.CandidateId).NotEmpty();
    }
}
