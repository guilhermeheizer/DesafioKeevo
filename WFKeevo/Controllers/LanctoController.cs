using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using WFKeevo.Data;
using WFKeevo.Models;
using System.Linq;
using static WFKeevo.Models.MntLancto;
using Newtonsoft.Json;

namespace WFKeevo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LanctoController : Controller
    {
        private readonly WFKeevoDBContext _context;
        private readonly object e;

        public LanctoController(WFKeevoDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Busca todas as lançamentos da tabela Lancto
        /// </summary>
        /// <returns>Retornar um json com todos os atributos.</returns>
        [HttpGet]
        public async Task<IActionResult> GetLancto()
        {
            try
            {
                var result = _context.Lancto.ToList();
                return Ok(result);

            } catch (Exception e)
            {
                return BadRequest($"Erro na listagem de Lançamentos. Exceção: {e.Message}");
            }
        }

        /// <summary>
        /// Inclusão de lançamento de horas
        /// </summary>
        /// <param name="lancto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostLancto([FromBody] Models.Lancto lancto)
        {
            Tarefa tarefa = await _context.Tarefa.FindAsync(lancto.TarCodigo);
            if (tarefa == null)
            {
                return NotFound($"Tarefa informada no lançamento das horas não existe: {lancto.Tarefa}");
            }

            Usuario usuario = await _context.Usuario.FindAsync(lancto.UsuarioId);
            if (usuario == null)
            {
                return NotFound($"Usuário informado no lançamento das horas não existe");
            }

            try
            {
                await _context.Lancto.AddAsync(lancto);
                var valor = await _context.SaveChangesAsync();

                if (valor == 1)
                {
                    return Ok("Lançamento incluido com sucesso.");
                } else
                {
                    return BadRequest("Lançamento não incluído.");
                }
            } catch (Exception e)
            {
                return BadRequest($"Erro na inclusão de Lançamento. Exceção: {e.Message}");
            }
        }

        /// <summary>
        /// Alteração de lançamento
        /// </summary>
        /// <param name="lancto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> PutLancto([FromBody] Lancto lancto)
        {
            try
            {
                var lanctoItem = await _context.Lancto.FindAsync(lancto.LanId);

                if (lanctoItem != null)
                {
                    lanctoItem.LanDataInicio = lancto.LanDataInicio;
                    lanctoItem.LanDataFinal = lancto.LanDataFinal;
                    lanctoItem.TarCodigo = lancto.TarCodigo;
                    lanctoItem.UsuarioId = lancto.UsuarioId;

                    _context.Lancto.Update(lanctoItem);
                    var valor = await _context.SaveChangesAsync();

                    if (valor == 1)
                    {
                        return Ok("Lançamento alterado com sucesso.");
                    } else
                    {
                        return BadRequest($"Erro na alteração de Lançamento.");
                    }
                } else
                {
                    return NotFound("Lançamento não encontrado.");
                }
            } catch (Exception e)
            {
                return BadRequest($"Erro na alteração de Lançamento. Exceção: {e.Message}");
            }
        }

        /// <summary>
        /// Exclusão de lançamento
        /// </summary>
        /// <param name="lanId"></param>
        /// <returns></returns>
        [HttpDelete("{lanId}")]
        public async Task<IActionResult> DeleteLancto([FromRoute] Guid lanId)
        {
            try
            {
                Lancto lancto = await _context.Lancto.FindAsync(lanId);

                if (lancto == null)
                {
                    return NotFound("Lançamento selecionado não existe.");
                }

                _context.Lancto.Remove(lancto);
                var valor = await _context.SaveChangesAsync();

                if (valor == 1)
                {
                    return Ok("Lançamento excluido com sucesso.");
                } else
                {
                    return NotFound("Lançamento não foi excluido.");
                }
            } catch (Exception e)
            {
                return BadRequest($"Erro na deleção do Lançamento. Exceção: {e.Message}");
            }
        }
    }
}