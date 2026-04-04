namespace GerenciadorLivros.Domain.Entities
{
    public class Livro
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Autor { get; set; }
        public int Paginas { get; set; }
        public bool Lido { get; set; }
        public DateTime? LidoInicio { get; set; }
        public DateTime? LidoFim { get; set; }
        public DateTime DataCriacao { get; set; }

        public Livro()
        {
            Id = Guid.NewGuid();
            DataCriacao = DateTime.UtcNow;
        }
    }
}
