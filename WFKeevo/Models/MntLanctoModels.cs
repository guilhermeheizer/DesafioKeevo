using System;
using System.Collections.Generic;
using WFKeevo.Data;

namespace WFKeevo.Models
{
    public class MntLancto
    {
        private WFKeevoDBContext context;

        public MntLancto(WFKeevoDBContext context)
        {
            this.context = context;
        }

        public class ConsultaLanctoRequest
        {
            public string UsuarioLogin { get; set; }
            public string UsuarioNome { get; set; }
            public DateTime? DataInicial { get; set; }
            public DateTime? DataFinal { get; set; }
            public int? TarCodigo { get; set; }
            public string TarNome { get; set; }
            public string UsuarioFuncao { get; set; }
            public char HorarioAberto { get; set; }
        }

        public class ConsultaLanctoReponse
        {
            public string Login { get; set; }
            public string Nome { get; set; }
            public int? TarCodigo { get; set; }
            public string TarNome { get; set; }
            public string Funcao { get; set; }
            public DateTime? LanDataInicial { get; set; }
            public DateTime? LanDataFinal { get; set; }
            public Guid Id { get; set; }
            public Guid LanId { get; set; }
        }

        public class ConsisteLanctoResquest
        {
            public Guid Id { get; set; }
            public DateTime LanDataInicial { get; set; }
            public DateTime? LanDataFinal { get; set; }
            public bool InclusaoRegistro { get; set; }
        }
        public class ConsisteLanctoResponse
        {
            public string MensagemErro { get; set; }
            public bool Sucesso => MensagemErro == null;
        }
        public class ArrayDeHora
        {
            public ArrayDeHora() { }
            public int HoraIni {  get; set; }
            public int HoraFim { get; set; }
            public ArrayDeHora(int horaIni, int horaFim)
            {
                this.HoraIni = horaIni;
                this.HoraFim = horaFim;
            }
        }
    }
}
