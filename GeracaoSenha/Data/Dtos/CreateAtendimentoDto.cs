using System.ComponentModel.DataAnnotations;

namespace GeracaoSenha.Data.Dtos
{
    public class CreateAtendimentoDto
    {
        [Required]
        public int CodigoTipoAtendimento { get; set; }
    }
}
