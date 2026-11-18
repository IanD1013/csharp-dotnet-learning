using ErrorOr;
using MediatR;

namespace DomeGym.Section5.Application.Reservations.Commands.CreateReservation;

public record CreateReservationCommand(Guid SessionId, Guid ParticipantId) : IRequest<ErrorOr<Success>>;
