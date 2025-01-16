using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using WFKeevo.Data;
using WFKeevo.Models;
using System.Linq;

namespace WFKeevo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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
        public async Task<IActionResult> GetTarefas()
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
        [Authorize(Roles = "Gerente,Empregado,Administrador")]
        public async Task<IActionResult> PostTarefa([FromBody] Models.Lancto lancto)
        {
            Tarefa tarefa = await _context.Tarefa.FindAsync(lancto.Tarefa);
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
        [Authorize(Roles = "Gerente,Empregado,Administrador")]
        public async Task<IActionResult> PutTarefa([FromBody] Lancto lancto)
        {
            try
            {
                var lanctoItem = await _context.Lancto.FindAsync(lancto.LanId);
                if (lancto.LanId == lanctoItem.LanId)
                {
                    _context.Lancto.Update(lancto);
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
                    return Ok("Lançamento informado não encontrado.");
                }
            } catch (Exception e)
            {
                return BadRequest($"Erro na alteração de Lançamento. Exceção: {e.Message}");
            }
        }


        //[HttpDelete("{tarCodigo}")]
        //[Authorize(Roles = "Gerente,Administrador")]
        //public async Task<IActionResult> DeleteTarefa([FromRoute] Guid lanId)
        //{
        //    try
        //    {
        //        Lancto lancto = await _context.Lancto.FindAsync(tarCodigo);
        //        var tarefa = await _context.Tarefa.FindAsync(tarCodigo);

        //        if (tarefa == null)
        //        {
        //            return NotFound($"Tarefa informada não existe: {tarCodigo}");
        //        }

        //        if (tarefa != null && lancto == null)
        //        {
        //            _context.Tarefa.Remove(tarefa);
        //            var valor = await _context.SaveChangesAsync();

        //            if (valor == 1)
        //            {
        //                return Ok($"Tarefa excluida com sucesso: {tarCodigo}");
        //            } else
        //            {
        //                return NotFound("Tarefa não foi excluida.");
        //            }
        //        } else
        //        {
        //            return NotFound($"Tarefa informada possui lançamentos de horas, não pode excluir: {tarCodigo}");
        //        }

        //    } catch (Exception e)
        //    {
        //        return BadRequest($"Erro na alteração de Tarefa. Exceção: {e.Message}");
        //    }
        //}

        /// <summary>
        /// Busca lançamentos
        /// </summary>
        /// <param name="valor"></param>
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