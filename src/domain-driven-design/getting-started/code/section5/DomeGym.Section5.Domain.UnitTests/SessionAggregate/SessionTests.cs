using DomeGym.Section5.Domain.ParticipantAggregate;
using DomeGym.Section5.Domain.SessionAggregate;
using DomeGym.Section5.Domain.UnitTests.TestUtils.Participants;
using DomeGym.Section5.Domain.UnitTests.TestUtils.Services;
using DomeGym.Section5.Domain.UnitTests.TestUtils.Sessions;
using ErrorOr;

namespace DomeGym.Section5.Domain.UnitTests.SessionAggregate;

public class SessionTests
{
    [Fact]
    public void ReserveSpot_WhenNoMoreRoom_ShouldFailReservation()
    {
        // Arrange
        Session session = SessionFactory.CreateSession(maxParticipants: 1);
        Participant participant1 = ParticipantFactory.CreateParticipant(id: Guid.NewGuid(), userId: Guid.NewGuid());
        Participant participant2 = ParticipantFactory.CreateParticipant(id: Guid.NewGuid(), userId: Guid.NewGuid());

        // Act
        ErrorOr<Success> reserveParticipant1Result = session.ReserveSpot(participant1);
        ErrorOr<Success> reserveParticipant2Result = session.ReserveSpot(participant2);

        // Assert
        Assert.False(reserveParticipant1Result.IsError);

        Assert.True(reserveParticipant2Result.IsError);
        Assert.Equal(SessionErrors.CannotHaveMoreReservationsThanParticipants, reserveParticipant2Result.FirstError);
    }

    [Fact]
    public void CancelReservation_WhenCancellationIsTooCloseToSession_ShouldFailCancellation()
    {
        // Arrange
        Session session = SessionFactory.CreateSession(
            date: Constants.Session.Date,
            time: Constants.Session.Time);

        Participant participant = ParticipantFactory.CreateParticipant();

        var cancellationDateTime = Constants.Session.Date.ToDateTime(TimeOnly.MinValue);

        // Act
        ErrorOr<Success> reserveSpotResult = session.ReserveSpot(participant);
        ErrorOr<Success> cancelReservationResult = session.CancelReservation(
            participant,
            new TestDateTimeProvider(fixedDateTime: cancellationDateTime));

        // Assert
        Assert.False(reserveSpotResult.IsError);

        Assert.True(cancelReservationResult.IsError);
        Assert.Equal(SessionErrors.CannotCancelReservationTooCloseToSession, cancelReservationResult.FirstError);
    }
}
