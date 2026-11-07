using DomeGym.Section3.Domain.UnitTests.TestUtils.Common;
using DomeGym.Section3.Domain.UnitTests.TestUtils.Sessions;
using DomeGym.Section3.Domain.UnitTests.TestUtils.Trainers;
using ErrorOr;

namespace DomeGym.Section3.Domain.UnitTests;

public class TrainerTests
{
    [Theory]
    [InlineData(1, 3, 1, 3)] // exact overlap
    [InlineData(1, 3, 2, 3)] // second session inside first session
    [InlineData(1, 3, 2, 4)] // second session ends after first session, but overlaps
    [InlineData(1, 3, 0, 2)] // second session starts before first session, but overlaps
    public void AddSessionToSchedule_WhenSessionOverlapsWithAnotherSession_ShouldFail(
        int startHourSession1,
        int endHourSession1,
        int startHourSession2,
        int endHourSession2)
    {
        // Arrange
        Trainer trainer = TrainerFactory.CreateTrainer();

        Session session1 = SessionFactory.CreateSession(
            date: Constants.Session.Date,
            time: TimeRangeFactory.CreateFromHours(startHourSession1, endHourSession1),
            id: Guid.NewGuid());

        Session session2 = SessionFactory.CreateSession(
            date: Constants.Session.Date,
            time: TimeRangeFactory.CreateFromHours(startHourSession2, endHourSession2),
            id: Guid.NewGuid());

        // Act
        ErrorOr<Success> addSession1Result = trainer.AddSessionToSchedule(session1);
        ErrorOr<Success> addSession2Result = trainer.AddSessionToSchedule(session2);

        // Assert
        Assert.False(addSession1Result.IsError);

        Assert.True(addSession2Result.IsError);
        Assert.Equal(TrainerErrors.CannotHaveTwoOrMoreOverlappingSessions, addSession2Result.FirstError);
    }
}
