using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WFKeevo.Data;
using WFKeevo.Models;
using WFKeevo.Modulo;
using WFKeevo.Services;

namespace WFKeevo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TarefaController : Controller
    {
        private readonly WFKeevoDBContext _context;
        private readonly TokenService _service;
        private readonly Modulo.TarefaModulo _modulo;

        public TarefaController(WFKeevoDBContext context, TokenService service)
        {
            _context = context;
            _service = service;
            _modulo = new Modulo.TarefaModulo(_context);
        }

        /// <summary>
        /// Busca todas as tarefas da tabela Tarefa
        /// </summary>
        /// <returns>Retornar um json com todos os atributos.</returns>
        [HttpGet]
        public async Task<IActionResult> GetTarefas()
        {
            try
            {
                var result = _context.Tarefa.ToList();
                return Ok(result);

            } catch (Exception e)
            {
                return BadRequest($"Erro na listagem de Tarefas. Exceção: {e.Message}");
            }
        }

        /// <summary>
        /// Inclusão de Paginacao
        /// </summary>
        /// <param name="tarefa"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostTarefa([FromBody] Tarefa tarefa)
        {
            var tarefaExiste = await _context.Tarefa.FindAsync(tarefa.TarCodigo);

            if (tarefaExiste != null)
            {
                return Ok($"Tarefa informada já cadastrada: {tarefa.TarCodigo}");
            }

            try
            {
                await _context.Tarefa.AddAsync(tarefa);
                var valor = await _context.SaveChangesAsync();

                if (valor == 1)
                {
                    return Ok("Tarefa incluida com sucesso.");
                } else
                {
                    return BadRequest("Tarefa não incluída.");
                }
            } catch (Exception e)
            {
                return BadRequest($"Erro na inclusão de Tarefa. Exceção: {e.Message}");
            }
        }

        /// <summary>
        /// Alteração da tarefa
        /// </summary>
        /// <param name="tarefa"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> PutTarefa([FromBody] Tarefa tarefa)
        {
            try
            {
                var tarefaItem = await _context.Tarefa.AsNoTracking().FirstOrDefaultAsync(t => t.TarCodigo == tarefa.TarCodigo);
                if (tarefaItem != null)
                {
                    tarefaItem.TarNome = tarefa.TarNome;
                    tarefaItem.TarDataInicio = tarefa.TarDataInicio;
                    tarefaItem.TarDataFinal = tarefa.TarDataFinal;
                    tarefaItem.TarStatus = tarefa.TarStatus;

                    _context.Tarefa.Update(tarefaItem);
                    var valor = await _context.SaveChangesAsync();

                    if (valor == 1)
                    {
                        return Ok("Tarefa alterada com sucesso.");
                    } else
                    {
                        return BadRequest("Tarefa não alterada.");
                    }
                } else
                {
                    return Ok("Tarefa informada não encontrada.");
                }
            } catch (Exception e)
            {
                return BadRequest($"Erro na alteração de Tarefa. Exceção: {e.Message}");
            }
        }

        /// <summary>
        /// Exclui uma tarefa específica
        /// </summary>
        /// <param name="tarCodigo"></param>
        /// <returns></returns>
        [HttpDelete("{tarCodigo}")]
        public async Task<IActionResult> DeleteTarefa([FromRoute] int tarCodigo)
        {
            try
            {
                Lancto lancto = await _context.Lancto.AsNoTracking().FirstOrDefaultAsync(t => t.TarCodigo == tarCodigo);
                var tarefa = await _context.Tarefa.AsNoTracking().FirstOrDefaultAsync(t => t.TarCodigo == tarCodigo);

                if (tarefa == null)
                {
                    return NotFound($"Tarefa informada não existe: {tarCodigo}");
                }

                if (tarefa != null && lancto == null)
                {
                    _context.Tarefa.Remove(tarefa);
                    var valor = await _context.SaveChangesAsync();

                    if (valor == 1)
                    {
                        return Ok($"Tarefa excluida com sucesso: {tarCodigo}");
                    } else
                    {
                        return NotFound("Tarefa não foi excluida.");
                    }
                } else
                {
                    return NotFound($"Tarefa informada possui lançamentos de horas, não pode excluir: {tarCodigo}");
                }

            } catch (Exception e)
            {
                return BadRequest($"Erro na alteração de Tarefa. Exceção: {e.Message}");
            }
        }

        /// <summary>
        /// Busca uma tarefa especifica
        /// </summary>
        /// <param name="tarCodigo"></param>
        /// <returns></returns>
        [HttpGet("{tarCodigo}")]
        public async Task<IActionResult> GetTarefa([FromRoute] int tarCodigo)
        {
            try
            {
                var Tarefa = await _context.Tarefa.FindAsync(tarCodigo);

                if (Tarefa == null)
                {
                    return NotFound($"Tarefa informada não existe: {tarCodigo}");
                }

                if (Tarefa.TarCodigo == tarCodigo && Tarefa.TarCodigo != 0)
                {
                    return Ok(Tarefa);
                } else
                {
                    return NotFound($"Erro: Tarefa informada não existe: {tarCodigo}");
                }

            } catch (Exception e)
            {
                return BadRequest($"Erro na consulta de Tarefa. Exceção: {e.Message}");
            }
        }

        /// <summary>
        /// Busca uma tarefa específica por código usando o módulo
        /// </summary>
        /// <param name="tarCodigo"></param>
        /// <returns></returns>
        [HttpGet("BuscarPorCodigo/{tarCodigo}")]
        public async Task<IActionResult> GetBuscaTarefa([FromRoute] int tarCodigo)
        {
            var resultado = await _modulo.BuscaTarefaAsync(tarCodigo);

            if (!resultado.Sucesso)
            {
                return NotFound(resultado.MensagemErro);
            }

            return Ok(resultado.Tarefa);
        }

        /// <summary>
        /// Busca tarefa por nome
        /// </summary>
        /// param name="valor"></param>
        /// <returns></returns>
        [HttpGet("Pesquisa")]
        public async Task<IActionResult> GetTarefaPesquisa([FromQuery] string valor)
        {
            try
            {
                // Query Criteria
                var lista = from o in _context.Tarefa.ToList()
                            where o.TarNome.ToUpper().Contains(valor.ToUpper())
                            select o;
                return Ok(lista);

            } catch (Exception e)
            {
                return BadRequest($"Erro na pesquisa de Tarefa. Exceção: {e.Message}");
            }
        }

        /// <summary>
        /// Busca tarefas por página
        /// </summary>
        /// <param name="valor"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="ordemDesc"></param>
        /// <returns></returns>
        [HttpGet("Paginacao")]
        public async Task<IActionResult> GetEstadoPaginacao([FromQuery] string? valor, int skip, int take, bool ordemDesc)
        {
            try
            {
                // Query Criteria
                var query = _context.Tarefa.AsQueryable();

                if (!string.IsNullOrWhiteSpace(valor))
                {
                    valor = valor.ToUpper();
                    query = query.Where(o => o.TarNome.ToUpper().Contains(valor));
                }

                // Sorting
                query = ordemDesc
                    ? query.OrderByDescending(o => o.TarNome)
                    : query.OrderBy(o => o.TarNome);

                // Get total count before applying pagination
                long qtde = query.Count();

                // Apply pagination

                var lista = query
                    .Skip((skip - 1) * take)
                    .Take(take)
                    .ToList();

                var paginacaoResponse = new PaginacaoResponse<Tarefa>(lista, qtde, skip, take);

                return Ok(paginacaoResponse);
            } catch (Exception e)
            {
                return BadRequest($"Erro na pesquisa de Tarefa. Exceção: {e.Message}");
            }
        }
    }
}