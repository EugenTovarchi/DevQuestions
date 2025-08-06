using Microsoft.Extensions.Configuration;
using Npgsql;
using Shared.Database;
using System.Data;

namespace Questions.Infrastructure.Postgres;

/// <summary>
/// Паттерн фабрики для создания соединения с Postgresql
/// </summary>
public class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly IConfiguration _configuration;

    public SqlConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IDbConnection Create ()
    {
        var connection = new NpgsqlConnection(_configuration.GetConnectionString("Database"));
        return connection;
    }
}
