using System.ComponentModel.DataAnnotations;
namespace GestranApi.Helpers.Enumeradores
{
    public enum EnumTipoUsuario
    {
        [Display(Name = "Supervisor")]
        Supervisor = 1,
        [Display(Name = "Executor")]
        Executor = 2
    }
}