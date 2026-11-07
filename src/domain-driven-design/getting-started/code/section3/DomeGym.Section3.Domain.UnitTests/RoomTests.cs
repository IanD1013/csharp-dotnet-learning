using DomeGym.Section3.Domain.UnitTests.TestUtils.Common;
using DomeGym.Section3.Domain.UnitTests.TestUtils.Rooms;
using DomeGym.Section3.Domain.UnitTests.TestUtils.Sessions;
using ErrorOr;

namespace DomeGym.Section3.Domain.UnitTests;

public class RoomTests
{
    [Fact]
    public void ScheduleSession_WhenMoreThanSubscriptionAllows_ShouldFail()
    {
        // Arrange
        Room room = RoomFactory.CreateRoom(maxDailySessions: 1);

        Session session1 = SessionFactory.CreateSession(id: Guid.NewGuid());
        Session session2 = SessionFactory.CreateSession(id: Guid.NewGuid());

        // Act
        ErrorOr<Success> scheduleSession1Result = room.ScheduleSession(session1);
        ErrorOr<Success> scheduleSession2Result = room.ScheduleSession(session2);

        // Assert
        Assert.False(scheduleSession1Result.IsError);

        Assert.True(scheduleSession2Result.IsError);
        Assert.Equal(RoomErrors.CannotHaveMoreSessionsThanSubscriptionAllows, scheduleSession2Result.FirstError);
    }

    [Theory]
    [InlineData(1, 3, 1, 3)] // exact overlap
    [InlineData(1, 3, 2, 3)] // second session inside first session
    [InlineData(1, 3, 2, 4)] // second session ends after first session, but overlaps
    [InlineData(1, 3, 0, 2)] // second session starts before first session, but overlaps
    public void ScheduleSession_WhenSessionOverlapsWithAnotherSession_ShouldFail(
        int startHourSession1,
        int endHourSession1,
        int startHourSession2,
        int endHourSession2)
    {
        // Arrange
        Room room = RoomFactory.CreateRoom(maxDailySessions: 2);

        Session session1 = SessionFactory.CreateSession(
            date: Constants.Session.Date,
            time: TimeRangeFactory.CreateFromHours(startHourSession1, endHourSession1),
            id: Guid.NewGuid());

        Session session2 = SessionFactory.CreateSession(
            date: Constants.Session.Date,
            time: TimeRangeFactory.CreateFromHours(startHourSession2, endHourSession2),
            id: Guid.NewGuid());

        // Act
        ErrorOr<Success> scheduleSession1Result = room.ScheduleSession(session1);
        ErrorOr<Success> scheduleSession2Result = room.ScheduleSession(session2);

        // Assert
        Assert.False(scheduleSession1Result.IsError);

        Assert.True(scheduleSession2Result.IsError);
        Assert.Equal(RoomErrors.CannotHaveTwoOrMoreOverlappingSessions, scheduleSession2Result.FirstError);
    }
}
