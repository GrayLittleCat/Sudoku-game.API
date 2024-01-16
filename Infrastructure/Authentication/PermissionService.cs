using Application.Abstractions.Data;
using Dapper;

namespace Infrastructure.Authentication;

public class PermissionService : IPermissionService
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public PermissionService(ApplicationDbContext context, IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<HashSet<string>> GetPermissionsAsync(string identityId)
    {
        using var connection = _dbConnectionFactory.CreateOpenConnection();

        const string sql =
            """
            SELECT rp.PERMISSION_ID,
                   p.ID as player_id,
                   pr.ROLE_ID,
                   per.NAME as permission_name
              FROM PLAYER_ROLES pr
              JOIN PLAYERS p
                ON pr.PLAYER_ID = p.ID
              JOIN ROLE_PERMISSIONS rp
                ON rp.ROLE_ID = pr.ROLE_ID
              JOIN PERMISSIONS per
                ON rp.PERMISSION_ID = per.ID
             WHERE p.IDENTITY_ID = :IdentityId
            """;

        var param = new DynamicParameters();

        param.Add("IdentityId", identityId);

        var resultSet =
            await connection
                .QueryAsync<(int permissionId, int playerId, int roleId, string permissionName)>(sql, param);

        var ret = resultSet.Select(x => x.permissionName).ToHashSet();

        return ret;
    }
}
