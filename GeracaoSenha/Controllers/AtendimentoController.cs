using GeracaoSenha.Data;
using GeracaoSenha.Data.Dtos;
using GeracaoSenha.Model;
using GeracaoSenha.Model.Enum;
using Microsoft.AspNetCore.Mvc;

namespace GeracaoSenha.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AtendimentoController : LogController<AtendimentoController>
    {

        public AtendimentoController(ILogger<AtendimentoController> logger) : base(logger)
        {

        }

        /// <summary>
        /// Registro inicial do atendimento e gera��o da senha. Retorna o atendimento com sua senha e link para acompanhamento.
        /// </summary>
        /// <param name="TipoAtendimento">Valor necess�rio para identificar o tipo do atendimento. [EXAME, CONSULTA, EXAME_PREFERENCIAL, CONSULTA_PREFERENCIAL, CIRURGIA]</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso o registro seja realizado com sucesso</response> 
        /// <response code="400">Caso o tipo de atendimento n�o exista.</response> 
        /// 
        [HttpPost]
        public IActionResult RegistrarAtendimento([FromBody] CreateAtendimentoDto atendimentoDto)
        {
            try
            {
                _logger.LogInformation("\n\n\nIniciando registro do atendimento.");
                Atendimento atendimento = AtendimentosContext.NovoAtendimento(atendimentoDto.TipoAtendimento);
                var retorno = new ReadAtendimentoDto(atendimento);
                _logger.LogInformation("Atendimento registrado com Sucesso.");
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return RetornarBadRequest(ex);
            }
        }

        /// <summary>
        /// Registra a chamada de um atendimento ao guich�.
        /// </summary>
        /// <param name="senha">C�digo da senha a ser finalizada.</param>
        /// <param name="guiche">N�mero do guich�.</param>
        /// <returns>IActionResult</returns>
        /// <response code="204">Caso o registro seja realizado com sucesso</response> 
        /// <response code="404">Caso a senha n�o exista.</response> 
        /// 
        [HttpPut("chamar/{senha}")]
        public IActionResult Chamar(string senha, [FromBody] ChamarAtendimentoDto chamarDto)
        {
            try
            {
                _logger.LogInformation($"\n\n\nIniciando consulta do atendimento {senha}: Chamar.");
                Atendimento atendimento = AtendimentosContext.ConsultarAtendimentoPelaSenha(senha);
                if (atendimento == null)
                {
                    return RetornarNotFound("Atendimento");
                }
                atendimento.Chamar(chamarDto.Guiche);
                _logger.LogInformation("Registro atualizado com sucesso.");
                return NoContent();
            }
            catch (Exception ex)
            {
                return RetornarBadRequest(ex);
            }
        }

        /// <summary>
        /// Registra o in�cio de um atendimento no guich�.
        /// </summary>
        /// <param name="senha">C�digo da senha.</param>
        /// <returns>IActionResult</returns>
        /// <response code="204">Caso o registro seja realizado com sucesso</response> 
        /// <response code="404">Caso a senha n�o exista.</response> 
        /// 
        [HttpPut("iniciar/{senha}")]
        public IActionResult IniciarAtendimento(string senha)
        {
            try
            {
                _logger.LogInformation($"\n\n\nIniciando consulta do atendimento {senha}: Iniciar.");
                Atendimento atendimento = AtendimentosContext.ConsultarAtendimentoPelaSenha(senha);
                if (atendimento == null)
                {
                    return RetornarNotFound("Atendimento");
                }
                atendimento.Iniciar();
                _logger.LogInformation("Registro atualizado com sucesso.");
                return NoContent();
            }
            catch (Exception ex)
            {
                return RetornarBadRequest(ex);
            }
        }

        /// <summary>
        /// Registra a finaliza��o de um atendimento no guich�.
        /// </summary>
        /// <param name="senha">C�digo da senha a ser finalizada.</param>
        /// <returns>IActionResult</returns>
        /// <response code="204">Caso o registro seja realizado com sucesso</response> 
        /// <response code="404">Caso a senha n�o exista.</response> 
        /// 
        [HttpPut("finalizar/{senha}")]
        public IActionResult FinalizarAtendimento(string senha)
        {
            try
            {
                _logger.LogInformation($"\n\n\nIniciando consulta do atendimento {senha}: Finalizar.");
                Atendimento atendimento = AtendimentosContext.ConsultarAtendimentoPelaSenha(senha);
                if (atendimento == null)
                {
                    return RetornarNotFound("Atendimento");
                }
                atendimento.Finalizar();
                _logger.LogInformation("Registro atualizado com sucesso.");
                return NoContent();
            }
            catch (Exception ex)
            {
                return RetornarBadRequest(ex);
            }
        }

        /// <summary>
        /// Consulta um atendimento com base na senha gerada.
        /// </summary>
        /// <param name="senha">C�digo da senha do atendimento.</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso a senha exista.</response> 
        /// <response code="404">Caso a senha n�o exista.</response> 
        /// 
        [HttpGet("{senha}")]
        public IActionResult ConsultarAtendimento(string senha)
        {

            try
            {
                _logger.LogInformation($"\n\n\nIniciando consulta do atendimento {senha}.");
                Atendimento atendimento = AtendimentosContext.ConsultarAtendimentoPelaSenha(senha);
                if (atendimento == null)
                {
                    return RetornarNotFound("Atendimento");
                }
                var retorno = new ReadAtendimentoDto(atendimento);
                _logger.LogInformation("Consulta realizada com sucesso.");
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return RetornarBadRequest(ex);
            }
        }
    }
}