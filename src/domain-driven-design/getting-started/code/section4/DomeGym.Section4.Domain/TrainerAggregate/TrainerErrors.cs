using ErrorOr;

namespace DomeGym.Section4.Domain.TrainerAggregate;

public static class TrainerErrors
{
    public static readonly Error CannotHaveTwoOrMoreOverlappingSessions = Error.Validation(
        code: "Trainer.CannotHaveTwoOrMoreOverlappingSessions",
        description: "A trainer cannot have two or more overlapping sessions");
}
