using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Candidates.Delete;

internal sealed class DeleteCandidateCommandValidator : AbstractValidator<DeleteCandidateCommand>
{
    public DeleteCandidateCommandValidator()
    {
        RuleFor(x => x.CandidateId).NotEmpty();
    }
}
