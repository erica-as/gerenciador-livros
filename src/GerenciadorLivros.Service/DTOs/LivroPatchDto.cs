namespace GerenciadorLivros.Service.DTOs
{
    // DTO para atualizações parciais. Todos os campos são opcionais.
    public class LivroPatchDto
    {
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public string? Autor { get; set; }
        public int? Paginas { get; set; }
        public bool? Lido { get; set; }
        public DateTime? LidoInicio { get; set; }
        public DateTime? LidoFim { get; set; }
    }
}
