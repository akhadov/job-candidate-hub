using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Abstractions.Models;
using Application.Candidates.Get;
using Domain.Canditates;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Candidates.Search;

internal sealed class SearchCandidatesQueryHandler(IApplicationDbContext context)
    : IQueryHandler<SearchCandidatesQuery, PaginatedList<CandidateResponse>>
{
    public async Task<Result<PaginatedList<CandidateResponse>>> Handle(SearchCandidatesQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Candidate> query = context.Candidates;

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            query = query.Where(c =>
                EF.Functions.Like(c.FirstName, $"%{request.SearchTerm}%") ||
                EF.Functions.Like(c.LastName, $"%{request.SearchTerm}%") ||
                EF.Functions.Like(c.Email, $"%{request.SearchTerm}%") ||
                c.PhoneNumber != null && EF.Functions.Like(c.PhoneNumber, $"%{request.SearchTerm}%") ||
                c.LinkedIn != null && EF.Functions.Like(c.LinkedIn, $"%{request.SearchTerm}%") ||
                c.GitHub != null && EF.Functions.Like(c.GitHub, $"%{request.SearchTerm}%")
            );
        }

        IQueryable<CandidateResponse> projectedQuery = query
            .OrderByDescending(c => c.CreatedAt)
            .Select(c => new CandidateResponse
            {
                Id = c.Id,
                Email = c.Email,
                FirstName = c.FirstName,
                LastName = c.LastName,
                PhoneNumber = c.PhoneNumber,
                PreferredCallStart = c.PreferredCallStart,
                PreferredCallEnd = c.PreferredCallEnd,
                LinkedIn = c.LinkedIn,
                GitHub = c.GitHub,
                Notes = c.Notes,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            });

        PaginatedList<CandidateResponse> paginatedList = await PaginatedList<CandidateResponse>.CreateAsync(
            projectedQuery,
            request.PageNumber,
            request.PageSize);

        return Result.Success(paginatedList);
    }
}
