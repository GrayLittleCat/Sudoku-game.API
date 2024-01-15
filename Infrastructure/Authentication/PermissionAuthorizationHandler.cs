using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Infrastructure.Authentication;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var playerId = context.User.Claims.FirstOrDefault(
            x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;

        if (!int.TryParse(playerId, out var parsedPlayerId))
        {
            return;
        }

        using var scope = _serviceScopeFactory.CreateScope();

        var permissionService = scope.ServiceProvider
            .GetRequiredService<IPermissionService>();

        var permissions = await permissionService.GetPermissionsAsync(parsedPlayerId);

        if (permissions.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }
    }
}
