using SharedKernel;

namespace Domain.Canditates;

public static class CandidateErrors
{
    public static Error NotFound(Guid candidateId) => Error.NotFound(
        "Candidates.NotFound",
        $"The candidate with the Id = '{candidateId}' was not found");

    public static readonly Error NotFoundByEmail = Error.NotFound(
        "Candidates.NotFoundByEmail",
        "The candidate with the specified email was not found");

    public static readonly Error EmailNotUnique = Error.Conflict(
        "Candidates.EmailNotUnique",
        "The provided email is not unique");
}
