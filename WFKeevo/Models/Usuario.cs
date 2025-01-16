using System;
using System.ComponentModel.DataAnnotations;

namespace WFKeevo.Models
{
    public class Usuario
    {
        /// <summary>
        /// Código do usuário
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Nome do usuário
        /// </summary>
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "O campo Nome deve ter entre 3 e 200 caracteres.")]
        public string Nome { get; set; }

        /// <summary>
        /// Login para entrar no sistema
        /// </summary>
        [Required(ErrorMessage = "O campo Login é obrigatório.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "O campo Login deve ter entre 3 e 20 caracteres.")]
        public string Login { get; set; }

        /// <summary>
        /// Senha
        /// </summary>
        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo Senha deve ter entre 3 e 100 caracteres.")]
        public string Password { get; set; }

        /// <summary>
        /// Função do usuário na Keevo
        /// </summary>
        [Required(ErrorMessage = "O campo Funcao é obrigatório.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "O campo Funcao deve ter entre 3 e 20 caracteres.")]
        public string Funcao { get; set; }
    }
}
