using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Domain.Levels;
using Domain.Players;
using Domain.PlayerScores;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Infrastructure.Authentication;
using Infrastructure.Levels;
using Infrastructure.Players;
using Infrastructure.PlayerScores;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    private const string OracleDbSecretName = "ConnectionStrings:OracleDB";

    public static void AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration[OracleDbSecretName];
        services.AddDbContext<ApplicationDbContext>(options =>
            options
                .UseUpperSnakeCaseNamingConvention()
                .UseOracle(connectionString));

        services.AddScoped<IApplicationDbContext>(sp =>
            sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IUnitOfWork>(sp =>
            sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();

        services.AddScoped<IPlayerRepository, PlayerRepository>();
        services.AddScoped<IPlayerScoreRepository, PlayerScoreRepository>();
        services.AddScoped<ILevelRepository, LevelRepository>();

        FirebaseApp.Create(new AppOptions
        {
            Credential = GoogleCredential.FromFile("firebase.json")
        });

        services.AddSingleton<IAuthenticationService, AuthenticationService>();
        services.AddHttpClient<IJwtProvider, JwtProvider>(httpClient =>
        {
            httpClient.BaseAddress = new Uri(configuration["Authentication:TokenUri"]);
        });
        services.AddAuthorization();
        services
            .AddAuthentication()
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtOptions =>
            {
                jwtOptions.Authority = configuration["Authentication:ValidIssuer"];
                jwtOptions.Audience = configuration["Authentication:Audience"];
                jwtOptions.TokenValidationParameters.ValidIssuer = configuration["Authentication:ValidIssuer"];
            });
    }
}
