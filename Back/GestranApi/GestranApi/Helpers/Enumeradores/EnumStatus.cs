using System.ComponentModel.DataAnnotations;
namespace GestranApi.Helpers.Enumeradores
{
    public enum EnumStatus
    {
        [Display(Name = "Pendente")]
        PENDENTE = 1,
        [Display(Name = "Executando")]
        EXECUTANDO = 2,
        [Display(Name = "Finalizado Executor")]
        FINALIZADOEXECUTOR = 3,
        [Display(Name = "Aprovado Supervisor")]
        APROVADOSUPERVISOR = 4,
        [Display(Name = "Reprovado Supervisor")]
        REPROVADOSUPERVISOR = 5
    }
}