using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontKeevo.Models
{
    public class Lancto
    {
        public Guid? LanId { get; set; }
        public DateTime LanDataInicio { get; set; }
        public DateTime? LanDataFinal { get; set; }
        public Guid UsuarioId { get; set; }
        public int TarCodigo { get; set; }
    }
}
