using DomeGym.Section5.Domain.RoomAggregate;
using DomeGym.Section5.Domain.SessionAggregate;
using DomeGym.Section5.Domain.UnitTests.TestUtils.Common;
using DomeGym.Section5.Domain.UnitTests.TestUtils.Rooms;
using DomeGym.Section5.Domain.UnitTests.TestUtils.Sessions;
using ErrorOr;

namespace DomeGym.Section5.Domain.UnitTests.RoomAggregate;

public class RoomTests
{
    [Fact]
    public void ScheduleSession_WhenMoreThanSubscriptionAllows_ShouldFail()
    {
        // Arrange
        Room room = RoomFactory.CreateRoom(maxDailySessions: 1);

        Session firstDailySession = SessionFactory.CreateSession(
            date: Constants.Session.Date,
            time: TimeRangeFactory.CreateFromHours(1, 2),
            id: Guid.NewGuid());

        Session secondDailySession = SessionFactory.CreateSession(
            date: Constants.Session.Date,
            time: TimeRangeFactory.CreateFromHours(3, 4),
            id: Guid.NewGuid());

        Session sessionOnAnotherDay = SessionFactory.CreateSession(
            date: Constants.Session.Date.AddDays(1),
            time: TimeRangeFactory.CreateFromHours(1, 2),
            id: Guid.NewGuid());

        // Act
        ErrorOr<Success> scheduleFirstSessionResult = room.ScheduleSession(firstDailySession);
        ErrorOr<Success> scheduleSecondSessionResult = room.ScheduleSession(secondDailySession);
        ErrorOr<Success> scheduleSessionOnAnotherDayResult = room.ScheduleSession(sessionOnAnotherDay);

        // Assert
        Assert.False(scheduleFirstSessionResult.IsError);
        Assert.False(scheduleSessionOnAnotherDayResult.IsError);

        Assert.True(scheduleSecondSessionResult.IsError);
        Assert.Equal(RoomErrors.CannotHaveMoreSessionsThanSubscriptionAllows, scheduleSecondSessionResult.FirstError);
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
