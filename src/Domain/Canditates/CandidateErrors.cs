using SharedKernel;

namespace Domain.Canditates;

public static class CandidateErrors
{
    public static Error NotFound(Guid candidateId) => Error.NotFound(
        "Candidates.NotFound",
        $"The candidate with the Id = '{candidateId}' was not found");
}
