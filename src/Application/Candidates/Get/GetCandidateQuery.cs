using Application.Abstractions.Messaging;

namespace Application.Candidates.Get;

public sealed record GetCandidateQuery(Guid CandidateId) : IQuery<CandidateResponse>;
