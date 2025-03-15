using Application.Abstractions.Messaging;

namespace Application.Candidates.Delete;

public sealed record DeleteCandidateCommand(Guid CandidateId) : ICommand;
