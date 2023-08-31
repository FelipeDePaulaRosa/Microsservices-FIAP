﻿using GeracaoSenha.Data;
using GeracaoSenha.Data.Dtos;
using GeracaoSenha.Model;
using GeracaoSenha.Model.Enum;
using Microsoft.AspNetCore.Mvc;

namespace GeracaoSenha.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilaController : ControllerBase
    {
        private readonly ILogger<FilaController> _logger;

        public FilaController(ILogger<FilaController> logger)
        {
            _logger = logger;
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
            List<Atendimento> lista = new List<Atendimento>();
            List<ReadAtendimentoDto> listaDto = new List<ReadAtendimentoDto>();
            lista.AddRange(AtendimentosContext.AtendimentosConsulta);
            lista.AddRange(AtendimentosContext.AtendimentosExame);
            lista.ForEach(atendimento => listaDto.Add(new ReadAtendimentoDto(atendimento)));
            return listaDto.Where(atendimentoDto => !atendimentoDto.Atendimento.Atendido).OrderBy(atendimentoDto => atendimentoDto.Atendimento.HorarioChegada);
        }

        /// <summary>
        /// Retorna a posição de um atendimento na fila com base na senha.
        /// </summary>
        /// <param name="senha">Código da senha do atendimento.</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso a senha exista.</response> 
        /// <response code="404">Caso a senha não exista.</response> 
        /// 

        [HttpGet("{senha}")]
        public IActionResult ConsultarPosicao(string senha)
        {
            List<Atendimento> lista = new List<Atendimento>();
            lista.AddRange(AtendimentosContext.AtendimentosConsulta);
            lista.AddRange(AtendimentosContext.AtendimentosExame);
            var pendentes = lista.Where(atendimento => !atendimento.Atendido).OrderBy(atendimento => atendimento.HorarioChegada).ToList();
            var atendimento = AtendimentosContext.ConsultarAtendimentoPelaSenha(senha);
            ReadPosicaoDto poscaoDto = new ReadPosicaoDto();
            poscaoDto.Posicao = pendentes.IndexOf(atendimento) + 1;
            if (poscaoDto.Posicao > 0)
            {
                return Ok(poscaoDto);
            }
            return NotFound();
        }
    }
}
