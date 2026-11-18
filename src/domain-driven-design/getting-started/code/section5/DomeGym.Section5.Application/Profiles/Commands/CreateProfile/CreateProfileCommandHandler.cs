using DomeGym.Section5.Application.Common.Interfaces;
using DomeGym.Section5.Domain.AdminAggregate;
using DomeGym.Section5.Domain.ParticipantAggregate;
using DomeGym.Section5.Domain.Profiles;
using DomeGym.Section5.Domain.TrainerAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Section5.Application.Profiles.Commands.CreateProfile;

public class CreateProfileCommandHandler(
    IAdminsRepository adminsRepository,
    ITrainersRepository trainersRepository,
    IParticipantsRepository participantsRepository)
    : IRequestHandler<CreateProfileCommand, ErrorOr<Guid>>
{
    public async Task<ErrorOr<Guid>> Handle(CreateProfileCommand command, CancellationToken cancellationToken)
    {
        return command.ProfileType.Name switch
        {
            nameof(ProfileType.Admin) => await CreateAdminAsync(command.UserId, cancellationToken),
            nameof(ProfileType.Trainer) => await CreateTrainerAsync(command.UserId, cancellationToken),
            nameof(ProfileType.Participant) => await CreateParticipantAsync(command.UserId, cancellationToken),
            _ => throw new InvalidOperationException($"Unknown profile type '{command.ProfileType.Name}'."),
        };
    }

    private async Task<ErrorOr<Guid>> CreateAdminAsync(Guid userId, CancellationToken cancellationToken)
    {
        if (await adminsRepository.GetProfileByUserIdAsync(userId, cancellationToken) is not null)
        {
            return Error.Conflict(description: "User already has an admin profile");
        }

        var admin = new Admin(userId);
        await adminsRepository.AddAdminAsync(admin, cancellationToken);

        return admin.Id;
    }

    private async Task<ErrorOr<Guid>> CreateTrainerAsync(Guid userId, CancellationToken cancellationToken)
    {
        if (await trainersRepository.GetProfileByUserIdAsync(userId, cancellationToken) is not null)
        {
            return Error.Conflict(description: "User already has a trainer profile");
        }

        var trainer = new Trainer(userId);
        await trainersRepository.AddTrainerAsync(trainer, cancellationToken);

        return trainer.Id;
    }

    private async Task<ErrorOr<Guid>> CreateParticipantAsync(Guid userId, CancellationToken cancellationToken)
    {
        if (await participantsRepository.GetProfileByUserIdAsync(userId, cancellationToken) is not null)
        {
            return Error.Conflict(description: "User already has a participant profile");
        }

        var participant = new Participant(userId);
        await participantsRepository.AddParticipantAsync(participant, cancellationToken);

        return participant.Id;
    }
}
