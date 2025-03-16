using Application.Candidates.Update;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Candidates;

internal sealed class Update : IEndpoint
{
    public sealed class UpdateRequest
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string Email { get; set; }
        public TimeSpan? PreferredCallStart { get; set; }
        public TimeSpan? PreferredCallEnd { get; set; }
        public string? LinkedIn { get; set; }
        public string? GitHub { get; set; }
        public string Notes { get; set; }
    }
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("candidates", async (UpdateRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new UpdateCandidateCommand
            {
                CandidateId = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                PreferredCallStart = request.PreferredCallStart,
                PreferredCallEnd = request.PreferredCallEnd,
                LinkedIn = request.LinkedIn,
                GitHub = request.GitHub,
                Notes = request.Notes
            };

            Result<Guid> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Candidate);
    }
}
