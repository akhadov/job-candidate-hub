using Application.Abstractions.Messaging;
using Application.Abstractions.Models;

namespace Application.Candidates.GetAll;

public sealed record GetCandidatesQuery(int PageNumber, int PageSize) : IQuery<PaginatedList<CandidatesResponse>>;
