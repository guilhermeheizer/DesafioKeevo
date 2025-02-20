using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WFKeevo.Models
{
    public class Lancto
    {
        /// <summary>
        /// Id do laçando
        /// </summary>
        [Key]
        public Guid? LanId { get; set; }

        /// <summary>
        /// Data / hora inicial da tarefa. 
        /// </summary>
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "A data/hora inicial é obrigatória.")]
        public DateTime LanDataInicio { get; set; }

        /// <summary>
        /// Data / hora que finalizou a tarefa no mesmo dia da data inicial.
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime? LanDataFinal { get; set; }

        /// <summary>
        /// O usuário que esta executando a tarefa
        /// </summary>
        [Required(ErrorMessage = "O Usuário é obrigatório.")]
        public Guid UsuarioId { get; set; }

        /// <summary>
        /// Tarefa que esta sendo executada
        /// </summary>
        [Required(ErrorMessage = "O Código do Lançamento é obrigatório.")]

        public int TarCodigo { get; set; }
        public Lancto() 
        {
            LanId = Guid.NewGuid();
        }

        // Relacionamento Entity Framework
        public Usuario Usuario { get; set; }
        public Tarefa Tarefa { get; set; }
    }
}