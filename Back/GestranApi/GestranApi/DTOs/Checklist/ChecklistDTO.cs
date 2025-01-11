using GestranApi.Helpers;
using GestranApi.Helpers.Enumeradores;

namespace GestranApi.DTOs.Checklist
{
    public class ChecklistDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int? IdUsuarioExecutor { get; set; }
        public int IdStatus { get; set; }
        public int IdUsuarioAlteracao { get; set; }

        //Custom
        public string StatusNome
        {
            get
            {
                switch (IdStatus)
                {
                    case ((int)EnumStatus.PENDENTE):
                        {
                            return EnumStatus.PENDENTE.GetDisplayAttributeFrom(typeof(EnumStatus));
                        }
                    case ((int)EnumStatus.EXECUTANDO):
                        {
                            return EnumStatus.EXECUTANDO.GetDisplayAttributeFrom(typeof(EnumStatus));
                        }
                    case ((int)EnumStatus.FINALIZADOEXECUTOR):
                        {
                            return EnumStatus.FINALIZADOEXECUTOR.GetDisplayAttributeFrom(typeof(EnumStatus));
                        }
                    case ((int)EnumStatus.APROVADOSUPERVISOR):
                        {
                            return EnumStatus.APROVADOSUPERVISOR.GetDisplayAttributeFrom(typeof(EnumStatus));
                        }
                    case ((int)EnumStatus.REPROVADOSUPERVISOR):
                        {
                            return EnumStatus.REPROVADOSUPERVISOR.GetDisplayAttributeFrom(typeof(EnumStatus));
                        }
                }
                return EnumStatus.PENDENTE.GetDisplayAttributeFrom(typeof(EnumStatus));
            }
        }
        public string Responsavel { get; set; }
    }
}