using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WFKeevo.Data;
using WFKeevo.Models;

namespace WFKeevo.Modulo
{
    public class TarefaModulo
    {
        private readonly WFKeevoDBContext _context;
        public TarefaModulo(WFKeevoDBContext context)
        {
            _context = context;
        }

        public async Task<TarefaResultado> BuscaTarefaAsync([FromRoute] int tarCodigo)
        {
            if (tarCodigo == 0)
            {
                return new TarefaResultado { MensagemErro = "Informe o código da tarefa." };
            }

            var tarefa = await _context.Tarefa.FindAsync(tarCodigo);

            if (tarefa == null)
            {
                return new TarefaResultado { MensagemErro = "Tarefa não encontrada." };
            }

            return new TarefaResultado { Tarefa = tarefa };
        }
    }
}
