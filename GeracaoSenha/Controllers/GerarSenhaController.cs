using GeracaoSenha.Model;
using Microsoft.AspNetCore.Mvc;

namespace GeracaoSenha.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GerarSenhaController : ControllerBase
    {
        public GerarSenhaController(ILogger<GerarSenhaController> logger)
        {
        }

        [HttpGet]
        public IActionResult GetSenha([FromQuery] int codigoTipoAtendimento)
        {
            TipoAtendimento tipoAtendimento = (TipoAtendimento)codigoTipoAtendimento;
            int sequencia = Atendimentos.ProximoAtendimento(tipoAtendimento);
            string senha = tipoAtendimento == TipoAtendimento.Consulta ? "C" : "E";
            senha += sequencia.ToString().PadLeft(4, '0');
            return Ok(new Retorno(senha));
        }
    }

    public class Atendimentos
    {
        public static int ProximoAtendimento(TipoAtendimento tipoAtendimento)
        {
            switch (tipoAtendimento)
            {
                case TipoAtendimento.Exame:
                    return 1;
                    break;
                case TipoAtendimento.Consulta:
                    return 2;
                    break;
                default:
                    return 0;
                    break;
            }
        }
    }

    public class Retorno
    {
        public String Senha { get; set; }
        public String Link => "https://hospital/acompanhamentofila/" + Senha;

        public Retorno(String _Senha)
        {
            Senha = _Senha;
        }
    }
}