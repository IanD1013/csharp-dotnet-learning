using DomeGym.Section5.Domain.Common.ValueObjects;
using DomeGym.Section5.Domain.SessionAggregate;

namespace DomeGym.Section5.Domain.UnitTests.TestUtils.Sessions;

public static class SessionFactory
{
    public static Session CreateSession(
        string name = Constants.Session.Name,
        string description = Constants.Session.Description,
        int maxParticipants = Constants.Session.MaxParticipants,
        Guid? roomId = null,
        Guid? trainerId = null,
        DateOnly? date = null,
        TimeRange? time = null,
        IEnumerable<SessionCategory>? categories = null,
        Guid? id = null)
    {
        return new Session(
            name: name,
            description: description,
            maxParticipants: maxParticipants,
            roomId: roomId ?? Constants.Room.Id,
            trainerId: trainerId ?? Constants.Trainer.Id,
            date: date ?? Constants.Session.Date,
            time: time ?? Constants.Session.Time,
            categories: categories ?? Constants.Session.Categories,
            id: id ?? Constants.Session.Id);
    }
}
