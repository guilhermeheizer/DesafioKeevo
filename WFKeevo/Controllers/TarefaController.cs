using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WFKeevo.Data;
using WFKeevo.Models;

namespace WFKeevo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TarefaController : Controller
    {
        private readonly WFKeevoDBContext _context;

        public TarefaController(WFKeevoDBContext context)
        {
            _context = context;
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
        /// Inclusão de tarefa
        /// </summary>
        /// <param name="tarefa"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Gerente,Empregado,Administrador")]
        public async Task<IActionResult> PostTarefa([FromBody] Tarefa tarefa)
        {
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
        [Authorize(Roles = "Gerente,Empregado,Administrador")]
        public async Task<IActionResult> PutTarefa([FromBody] Tarefa tarefa)
        {
            try
            {
                var tarefaItem = await _context.Tarefa.FindAsync(tarefa.TarCodigo);
                if (tarefa.TarCodigo == tarefaItem.TarCodigo && tarefaItem.TarCodigo != 0)
                {
                    _context.Tarefa.Update(tarefa);
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
        /// Busca uma tarefa específica
        /// </summary>
        /// <param name="tarCodigo"></param>
        /// <returns></returns>
        [HttpDelete("{tarCodigo}")]
        [Authorize(Roles = "Gerente,Administrador")]
        public async Task<IActionResult> DeleteTarefa([FromRoute] int tarCodigo)
        {
            try
            {
                Lancto lancto = await _context.Lancto.FindAsync(tarCodigo);
                var tarefa = await _context.Tarefa.FindAsync(tarCodigo);

                if ( tarefa == null)
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
        // param name="valor"></param>

        /// <summary>
        /// Busca tarefa por nome
        /// </summary>
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
        public async Task<IActionResult> GetEstadoPaginacao([FromQuery] string valor, int skip, int take, bool ordemDesc)
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
                    .Skip(skip)
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
