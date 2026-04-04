namespace GerenciadorLivros.Service.DTOs
{
    public class LivroUpdateDto
    {
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
        public int Paginas { get; set; }
        public bool Lido { get; set; }
        public DateTime? LidoInicio { get; set; }
        public DateTime? LidoFim { get; set; }
    }
}
