using ErrorOr;
using FluentValidation;
using GymManagement.Application.Common.Behaviors;
using GymManagement.Application.Gyms.Commands.CreateGym;
using GymManagement.Domain.Gyms;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GymManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // MediatR tells the DI container: Whenever you resolve a request/response pair for IPipelineBehavior<TRequest, TResponse>,
        // if it matches the generic structure of ValidationBehavior<TRequest, TResponse>, use this class in the pipeline.
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));
            // options.AddBehavior<IPipelineBehavior<CreateGymCommand, ErrorOr<Gym>>, CreateGymCommandBehavior>();
            options.AddOpenBehavior(typeof(ValidationBehavior<,>)); // a ValidationBehavior with two type parameters, but we don’t know what they are yet.
        });

        services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection));
            
        return services;
    }
}