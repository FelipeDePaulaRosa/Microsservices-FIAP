using GeracaoSenha.Model.Enum;
using System.ComponentModel.DataAnnotations;

namespace GeracaoSenha.Data.Dtos
{
    public class CreateAtendimentoDto
    {
        [Required]
        public TipoAtendimento TipoAtendimento { get; set; }
    }
}
