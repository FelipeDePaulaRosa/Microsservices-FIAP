using GeracaoSenha.Data;
using GeracaoSenha.Data.Dtos;
using GeracaoSenha.Model;
using GeracaoSenha.Model.Enum;
using Microsoft.AspNetCore.Mvc;

namespace GeracaoSenha.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilaController : LogController<FilaController>
    {

        public FilaController(ILogger<FilaController> logger) : base(logger)
        {

        }

        /// <summary>
        /// Retorna a lista de atendimentos pendentes por ordem de chegada.
        /// </summary>
        /// <returns>IEnumerable</returns>
        /// <response code="200"></response>
        /// 
        [HttpGet]
        public IEnumerable<ReadAtendimentoDto> ConsultarFila()
        {
            try
            {
                _logger.LogInformation("\n\n\nIniciando consulta da fila de atendimento.");
                List<ReadAtendimentoDto> listaDto = new List<ReadAtendimentoDto>();
                AtendimentosContext.FilaOrdenada().ForEach(atendimento => listaDto.Add(new ReadAtendimentoDto(atendimento)));
                _logger.LogInformation("Consulta realizada com Sucesso.");
                return listaDto;

            }
            catch (Exception ex)
            {
                GravarBadRequest(ex);
                return null;
            }
        }

        /// <summary>
        /// Retorna o próximo atendimento pendentes.
        /// </summary>
        /// <returns>ReadAtendimentoDto</returns>
        /// <response code="200"></response>
        /// 
        [HttpGet("proximo")]
        public ReadAtendimentoDto ConsultarProximo()
        {
            try
            {
                _logger.LogInformation("\n\n\nIniciando consulta do próximo atendimento.");
                var atendimento = AtendimentosContext.FilaOrdenada().FirstOrDefault();
                var retorno = new ReadAtendimentoDto(atendimento);
                _logger.LogInformation("Consulta realizada com Sucesso.");
                return retorno;
            }
            catch (Exception ex)
            {
                GravarBadRequest(ex);
                return null;
            }
        }

        /// <summary>
        /// Retorna a posição de um atendimento na fila com base na senha.
        /// </summary>
        /// <param name="senha">Código da senha do atendimento.</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso a senha exista.</response> 
        /// <response code="404">Caso a senha não exista.</response> 
        /// 
        [HttpGet("posicao/{senha}")]
        public IActionResult ConsultarPosicao(string senha)
        {
            try
            {
                _logger.LogInformation($"\n\n\nIniciando consulta da posição na fila. Senha: {senha}.");
                var pendentes = AtendimentosContext.Atendimentos.Where(atendimento => atendimento.IsPendente()).OrderBy(atendimento => atendimento.HorarioChegada).ToList();
                var atendimento = AtendimentosContext.ConsultarAtendimentoPelaSenha(senha);
                ReadPosicaoDto poscaoDto = new ReadPosicaoDto();
                poscaoDto.Posicao = pendentes.IndexOf(atendimento) + 1;
                if (poscaoDto.Posicao > 0)
                {
                    _logger.LogInformation("Consulta realizada com sucesso.");
                    return Ok(poscaoDto);
                }
                return RetornarNotFound("Atendimento");
            }
            catch (Exception ex)
            {
                GravarBadRequest(ex);
                return null;
            }
        }
    }
}
