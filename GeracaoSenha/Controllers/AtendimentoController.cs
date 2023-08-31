using GeracaoSenha.Data;
using GeracaoSenha.Data.Dtos;
using GeracaoSenha.Model;
using GeracaoSenha.Model.Enum;
using Microsoft.AspNetCore.Mvc;

namespace GeracaoSenha.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AtendimentoController : ControllerBase
    {
        private readonly ILogger<AtendimentoController> _logger;

        public AtendimentoController(ILogger<AtendimentoController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Registra o início de um atendimento e retorna a senha gerada
        /// </summary>
        /// <param name="codigoTipoAtendimento">Valor necessário para identificar o tipo do atendimento.
        /// <br></br>
        /// 1 - Exame<br></br>
        /// 2 - Consulta<br></br>
        /// </param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso o registro seja realizado com sucesso</response> 
        /// <response code="400">Caso o tipo de atendimento não exista.</response> 
        /// 

        [HttpPost]
        public IActionResult RegistrarAtendimento([FromBody] CreateAtendimentoDto atendimentoDto)
        {
            _logger.LogTrace("Iniciando geração da senha.");
            if (!Enum.IsDefined(typeof(TipoAtendimento), atendimentoDto.CodigoTipoAtendimento))
            {
                _logger.LogError("Tipo de Atendimento inválido.");
                return BadRequest("Tipo de Atendimento inválido");
            }
            TipoAtendimento tipoAtendimento = (TipoAtendimento)atendimentoDto.CodigoTipoAtendimento;
            Atendimento atendimento = AtendimentosContext.NovoAtendimento(tipoAtendimento);
            var retorno = new ReadAtendimentoDto(atendimento);
            _logger.LogTrace("Senha gerada com Sucesso.");
            return Ok(retorno);
        }

        /// <summary>
        /// Registra a finalização de um atendimento no guichê.
        /// </summary>
        /// <param name="senha">Código da senha a ser finalizada.</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso o registro seja realizado com sucesso</response> 
        /// <response code="404">Caso a senha não exista.</response> 
        /// 

        [HttpPut("{senha}")]
        public IActionResult FinalizarAtendimento(string senha)
        {
            Atendimento atendimento = AtendimentosContext.ConsultarAtendimentoPelaSenha(senha);
            if (atendimento == null) return NotFound(); 
            atendimento.Atender();
            return NoContent();
        }

        /// <summary>
        /// Consulta um atendimento com base na senha gerada.
        /// </summary>
        /// <param name="senha">Código da senha do atendimento.</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso a senha exista.</response> 
        /// <response code="404">Caso a senha não exista.</response> 
        /// 

        [HttpGet("{senha}")]
        public IActionResult ConsultarAtendimento(string senha)
        {
            try
            {
                Atendimento atendimento = AtendimentosContext.ConsultarAtendimentoPelaSenha(senha);
                if (atendimento == null) return NotFound();
                var retorno = new ReadAtendimentoDto(atendimento);
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return Problem();
            }
        }


    }

}
