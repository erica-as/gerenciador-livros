using Microsoft.EntityFrameworkCore;
using GerenciadorLivros.Domain.Entities;

namespace GerenciadorLivros.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        }

        public DbSet<Livro> Livros { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings => 
                warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Livro>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Titulo).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Descricao).HasColumnType("NVARCHAR(MAX)");
                entity.Property(e => e.Autor).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Paginas).IsRequired();
                entity.Property(e => e.Lido).IsRequired();
                entity.Property(e => e.LidoInicio);
                entity.Property(e => e.LidoFim);
                entity.Property(e => e.DataCriacao).IsRequired();
            });
        }
    }
}
