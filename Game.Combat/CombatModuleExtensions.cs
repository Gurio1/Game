using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
namespace Game.Combat;

public static class CombatModuleExtensions
{
    public static IServiceCollection AddCombatModuleServices(
        this IServiceCollection services,
        ConfigurationManager config,
        ILogger logger,
        List<System.Reflection.Assembly> mediatRAssemblies)
    {
        
        services.AddScoped<CombatService>();
        
        // if using MediatR in this module, add any assemblies that contain handlers to the list
        mediatRAssemblies.Add(typeof(CombatModuleExtensions).Assembly);

        logger.Information("{Module} module services registered", "Combat");

        return services;
    }
}