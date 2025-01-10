using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GestranApi.Models.Entidades
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(20)]
        public string Nome { get; set; }
        [MaxLength(200)]
        public string Observacao { get; set; }
        public List<ChecklisItem> ItensChecklist { get; set; }
    }
}