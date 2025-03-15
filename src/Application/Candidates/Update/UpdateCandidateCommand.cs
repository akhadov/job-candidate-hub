using Application.Abstractions.Messaging;

namespace Application.Candidates.Update;

public sealed class UpdateCandidateCommand() : ICommand<Guid>
{
    public Guid CandidateId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string Email { get; set; }
    public TimeSpan? PreferredCallStart { get; set; }
    public TimeSpan? PreferredCallEnd { get; set; }
    public string? LinkedIn { get; set; }
    public string? GitHub { get; set; }
    public string Notes { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
