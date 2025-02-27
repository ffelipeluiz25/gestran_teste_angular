﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GestranApi.Models.Entidades
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(50)]
        public string NomeCompleto { get; set; }
        public int IdTipoUsuario { get; set; }
        [MaxLength(20)]
        public string Login { get; set; }
        [MaxLength(50)]
        public string Senha { get; set; }
    }
}