using Application.Abstractions.Models;
using Application.Candidates.GetAll;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Candidates;

internal sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("candidates", async (int pageNumber, int pageSize, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new GetCandidatesQuery(pageNumber, pageSize);

            Result<PaginatedList<CandidatesResponse>> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Candidate);
    }
}
