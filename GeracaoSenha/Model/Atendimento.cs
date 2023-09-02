using GeracaoSenha.Model;
using GeracaoSenha.Model.Enum;

namespace GeracaoSenha.Model
{
    public class Atendimento
    {
        private TipoAtendimento exame;

        public int Id { get; set; }
        public TipoAtendimento TipoAtendimento { get; private set; }
        public DateTime HorarioChegada { get; private set; }
        public bool Atendido { get; private set; }
        public DateTime? HorarioAtendimento { get; private set; }

        public Atendimento(int idAnterior, TipoAtendimento _TipoAtendimento)
        {
            Id = idAnterior + 1;
            TipoAtendimento = _TipoAtendimento;
            HorarioChegada = DateTime.Now;
            Atendido = false;
        }

        public Atendimento(TipoAtendimento _TipoAtendimento)
        {
            Id = 1;
            TipoAtendimento = _TipoAtendimento;
            HorarioChegada = DateTime.Now;
            Atendido = false;
        }
        public void Atender()
        {
            Atendido = true;
            HorarioAtendimento = DateTime.Now;
        }
    }
}
