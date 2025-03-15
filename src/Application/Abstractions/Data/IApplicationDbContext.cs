using Domain.Canditates;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<Candidate> Candidates { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
