using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using WFKeevo.Data;
using WFKeevo.Models;
using WFKeevo.Services;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WFKeevo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsuarioController : Controller
    {
        private readonly WFKeevoDBContext _context;
        private readonly TokenService _service;

        public UsuarioController(WFKeevoDBContext context, TokenService service)
        {
            _context = context;
            _service = service;
        }

        /// <summary>
        /// Login do usuário, para o usuário cuja função é "Administrador" a senha não é verificad
        /// </summary>
        /// <param name="usuarioLogin"></param>
        /// <returns>Retorna os dados no usário e o token para ser utilizado em todos os endpoints</returns>
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UsuarioLogin usuarioLogin)
        {
            var usuario = _context.Usuario.Where(x => x.Login == usuarioLogin.Login).FirstOrDefault();

            if (usuario == null)
            {
                return NotFound("Usuário não existe.");
            }

            if ( usuario.Funcao != "Administrador")
            {
                var passwordHash = MD5Hash.CalcHash(usuarioLogin.Password);


                if (usuario.Password != passwordHash)
                {
                    return BadRequest("Senha inválida.");
                }
            }

            var token = _service.GerarToken(usuario);

            usuario.Password = "";

            var resut = new UsuarioResponse()
            {
                Usuario = usuario,
                Token = token
            };

            return Ok(resut);
        }

        /// <summary>
        /// Lista todos os usuários da tabela Usuario
        /// </summary>
        /// <returns>Para cada usuário retorna todos os atributos</returns>
        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            try
            {
                var result = _context.Usuario.ToList();
                return Ok(result);

            } catch (Exception e)
            {
                return BadRequest($"Erro na listagem de Usuario. Exceção: {e.Message}");
            }
        }

        /// <summary>
        /// Inclusão do usuário na tabela Usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostUsuario([FromBody] Models.Usuario usuario)
        {
            try
            {
                var listUsuario = _context.Usuario.Where(x => x.Login == usuario.Login).ToList();
                if (listUsuario.Count > 0)
                {
                    return BadRequest("Erro, Login informado já existe.");
                }

                string passwordHash = MD5Hash.CalcHash(usuario.Password);

                usuario.Password = passwordHash;

                await _context.Usuario.AddAsync(usuario);
                var valor = await _context.SaveChangesAsync();
                if (valor == 1)
                {
                    return Ok("Usuário incluído com sucesso.");
                } else
                {
                    return BadRequest("Erro Usuário não incluído.");
                }

            } catch (Exception e)
            {
                return BadRequest($"Erro na inclusão de Usuário. Exceção: {e.Message}");
            }
        }

        /// <summary>
        /// Alteração dos dados do usuário
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> PutUsuario([FromBody] Models.Usuario usuario)
        {
            try
            {
                string passwordHash = MD5Hash.CalcHash(usuario.Password);

                usuario.Password = passwordHash;

                _context.Usuario.Update(usuario);
                var valor = await _context.SaveChangesAsync();
                if (valor == 1)
                {
                    return Ok("Usuário alterado com sucesso.");
                } else
                {
                    return BadRequest("Erro Usuário não alterado.");
                }
            } catch (Exception e)
            {
                return BadRequest($"Erro na alteração de Usuário. Exceção: {e.Message}");
            }
        }

        /// <summary>
        /// Deleta o usuario da tabela Usuario, caso tenha horas lançadas na tabela Lancto a deleção não será efetivada
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario([FromRoute] Guid id)
        {
            try
            {
                Lancto lancto = await _context.Lancto.FindAsync(id);
                Usuario usuario = await _context.Usuario.FindAsync(id);

                if (usuario == null)
                {
                    return NotFound("Usuário não existe.");
                }

                if (usuario != null && lancto == null)
                {
                    _context.Usuario.Remove(usuario);
                    var valor = await _context.SaveChangesAsync();
                    if (valor == 1)
                    {
                        return Ok("Usuário excluído com sucesso.");
                    } else
                    {
                        return BadRequest("Erro Usuário não excluído.");
                    }
                } else
                {
                    return NotFound("Usuário informado possui lançamentos de horas, não pode excluir.");
                }
            } catch (Exception e)
            {
                return BadRequest($"Erro na alteração de Usuário. Exceção: {e.Message}");
            }
        }

        /// <summary>
        /// Busca os usuarios por nome ou login partir do paramentro informado
        /// </summary>
        /// <param name="valor"></param>
        /// <returns>Retorna uma lista de usuários</returns>
        [HttpGet("Busca")]
        public async Task<IActionResult> GetUsuario([FromQuery] string valor)
        {
            try
            {
                // Query criteria
                var lista = from u in _context.Usuario.ToList()
                            where u.Nome.ToUpper().Contains(valor.ToUpper())
                            || u.Login.ToUpper().Contains(valor.ToUpper())
                            select u;

                if (lista != null)
                {
                    return Ok(lista);
                } else
                {
                    return NotFound("Usuário encontrado.");
                }
            } catch (Exception e)
            {
                return BadRequest($"Erro na consulta de Usuário. Exceção: {e.Message}");
            }
        }

        /// <summary>
        /// Altera a senha de todos os usuários da tabela Usuario, a nova senha será o login do usuário que é criptografada
        /// </summary>
        /// <returns></returns>
        [HttpPut("AlteraSenhaGeral")]
        public async Task<IActionResult> AlteraSenhaGeral()
        {
            try
            {
                // Buscar todos os usuários
                var usuarios = await _context.Usuario.ToListAsync();

                if (usuarios != null && usuarios.Count > 0)
                {
                    // Atualizar a senha de todos os usuários
                    foreach (var usuario in usuarios)
                    {
                        usuario.Password = MD5Hash.CalcHash(usuario.Login);
                    }

                    await _context.SaveChangesAsync();
                    return Ok("Senha de todos os usuários alterada com sucesso.");
                } else
                {
                    return NotFound("Nenhum usuário encontrado.");
                }
            } catch (Exception e)
            {
                return BadRequest($"Erro na alteração das senhas. Exceção: {e.Message}");
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
                var query = _context.Usuario.AsQueryable();

                if (!string.IsNullOrWhiteSpace(valor))
                {
                    valor = valor.ToUpper();
                    query = query.Where(o => o.Nome.ToUpper().Contains(valor) ||
                                             o.Login.ToUpper().Contains(valor));
                }

                // Sorting
                query = ordemDesc
                    ? query.OrderByDescending(o => o.Nome)
                    : query.OrderBy(o => o.Nome);

                // Get total count before applying pagination
                long qtde = query.Count();

                // Apply pagination

                var lista = query
                    .Skip((skip - 1) * take)
                    .Take(take)
                    .ToList();

                var paginacaoResponse = new PaginacaoResponse<Usuario>(lista, qtde, skip, take);

                return Ok(paginacaoResponse);
            } catch (Exception e)
            {
                return BadRequest($"Erro na pesquisa de Usuario. Exceção: {e.Message}");
            }
        }
    }
}

