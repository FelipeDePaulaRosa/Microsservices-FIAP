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
        public static List<Atendimento> FilaOrdenada()
        {
            List<Atendimento> fila = new List<Atendimento>();
            List<Atendimento> pendentes = new List<Atendimento>(Atendimentos.Where(atendimento => atendimento.IsPendente()).OrderBy(atendimento => atendimento.HorarioChegada));
            Atendimento preferencial = pendentes.Where(atendimento => atendimento.isPreferencial()).FirstOrDefault();
            int preferenciaisAdicionados = 0;
            while (pendentes.Count > 0)
            {
                if (preferencial is null || preferenciaisAdicionados == 2)
                {
                    preferenciaisAdicionados = 0;
                    fila.Add(pendentes.Where(atendimento => !atendimento.isPreferencial()).First());
                    pendentes.Remove(pendentes.Where(atendimento => !atendimento.isPreferencial()).First());
                }
                else
                {
                    preferenciaisAdicionados++;
                    fila.Add(preferencial);
                    pendentes.Remove(preferencial);
                    preferencial = pendentes.Where(atendimento => atendimento.isPreferencial()).FirstOrDefault();
                }
            }
            return fila;
        }
    }
}
