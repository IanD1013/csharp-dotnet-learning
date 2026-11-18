using ErrorOr;
using MediatR;

namespace DomeGym.Section5.Application.Participants.Commands.CancelReservation;

public record CancelReservationCommand(Guid ParticipantId, Guid SessionId) : IRequest<ErrorOr<Deleted>>;
