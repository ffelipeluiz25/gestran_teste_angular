using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GestranApi.Models.Entidades
{
    public class Checklist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Descricao { get; set; }
        [ForeignKey("fkChecklist_UsuarioExecutor")]
        public int? IdUsuarioExecutor { get; set; }
        public Usuario? UsuarioExecutor { get; set; }

        [ForeignKey("fkChecklist_Status")]
        public int IdStatus { get; set; }
        public Status Status { get; set; }

        [ForeignKey("fkChecklist_UsuarioAlteracao")]
        public int IdUsuarioAlteracao { get; set; }
        public Usuario UsuarioAlteracao { get; set; }
        public List<ChecklistItem> ChecklistItens { get; set; }
    }
}