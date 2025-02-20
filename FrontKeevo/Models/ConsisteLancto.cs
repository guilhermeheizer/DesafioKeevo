using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontKeevo.Models
{
    public class ConsisteLancto
    {
        public class ConsisteLanctoResquest
        {
            public Guid Id { get; set; }
            public DateTime LanDataInicial { get; set; }
            public DateTime? LanDataFinal { get; set; }
            public bool InclusaoRegistro { get; set; }
        }
    }
}
