using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace RiverBooks.Reporting;

public static class ReportingModuleServicesExtensions
{
    public static IServiceCollection AddReportingModuleServices(
        this IServiceCollection services,
        ConfigurationManager config,
        ILogger logger,
        List<System.Reflection.Assembly> mediatRAssemblies)
    {
        //var connectionString = config.GetConnectionString("OrderProcessingConnectionString");
        //services.AddDbContext<OrderProcessingDbContext>(config =>
        //  config.UseSqlServer(connectionString));


        // if using MediatR in this module, add any assemblies that contain handlers to the list
        mediatRAssemblies.Add(typeof(ReportingModuleServicesExtensions).Assembly);

        logger.Information("{Module} module services registered", "Reporting");
        return services;
    }
}