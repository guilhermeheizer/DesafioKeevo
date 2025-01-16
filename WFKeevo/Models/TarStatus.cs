using System.ComponentModel;

namespace WFKeevo.Models
{
    public enum TarStatus
    {
        [Description("Incluida")]
        Incluida = 1,

        [Description("Executando")]
        Executando = 2,

        [Description("Finalizada")]
        Finalizada = 3,

        [Description("Cancelada")]
        Cancelada = 4 
    }
}
