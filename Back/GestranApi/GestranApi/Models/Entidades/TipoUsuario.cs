using System.ComponentModel.DataAnnotations;
namespace GestranApi.Models.Entidades
{
    public class TipoUsuario
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(30)]
        public string Descricao { get; set; }
    }
}