using GerenciadorLivros.Domain.Entities;

namespace GerenciadorLivros.Domain.Interfaces
{
    public interface ILivroRepository
    {
        Task<IEnumerable<Livro>> ObterTodosAsync();
        Task<Livro?> ObterPorIdAsync(Guid id);
        Task AdicionarAsync(Livro livro);
        Task AtualizarAsync(Livro livro);
        Task ExcluirAsync(Guid id);
    }
}
