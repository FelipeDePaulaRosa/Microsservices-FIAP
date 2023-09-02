namespace GeracaoSenha.Model.Enum
{
    public class SiglaTipoAtendimento
    {
        public static string GetSiglaTipoAtendimento(TipoAtendimento tipoAtendimento)
        {
            switch (tipoAtendimento)
            {
                case TipoAtendimento.EXAME:
                    return "EX";
                    break;
                case TipoAtendimento.CONSULTA:
                    return "CO";
                    break;
                case TipoAtendimento.EXAME_PREFERENCIAL:
                    return "EP";
                    break;
                case TipoAtendimento.CONSULTA_PREFERENCIAL:
                    return "CP";
                    break;
                case TipoAtendimento.CIRURGIA:
                    return "CI";
                    break;
                default:
                    return "";
                    break;
            }
        }

        public static TipoAtendimento GetTipoAtendimentoSigla(String sigla)
        {
            switch (sigla)
            {
                case "EX":
                    return TipoAtendimento.EXAME;
                    break;
                case "CO":
                    return TipoAtendimento.CONSULTA;
                    break;
                case "EP":
                    return TipoAtendimento.EXAME_PREFERENCIAL;
                    break;
                case "CP":
                    return TipoAtendimento.CONSULTA_PREFERENCIAL;
                    break;
                case "CI":
                    return TipoAtendimento.CIRURGIA;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("A sigla " + sigla + "não corresponde a um tipo de atendimento.");
                    break;
            }
        }
    }
}
