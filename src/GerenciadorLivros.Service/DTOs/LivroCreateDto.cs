using System.ComponentModel.DataAnnotations;

namespace GerenciadorLivros.Service.DTOs
{
    public class LivroCreateDto
    {
        [Required(ErrorMessage = "O título é obrigatório")]
        [StringLength(200, ErrorMessage = "O título deve ter no máximo 200 caracteres")]
        public string Titulo { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "O autor é obrigatório")]
        [StringLength(100, ErrorMessage = "O autor deve ter no máximo 100 caracteres")]
        public string Autor { get; set; } = string.Empty;

        [Range(1, 10000, ErrorMessage = "O número de páginas deve estar entre 1 e 10000")]
        public int Paginas { get; set; }
    }
}
