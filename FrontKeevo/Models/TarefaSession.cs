using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontKeevo.Models
{
    public static class TarefaSession
    {
        public static int TarCodigo { get; set; }
        public static string TarNome { get; set; }
        public static DateTime TarDataInicio { get; set; }
        public static DateTime? TarDataFinal { get; set; }
        public static int TarStatus { get; set; }
        public static void LimparSessao()
        {
            TarCodigo = 0;
            TarNome = string.Empty;
            TarDataInicio = DateTime.MinValue;
            TarDataFinal = null;
            TarStatus = 0;
        }
    }
}
