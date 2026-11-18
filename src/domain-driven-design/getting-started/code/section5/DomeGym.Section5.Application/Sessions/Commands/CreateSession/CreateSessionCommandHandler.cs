using DomeGym.Section5.Application.Common.Interfaces;
using DomeGym.Section5.Domain.Common.ValueObjects;
using DomeGym.Section5.Domain.RoomAggregate;
using DomeGym.Section5.Domain.SessionAggregate;
using DomeGym.Section5.Domain.TrainerAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Section5.Application.Sessions.Commands.CreateSession;

public class CreateSessionCommandHandler(IRoomsRepository roomsRepository, ITrainersRepository trainersRepository)
    : IRequestHandler<CreateSessionCommand, ErrorOr<Session>>
{
    public async Task<ErrorOr<Session>> Handle(CreateSessionCommand command, CancellationToken cancellationToken)
    {
        Room? room = await roomsRepository.GetByIdAsync(command.RoomId, cancellationToken);

        if (room is null)
        {
            return Error.NotFound(description: "Room not found");
        }

        Trainer? trainer = await trainersRepository.GetByIdAsync(command.TrainerId, cancellationToken);

        if (trainer is null)
        {
            return Error.NotFound(description: "Trainer not found");
        }

        ErrorOr<TimeRange> createTimeRangeResult = TimeRange.FromDateTimes(command.StartDateTime, command.EndDateTime);

        if (createTimeRangeResult.IsError)
        {
            return createTimeRangeResult.Errors;
        }

        var date = DateOnly.FromDateTime(command.StartDateTime);

        if (!trainer.IsTimeSlotFree(date, createTimeRangeResult.Value))
        {
            return Error.Conflict(description: "Trainer's calendar is not free for the entire session duration");
        }

        var session = new Session(
            name: command.Name,
            description: command.Description,
            maxParticipants: command.MaxParticipants,
            roomId: command.RoomId,
            trainerId: command.TrainerId,
            date: date,
            time: createTimeRangeResult.Value,
            categories: command.Categories);

        ErrorOr<Success> scheduleSessionResult = room.ScheduleSession(session);

        if (scheduleSessionResult.IsError)
        {
            return scheduleSessionResult.Errors;
        }

        await roomsRepository.UpdateAsync(room, cancellationToken);

        return session;
    }
}
