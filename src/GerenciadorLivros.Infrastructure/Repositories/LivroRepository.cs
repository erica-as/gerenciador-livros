using GerenciadorLivros.Domain.Entities;
using GerenciadorLivros.Domain.Interfaces;
using GerenciadorLivros.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorLivros.Infrastructure.Repositories
{
   public class LivroRepository: ILivroRepository
    {
        private readonly AppDbContext _context;

        public LivroRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Livro>> ObterTodosAsync()
        {
            return await _context.Livros.ToListAsync();
        }

        public async Task<Livro?> ObterPorIdAsync(Guid id)
        {
            return await _context.Livros.FindAsync(id);
        }

        public async Task AdicionarAsync(Livro livro)
        {
            await _context.Livros.AddAsync(livro);
            await _context.SaveChangesAsync();
        }


        public async Task AtualizarAsync(Livro livro)
        {
            _context.Livros.Update(livro);
            await _context.SaveChangesAsync();
        }

        public async Task ExcluirAsync(Guid id)
        {
            var livro = await ObterPorIdAsync(id);
            if(livro != null)
            {
                _context.Livros.Remove(livro);
                await _context.SaveChangesAsync();
            }
        }
    }
}