using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Game.Monsters;

public static class MonstersModuleExtensions
{
    public static IServiceCollection AddMonstersModuleServices(
        this IServiceCollection services,
        ConfigurationManager config,
        ILogger logger,
        List<System.Reflection.Assembly> mediatRAssemblies)
    {
        // if using MediatR in this module, add any assemblies that contain handlers to the list
        mediatRAssemblies.Add(typeof(MonstersModuleExtensions).Assembly);

        logger.Information("{Module} module services registered", "Monsters");

        return services;
    }
}