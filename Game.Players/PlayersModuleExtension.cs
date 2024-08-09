using FluentValidation;
using Game.Characters.Data;
using Game.Characters.Endpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Game.Characters;

public static class PlayersModuleExtension
{
    public static IServiceCollection AddPlayersModuleServices(
        this IServiceCollection services,
        ConfigurationManager config,
        ILogger logger,
        List<System.Reflection.Assembly> mediatRAssemblies)
    {
        string? connectionString = config.GetConnectionString("PlayersConnectionString");
        services.AddDbContext<PlayersDbContext>(builder =>
        {
            builder.UseSqlServer(connectionString);
        });

        services.AddScoped<IPlayerRepository, PlayerRepository>();
        
        // if using MediatR in this module, add any assemblies that contain handlers to the list
        mediatRAssemblies.Add(typeof(PlayersModuleExtension).Assembly);
        
        logger.Information("{Module} module services registered", "Players");

        return services;
    }
}