
using GeracaoSenha.Model;
using GeracaoSenha.Model.Enum;

namespace GeracaoSenha.Data.Dtos
{
    public class ReadAtendimentoDto
    {
        public Atendimento Atendimento { get; set; }
        public String Senha { get; set; }
        public String Link => "https://hospital/acompanhamentofila/" + Senha;

        public ReadAtendimentoDto(Atendimento _Atendimento)
        {
            Atendimento = _Atendimento;
            Senha = Atendimento.TipoAtendimento == TipoAtendimento.Consulta ? "C" : "E";
            Senha += Atendimento.Id.ToString().PadLeft(4, '0');
        }
    }
}