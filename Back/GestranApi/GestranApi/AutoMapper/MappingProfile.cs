using AutoMapper;
using GestranApi.DTOs.Checklist;
using GestranApi.DTOs.Item;
using GestranApi.Models.Entidades;
namespace GestranApi.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Checklist, ChecklistDTO>().ReverseMap();
            CreateMap<Checklist, ChecklistRequestDTO>().ReverseMap();
            CreateMap<Checklist, ChecklistAtualizarRequestDTO>().ReverseMap();
            CreateMap<Checklist, ChecklistExecutaRequestDTO>().ReverseMap();

            CreateMap<Item, ItemDTO>().ReverseMap();
            CreateMap<Item, ItemRequestDTO>().ReverseMap();

        }
    }
}