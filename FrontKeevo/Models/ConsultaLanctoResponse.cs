using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontKeevo.Models
{
    public class ConsultaLanctoResponse
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
}
