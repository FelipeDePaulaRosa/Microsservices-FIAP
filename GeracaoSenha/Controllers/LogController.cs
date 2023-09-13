using Microsoft.AspNetCore.Mvc;

namespace GeracaoSenha.Controllers
{
    public class LogController<T> : ControllerBase
    {
        protected readonly ILogger<T> _logger;

        public LogController(ILogger<T> logger)
        {
            _logger = logger;
        }

        protected IActionResult RetornarBadRequest(Exception ex)
        {
            var erro = "Um erro ocorreu: " + ex.Message;
            _logger.LogError(erro);
            return BadRequest(erro);
        }

        protected IActionResult RetornarNotFound(string objeto)
        {
            string erro = objeto + "não encontrado";
            _logger.LogError(erro);
            return NotFound(erro);
        }
        protected void GravarBadRequest(Exception ex)
        {
            var erro = "Um erro ocorreu: " + ex.Message;
            _logger.LogError(erro);
            Response.StatusCode = 400;
        }


    }
}
