using System.Text;
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
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure;

public static class DependencyInjection
{
    private const string OracleDbSecretName = "ConnectionStrings:OracleDB";
    private const string AuthenticationTokenUriName = "Authentication:TokenUri";
    private const string AuthenticationValidIssuerName = "Authentication:ValidIssuer";
    private const string AuthenticationAudienceName = "Authentication:Audience";
    private const string FirebaseJsonName = "Firebase_JSON";

    public static void AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        string? GetConfigValueBase64(string configName)
        {
            var configNameBase64 = configName + "_BASE64";
            var configValueBase64 = configuration[configNameBase64];

            return configValueBase64.IsNullOrEmpty()
                ? configuration[configName]
                : Encoding.UTF8.GetString(Convert.FromBase64String(configValueBase64!));
        }

        var connectionString = GetConfigValueBase64(OracleDbSecretName);
        var firebaseJson = GetConfigValueBase64(FirebaseJsonName);
        var authTokenUriString = GetConfigValueBase64(AuthenticationTokenUriName);
        var authValidIssuer = GetConfigValueBase64(AuthenticationValidIssuerName);
        var authAudience = GetConfigValueBase64(AuthenticationAudienceName);

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
            Credential = GoogleCredential.FromJson(firebaseJson)
        });

        services.AddSingleton<IAuthenticationService, AuthenticationService>();
        services.AddHttpClient<IJwtProvider, JwtProvider>(httpClient =>
        {
            httpClient.BaseAddress = new Uri(authTokenUriString!);
        });
        services.AddAuthorization();
        services
            .AddAuthentication()
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtOptions =>
            {
                jwtOptions.Authority = authValidIssuer;
                jwtOptions.Audience = authAudience;
                jwtOptions.TokenValidationParameters.ValidIssuer = authValidIssuer;
            });

        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationProvider>();
        services.AddScoped<IPermissionService, PermissionService>();
    }
}
