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
        public SituacaoAtendimento SituacaoAtendimento { get; private set; }
        public DateTime? HorarioChamada { get; private set; }
        public DateTime? HorarioInicio { get; private set; }
        public DateTime? HorarioFinalizacao { get; private set; }
        public int? Guiche { get; private set; }
        public Atendimento(int idAnterior, TipoAtendimento _TipoAtendimento)
        {
            Id = idAnterior + 1;
            TipoAtendimento = _TipoAtendimento;
            HorarioChegada = DateTime.Now;
            SituacaoAtendimento = SituacaoAtendimento.Aguardando;
        }

        public Atendimento(TipoAtendimento _TipoAtendimento)
        {
            Id = 1;
            TipoAtendimento = _TipoAtendimento;
            HorarioChegada = DateTime.Now;
            SituacaoAtendimento = SituacaoAtendimento.Aguardando;
        }
        public void Chamar(int guiche)
        {
            if (SituacaoAtendimento!=SituacaoAtendimento.Aguardando)
            {
                throw new Exception();
            }
            AtualizarSituacao(SituacaoAtendimento.Chamado);
            HorarioChamada = DateTime.Now;
            Guiche= guiche;
        }
        public void Iniciar()
        {
            if (SituacaoAtendimento != SituacaoAtendimento.Chamado)
            {
                throw new Exception();
            }
            AtualizarSituacao(SituacaoAtendimento.Iniciado);
            HorarioInicio = DateTime.Now;
        }
        public void Finalizar()
        {
            if (SituacaoAtendimento != SituacaoAtendimento.Iniciado)
            {
                throw new Exception();
            }
            AtualizarSituacao(SituacaoAtendimento.Finalizado);
            HorarioFinalizacao = DateTime.Now;
        }

        public void AtualizarSituacao(SituacaoAtendimento situacaoAtendimento)
        {
            SituacaoAtendimento = situacaoAtendimento;
        }

        public bool isPreferencial()
        {
            return TipoAtendimento == TipoAtendimento.EXAME_PREFERENCIAL || TipoAtendimento == TipoAtendimento.CONSULTA_PREFERENCIAL;
        }
        public bool IsPendente() 
        { 
            return SituacaoAtendimento == SituacaoAtendimento.Aguardando || SituacaoAtendimento == SituacaoAtendimento.Chamado; 
        }

    }
}
