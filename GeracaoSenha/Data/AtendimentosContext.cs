using GeracaoSenha.Model;
using GeracaoSenha.Model.Enum;

namespace GeracaoSenha.Data
{
    public static class AtendimentosContext
    {
        public static List<Atendimento> AtendimentosConsulta = new List<Atendimento>();
        public static List<Atendimento> AtendimentosExame = new List<Atendimento>();

        public static Atendimento NovoAtendimento(TipoAtendimento tipoAtendimento)
        {
            var Lista = new List<Atendimento>();

            switch (tipoAtendimento)
            {
                case TipoAtendimento.Exame:
                    Lista = AtendimentosExame;
                    break;
                case TipoAtendimento.Consulta:
                    Lista = AtendimentosConsulta;
                    break;
            }

            if (Lista.Count == 0)
            {
                Lista.Add(new Atendimento(tipoAtendimento));
            }
            else
            {
                Lista.Add(new Atendimento(Lista.Last().Id, tipoAtendimento));
            }
            return Lista.Last();
        }

        public static Atendimento ConsultarAtendimentoPelaSenha(string Senha)
        {
            var Lista = new List<Atendimento>();

            switch (Senha[0])
            {
                case 'C':
                    Lista = AtendimentosConsulta;
                    break;
                case 'E':
                    Lista = AtendimentosExame;
                    break;
            }

            var atendimento = Lista.FirstOrDefault(atendimento => atendimento.Id == Convert.ToInt32(Senha.Substring(1, 4)));
            return atendimento;
        }
    }
}
