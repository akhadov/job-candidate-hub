using Application.Abstractions.Models;
using Application.Candidates.Get;
using Application.Candidates.Search;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Candidates;

internal sealed class SearchCandidates : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("candidates/search", async (
            string? searchTerm,
            int pageNumber,
            int pageSize,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new SearchCandidatesQuery(searchTerm, pageNumber, pageSize);

            Result<PaginatedList<CandidateResponse>> result = await sender.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Candidate);
    }
}
