using SharedKernel;

namespace Domain.Canditates;

public sealed class Candidate : Entity
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
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
