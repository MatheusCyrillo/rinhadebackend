using Dapper;
using rinha_minimal_api.Helpers;
using rinha_minimal_api.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace rinha_minimal_api.Repository
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly DataContext _dataContext;

        public PessoaRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> Create(Pessoa pessoa)
        {
            try
            {
                using var connection = _dataContext.CreateConnection();
                var sql = """
            INSERT INTO pessoa (id, apelido, nome, nascimento, stack)
            VALUES (@Id, @Apelido, @Nome, @Nascimento, @Stack)
        """;
                return Convert.ToBoolean(await connection.ExecuteAsync(sql, pessoa));
            }
            catch (Exception)
            { 
                return false;
            }
        }

        public async Task<Pessoa?> Get(Guid id)
        {
            using var connection = _dataContext.CreateConnection();
            var sql = """
            SELECT * FROM pessoa 
            WHERE Id = @id
        """;
            return await connection.QuerySingleOrDefaultAsync<Pessoa>(sql, new { id });
        }

        public async Task<IEnumerable<Pessoa>> GetPessoasByTermo(string termo)
        {
            using var connection = _dataContext.CreateConnection();
            var sql = """
            SELECT * FROM pessoa 
            WHERE 
                  Nome like @t or 
                  Apelido like @t or
        array_to_string(Stack, ',') like @t
        limit 50;   
        """;
            return await connection.QueryAsync<Pessoa>(sql, new { t = "%" + termo + "%" });
        }

        public async Task<int> TotalCount()
        {
            using var connection = _dataContext.CreateConnection();
            var sql = """SELECT count(*) FROM pessoa""";
            return await connection.ExecuteScalarAsync<int>(sql);
        }
    }
}
