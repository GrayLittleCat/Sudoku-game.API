using System.Diagnostics.CodeAnalysis;
using Application.Abstractions.Data;
using Domain.Players;
using Infrastructure.Players;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    private const string AzureDbSecretName = "ConnectionStrings:AzureSql";
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration[AzureDbSecretName];
        services.AddDbContext<ApplicationDbContext>(options =>
            options
                .UseSqlServer(connectionString));

        services.AddScoped<IApplicationDbContext>(sp =>
            sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IUnitOfWork>(sp =>
            sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IPlayerRepository, PlayerRepository>();

        return services;
    }
}
