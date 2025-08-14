using ErrorOr;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using GymManagement.Application.Common.Behaviors;
using GymManagement.Application.Gyms.Commands.CreateGym;
using GymManagement.Domain.Gyms;
using MediatR;
using NSubstitute;
using TestCommon.Gyms;
using Xunit;

namespace GymManagement.Application.UnitTests.Common.Behaviors;

public class ValidationBehaviorTests
{
    [Fact]
    public async Task InvokeBehavior_WhenValidatorResultIsValid_ShouldInvokeNextBehavior()
    {
        // ARRANGE:
        // Create a request
        var createGymRequest = GymCommandFactory.CreateCreateGymCommand();

        // Create a next behavior (mock)
        var gym = GymFactory.CreateGym();
        var mockNextBehavior = Substitute.For<RequestHandlerDelegate<ErrorOr<Gym>>>();
        mockNextBehavior.Invoke().Returns(gym);
        
        // Create validator (mock)
        var mockValidator = Substitute.For<IValidator<CreateGymCommand>>();
        mockValidator
            .ValidateAsync(createGymRequest, Arg.Any<CancellationToken>())
            .Returns(new ValidationResult());

        // Create validation behavior (system under test)
        var validationBehavior = new ValidationBehavior<CreateGymCommand, ErrorOr<Gym>>(mockValidator);
        
        // ACT:
        // Invoke behavior
        var result = await validationBehavior.Handle(createGymRequest, mockNextBehavior, CancellationToken.None);

        // ASSERT:
        // Result from invoking the behavior was the result returned by the next behavior
        result.IsError.Should().BeFalse();
        result.Value.Should().BeEquivalentTo(gym);
    }
}