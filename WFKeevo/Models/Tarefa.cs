using System;
using System.ComponentModel.DataAnnotations;

namespace WFKeevo.Models
{
    public class Tarefa
    {
        /// <summary>
        /// O código da tarefa é informado pelo usuário
        /// </summary>
        [Key]
        public int TarCodigo { get; set; }

        [Required(ErrorMessage = "O nome da tarefa é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Informe o nome da tarefa entre 3 e 100 caracteres")]
        public string TarNome { get; set; }

        /// <summary>
        /// Data / hora que a tarefa foi criada
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime? TarDataInicio { get; set; }

        /// <summary>
        /// Data / hora que a tarefa foi finalizada ou cancelada
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime? TarDataFinal { get; set; }

        /// <summary>
        /// Status: I - Incluida, E - Executando, F - Finalizada, C - Cancelada
        /// Os usuários somente podem lançar horas trabalhadas quando o status for 'E'
        /// </summary>
        public TarStatus TarStatus { get; set; } = (TarStatus) 1;

    }
}
