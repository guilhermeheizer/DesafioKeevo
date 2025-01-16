using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontKeevo.Models
{
    public class Tarefa
    {
        public int TarCodigo { get; set; }
        public string TarNome { get; set; }
        public DateTime TarDataInicio { get; set; }
        public DateTime? TarDataFinal { get; set; }
        public int TarStatus { get; set; }
    }
}
