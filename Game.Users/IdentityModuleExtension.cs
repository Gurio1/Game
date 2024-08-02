using FluentValidation;
using Game.Users.Data;
using Game.Users.Endpoints;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Game.Users;

public static class IdentityModuleExtension
{
    public static IServiceCollection AddIdentityModuleServices(
        this IServiceCollection services,
        ConfigurationManager config,
        ILogger logger,
        List<System.Reflection.Assembly> mediatRAssemblies)
    {
        string? connectionString = config.GetConnectionString("IdentityConnectionString");
        services.AddDbContext<UsersDbContext>(builder =>
        {
            builder.UseSqlServer(connectionString);
        });

        services.AddIdentityCore<ApplicationUser>()
            .AddEntityFrameworkStores<UsersDbContext>();
        
        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.User.RequireUniqueEmail = true;
        });
        
        // if using MediatR in this module, add any assemblies that contain handlers to the list
        mediatRAssemblies.Add(typeof(IdentityModuleExtension).Assembly);
        
        logger.Information("{Module} module services registered", "Users");

        return services;
    }
}