using System.ComponentModel.DataAnnotations;

namespace GeracaoSenha.Data.Dtos
{
    public class ChamarAtendimentoDto
    {
        [Required]
        public int Guiche { get; set; }
    }
}
