using DomeGym.Section5.Application.Common.Interfaces;
using DomeGym.Section5.Domain.Common.Interfaces;
using DomeGym.Section5.Infrastructure.Persistence;
using DomeGym.Section5.Infrastructure.Persistence.Repositories;
using DomeGym.Section5.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DomeGym.Section5.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddServices()
            .AddPersistence(configuration);

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();

        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("DomeGym")
            ?? throw new InvalidOperationException("Connection string 'DomeGym' is not configured.");

        // DomeGymDbContext queues domain events on the current HttpContext.
        services.AddHttpContextAccessor();
        services.AddDbContext<DomeGymDbContext>(options => options.UseSqlite(connectionString));

        services.AddScoped<IAdminsRepository, AdminsRepository>();
        services.AddScoped<IGymsRepository, GymsRepository>();
        services.AddScoped<IParticipantsRepository, ParticipantsRepository>();
        services.AddScoped<IRoomsRepository, RoomsRepository>();
        services.AddScoped<ISessionsRepository, SessionsRepository>();
        services.AddScoped<ISubscriptionsRepository, SubscriptionsRepository>();
        services.AddScoped<ITrainersRepository, TrainersRepository>();

        return services;
    }
}
