using Application.Abstractions.Messaging;
using Application.Abstractions.Models;
using Application.Candidates.Get;

namespace Application.Candidates.Search;

public sealed record SearchCandidatesQuery(string? SearchTerm, int PageNumber = 1, int PageSize = 10) : IQuery<PaginatedList<CandidateResponse>>;
