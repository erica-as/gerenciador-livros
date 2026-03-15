using GerenciadorLivros.Domain.Entities;
using GerenciadorLivros.Service.DTOs;

namespace GerenciadorLivros.Service.Interfaces
{
    public interface ILivroService
    {
        Task<IEnumerable<Livro>> ObterTodosAsync();
        Task<Livro?> ObterPorIdAsync(Guid id);
        Task<Guid> AdicionarAsync(LivroCreateDto livro);
        Task AtualizarAsync(Guid id, LivroUpdateDto dto);
        Task PatchAsync(Guid id, LivroPatchDto dto);
        Task ExcluirAsync(Guid id);
        Task<bool> MarcarComoLidoAsync(Guid id);
        Task<IEnumerable<Livro>> ObterLivrosLidosAsync();
    }
}