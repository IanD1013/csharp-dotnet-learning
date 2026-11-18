using ErrorOr;
using MediatR;

namespace DomeGym.Section5.Application.Gyms.Commands.AddTrainer;

public record AddTrainerCommand(Guid SubscriptionId, Guid GymId, Guid TrainerId) : IRequest<ErrorOr<Success>>;
