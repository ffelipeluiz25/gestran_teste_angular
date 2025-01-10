using System.ComponentModel.DataAnnotations;
namespace GestranApi.Models.Entidades
{
    public class Usuario
    {
        [Key]
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