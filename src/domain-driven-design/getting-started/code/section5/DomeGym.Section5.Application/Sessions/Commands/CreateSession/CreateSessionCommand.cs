using DomeGym.Section5.Domain.SessionAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Section5.Application.Sessions.Commands.CreateSession;

public record CreateSessionCommand(
    Guid RoomId,
    string Name,
    string Description,
    int MaxParticipants,
    DateTime StartDateTime,
    DateTime EndDateTime,
    Guid TrainerId,
    IReadOnlyList<SessionCategory> Categories) : IRequest<ErrorOr<Session>>;
