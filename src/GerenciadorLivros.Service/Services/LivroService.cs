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

        public async Task<IEnumerable<Livro>> ObterTodosAsync()
        {
            var livros = await _repository.ObterTodosAsync();
            return livros;
        }

        public async Task<Livro?> ObterPorIdAsync(Guid id)
        {
            return await _repository.ObterPorIdAsync(id);
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

        public async Task PatchAsync(Guid id, LivroPatchDto dto)
        {
            var livro = await _repository.ObterPorIdAsync(id);
            if (livro == null) throw new KeyNotFoundException();
            if (dto.Titulo is not null) livro.Titulo = dto.Titulo;
            if (dto.Descricao is not null) livro.Descricao = dto.Descricao;
            if (dto.Autor is not null) livro.Autor = dto.Autor;
            if (dto.Paginas.HasValue) livro.Paginas = dto.Paginas.Value;
            if (dto.Lido.HasValue) livro.Lido = dto.Lido.Value;
            if (dto.LidoInicio.HasValue) livro.LidoInicio = dto.LidoInicio;
            if (dto.LidoFim.HasValue) livro.LidoFim = dto.LidoFim;
            await _repository.AtualizarAsync(livro);
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

        public async Task<IEnumerable<Livro>> ObterLivrosLidosAsync()
        {
            var todosLivros = await _repository.ObterTodosAsync();
            return todosLivros;
        }

        public async Task AtualizarAsync(Guid id, LivroUpdateDto dto)
        {
            var livro = await _repository.ObterPorIdAsync(id);
            if (livro == null) throw new KeyNotFoundException("Livro não encontrado.");

            livro.Titulo = dto.Titulo;
            livro.Descricao = dto.Descricao;
            livro.Autor = dto.Autor;
            livro.Paginas = dto.Paginas;
            livro.Lido = dto.Lido;
            livro.LidoInicio = dto.LidoInicio;
            livro.LidoFim = dto.LidoFim;

            await _repository.AtualizarAsync(livro);
        }

        public async Task ExcluirAsync(Guid id) => await _repository.ExcluirAsync(id);
    }
}