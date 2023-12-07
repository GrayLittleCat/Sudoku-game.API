using System.Data;
using Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class DbConnectionFactory : IDbConnectionFactory
{
    private readonly ApplicationDbContext _context;

    public DbConnectionFactory(ApplicationDbContext context)
    {
        _context = context;
    }

    public IDbConnection CreateOpenConnection()
    {
        var connection = _context.Database.GetDbConnection();

        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        return connection;
    }
}
