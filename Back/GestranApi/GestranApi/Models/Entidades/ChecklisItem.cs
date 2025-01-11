using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GestranApi.Models.Entidades
{
    public class ChecklistItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("fkChecklistItem_Checklist")]
        public int IdChecklist { get; set; }
        public Checklist Checklist { get; set; }
        [ForeignKey("fkChecklistItem_Item")]
        public int IdItem { get; set; }
        public Item Item { get; set; }
        public int IdUsuarioAlteracao { get; set; }
        public Usuario UsuarioAlteracao { get; set; }
        public bool Executado { get; set; }
    }
}