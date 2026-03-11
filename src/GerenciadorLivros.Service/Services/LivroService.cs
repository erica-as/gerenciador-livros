using GerenciadorLivros.Domain.Entities;
using GerenciadorLivros.Domain.Interfaces;
using GerenciadorLivros.Service.DTOs;
using GerenciadorLivros.Domain.DTOs.Log;
using GerenciadorLivros.Service.Interfaces;

namespace GerenciadorLivros.Service.Services
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _repository;

        private readonly ILogRepository _logRepository;

        public LivroService(ILivroRepository repository, ILogRepository log)
        {
            _repository = repository;
            _logRepository = log;
        }

        public async Task<IEnumerable<LivroResponseDto>> ObterTodosAsync()
        {
            var livros = await _repository.ObterTodosAsync();
            return livros.Select(l => new LivroResponseDto
            {
                Id = l.Id,
                Titulo = l.Titulo,
                Autor = l.Autor,
                Lido = l.Lido
            });
        }

        public async Task<LivroResponseDto?> ObterPorIdAsync(Guid id)
        {
            var livro = await _repository.ObterPorIdAsync(id);
            if (livro == null)
                return null;

            return new LivroResponseDto
            {
                Id = livro.Id,
                Titulo = livro.Titulo,
                Autor = livro.Autor,
                Lido = livro.Lido
            };
        }

        public async Task<Guid> AdicionarAsync(LivroCreateDto dto)
        {
            var novoLivro = new Livro
            {
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                Autor = dto.Autor,
                Paginas = dto.Paginas
            };

            await _repository.AdicionarAsync(novoLivro);

            await _logRepository.SalvarLogAsync(new LogAtividadeDto
            {
                AcaoCode = "BOOK_CREATED",
                Mensagem = $"Livro '{dto.Titulo}' criado.",
                EntidadeId = novoLivro.Id.ToString(),
                Metadata = new Dictionary<string, object> { ["paginas"] = dto.Paginas }
            });

            return novoLivro.Id;
        }

        public async Task<bool> MarcarComoLidoAsync(Guid id)
        {
            var livro = await _repository.ObterPorIdAsync(id);
            if (livro != null)
            {
                livro.Lido = true;
                livro.LidoFim = DateTime.Now; 
                await _repository.AtualizarAsync(livro);
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<LivroResponseDto>> ObterLivrosLidosAsync()
        {
            var todosLivros = await _repository.ObterTodosAsync();
            return todosLivros
                .Where(l => l.Lido)
                .Select(l => new LivroResponseDto
                {
                    Id = l.Id,
                    Titulo = l.Titulo,
                    Autor = l.Autor,
                    Lido = l.Lido
                });
        }

        public async Task AtualizarAsync(Livro livro) => await _repository.AtualizarAsync(livro);

        public async Task ExcluirAsync(Guid id) => await _repository.ExcluirAsync(id);
    }
}