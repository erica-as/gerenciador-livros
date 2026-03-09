namespace GerenciadorLivros.Service.DTOs
{
    public class LivroResponseDto
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
        public bool Lido { get; set; }
        public string StatusLeitura => Lido ? "Concluído" : "Pendente";
    }
}
