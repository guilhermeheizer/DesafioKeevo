using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WFKeevo.Data;
using WFKeevo.Services;
using WFKeevo.Modulo;
using static WFKeevo.Models.MntLancto;
using System;
using Newtonsoft.Json;
using System.Drawing;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using WFKeevo.Models;

namespace WFKeevo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MntLanctoController : ControllerBase
    {
        private readonly WFKeevoDBContext _context;
        private readonly TokenService _service;
        private readonly Modulo.MntLancto _modulo;
        private object _mntLancto;

        public MntLanctoController(WFKeevoDBContext context, TokenService service)
        {
            _context = context;
            _service = service;
            _modulo = new Modulo.MntLancto(_context);
        }

        /// <summary>
        /// Retorna a lista dos lançamento de horas
        /// <returns></returns>
        [HttpGet("ConsultaLancto")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginacaoResponse<ConsultaLanctoReponse>))]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public IActionResult ConsultaLancto([FromQuery] string valor, int skip, int take, bool ordemDesc)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(valor))
                {
                    return BadRequest("Parâmetro 'valor' não pode ser nulo ou vazio.");
                }

                var consultaLanctoRequest = JsonConvert.DeserializeObject<ConsultaLanctoRequest>(valor);

                if (consultaLanctoRequest == null)
                {
                    return BadRequest("Erro na deserialização do 'valor'. Verifique o formato JSON.");
                }

                var paginacaoResponse = _modulo.ConsultaLancto(consultaLanctoRequest, skip, take, ordemDesc);
                return Ok(paginacaoResponse);
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro inesperado: {ex.Message}");
            }
        }

        [HttpPost("ConsisteLancto")]
        public ActionResult<Models.MntLancto.ConsisteLanctoResponse> ConsisteLancto([FromBody] Models.MntLancto.ConsisteLanctoResquest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = _modulo.ConsisteLancto(request);

            if (response.Sucesso)
            {
                return Ok(null);
            } else
            {
                return BadRequest(response.MensagemErro);
            }
        }
    }
}