using DomeGym.Section4.Domain.ParticipantAggregate;
using DomeGym.Section4.Domain.SessionAggregate;
using DomeGym.Section4.Domain.UnitTests.TestUtils.Common;
using DomeGym.Section4.Domain.UnitTests.TestUtils.Participants;
using DomeGym.Section4.Domain.UnitTests.TestUtils.Sessions;
using ErrorOr;

namespace DomeGym.Section4.Domain.UnitTests.ParticipantAggregate;

public class ParticipantTests
{
    [Theory]
    [InlineData(1, 3, 1, 3)] // exact overlap
    [InlineData(1, 3, 2, 3)] // second session inside first session
    [InlineData(1, 3, 2, 4)] // second session ends after first session, but overlaps
    [InlineData(1, 3, 0, 2)] // second session starts before first session, but overlaps
    public void AddToSchedule_WhenSessionOverlapsWithAnotherSession_ShouldFail(
        int startHourSession1,
        int endHourSession1,
        int startHourSession2,
        int endHourSession2)
    {
        // Arrange
        Participant participant = ParticipantFactory.CreateParticipant();

        Session session1 = SessionFactory.CreateSession(
            date: Constants.Session.Date,
            time: TimeRangeFactory.CreateFromHours(startHourSession1, endHourSession1),
            id: Guid.NewGuid());

        Session session2 = SessionFactory.CreateSession(
            date: Constants.Session.Date,
            time: TimeRangeFactory.CreateFromHours(startHourSession2, endHourSession2),
            id: Guid.NewGuid());

        // Act
        ErrorOr<Success> addSession1Result = participant.AddToSchedule(session1);
        ErrorOr<Success> addSession2Result = participant.AddToSchedule(session2);

        // Assert
        Assert.False(addSession1Result.IsError);

        Assert.True(addSession2Result.IsError);
        Assert.Equal(ParticipantErrors.CannotHaveTwoOrMoreOverlappingSessions, addSession2Result.FirstError);
    }
}
