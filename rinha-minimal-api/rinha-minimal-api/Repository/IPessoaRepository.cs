using rinha_minimal_api.Models;

namespace rinha_minimal_api.Repository
{
    public interface IPessoaRepository
    {
        Task<bool> Create(Pessoa pessoa);
        Task<Pessoa?> Get(Guid id);
        Task<int> TotalCount();
        Task<IEnumerable<Pessoa>> GetPessoasByTermo(string stack);
    }
}
