using GerenciadorLivros.Domain.Entities;
using GerenciadorLivros.Service.DTOs;

namespace GerenciadorLivros.Service.Interfaces
{
    public interface ILivroService
    {
        Task<IEnumerable<LivroResponseDto>> ObterTodosAsync();
        Task<LivroResponseDto?> ObterPorIdAsync(Guid id);
        Task<Guid> AdicionarAsync(LivroCreateDto livro);
        Task AtualizarAsync(Livro livro);
        Task ExcluirAsync(Guid id);
        Task<bool> MarcarComoLidoAsync(Guid id);
        Task<IEnumerable<LivroResponseDto>> ObterLivrosLidosAsync();
    }
}