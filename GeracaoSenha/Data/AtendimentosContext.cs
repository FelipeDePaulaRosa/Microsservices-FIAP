using GeracaoSenha.Model;
using GeracaoSenha.Model.Enum;

namespace GeracaoSenha.Data
{
    public static class AtendimentosContext
    {
        public static List<Atendimento> Atendimentos = new List<Atendimento>();

        public static Atendimento NovoAtendimento(TipoAtendimento tipoAtendimento)
        {
            Atendimento Ultimo = Atendimentos.Where(atendimento => atendimento.TipoAtendimento == tipoAtendimento).LastOrDefault();
            Atendimento Novo = null;

            if (Ultimo is null)
            {
                Novo = new Atendimento(tipoAtendimento);
                Atendimentos.Add(Novo);
            }
            else
            {
                Novo = new Atendimento(Ultimo.Id, tipoAtendimento);
                Atendimentos.Add(Novo);
            }
            return Novo;
        }

        public static Atendimento ConsultarAtendimentoPelaSenha(string Senha)
        {
            var Lista = new List<Atendimento>();

            var sigla = Senha.Substring(0, 2);
            var tipoAtendimento = SiglaTipoAtendimento.GetTipoAtendimentoSigla(sigla);

            var atendimento = Atendimentos.FirstOrDefault(atendimento => atendimento.Id == Convert.ToInt32(Senha.Substring(2, 4)) && atendimento.TipoAtendimento == tipoAtendimento);
            return atendimento;
        }
    }
}
