using static WFKeevo.Models.MntLancto;
using System;

namespace WFKeevo.Models
{
    public class ResquestLancto
    {
        private readonly ConsultaLanctoRequest consultaLanctoRequest = new ConsultaLanctoRequest
        {
            UsuarioLogin = "usuario123",
            UsuarioNome = "João Silva",
            DataInicial = DateTime.Now.AddMonths(-1),
            DataFinal = DateTime.Now,
            TarCodigo = 42,
            TarNome = "Tarefa X",
            UsuarioFuncao = "Administrador",
            HorarioAberto = 'S'
        };
    }
}