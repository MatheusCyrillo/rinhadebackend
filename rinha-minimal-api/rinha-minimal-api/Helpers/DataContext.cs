using System.Data;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;

namespace rinha_minimal_api.Helpers
{
    public class DataContext
    {
        private DbSettings _dbSettings;

        public DataContext(IOptions<DbSettings> dbSettings)
        {
            _dbSettings = dbSettings.Value;
        }

        public IDbConnection CreateConnection()
        {
            var connectionString = $"Host={_dbSettings.Server}; Database={_dbSettings.Database}; Username={_dbSettings.UserId}; Password={_dbSettings.Password};";
            return new NpgsqlConnection(connectionString);
        }

        public async Task Init()
        {
            await Task.Delay(3000);
            Console.WriteLine("VERSAO DO DOCKER 2");
            await _initDatabase();
            await _initTables();
        }

        private async Task _initDatabase()
        {
            // create database if it doesn't exist
            var connectionString = $"Server={_dbSettings.Server}; Database={_dbSettings.Database}; User ID={_dbSettings.UserId}; Password={_dbSettings.Password};IntegratedSecurity=true;";
            Console.WriteLine(connectionString);
            using var connection = new NpgsqlConnection(connectionString);
            var sqlDbCount = $"SELECT COUNT(*) FROM pg_database WHERE datname = '{_dbSettings.Database}';";
            var dbCount = await connection.ExecuteScalarAsync<int>(sqlDbCount);
            if (dbCount == 0)
            {
                var sql = $"CREATE DATABASE \"{_dbSettings.Database}\"";
                await connection.ExecuteAsync(sql);
            }
        }

        private async Task _initTables()
        {
            // create tables if they don't exist
            using var connection = CreateConnection();
            await CreatePersonTable(connection);
        }

        private async Task CreatePersonTable(IDbConnection connection)
        {

            var sql = """
                CREATE TABLE IF NOT EXISTS Pessoa (
                    Id uuid PRIMARY KEY,
                    apelido VARCHAR(32) UNIQUE,
                    nome VARCHAR(100),
                    nascimento VARCHAR,
                    stack VARCHAR[]
                );
            """;

            await connection.ExecuteAsync(sql);

        }
    }
}
