﻿using AutoMapper;
using Azure.Core;
using GestranApi.DTOs;
using GestranApi.DTOs.Checklist;
using GestranApi.DTOs.Item;
using GestranApi.Helpers.Enumeradores;
using GestranApi.Models.Entidades;
using GestranApi.Repository.Interface;
using GestranApi.Service.Interface;
namespace GestranApi.Service
{
    public class ChecklistService : BaseServices<Checklist>, IChecklistService
    {
        private readonly IMapper _mapper;
        private readonly IChecklistRepository _checklistRepository;
        private readonly IChecklistItemRepository _checklistItemRepository;

        public ChecklistService(IMapper mapper,
                                IChecklistRepository checklistServiceRepository,
                                IChecklistItemRepository checklistItemRepository) : base(checklistServiceRepository)
        {
            _mapper = mapper;
            _checklistRepository = checklistServiceRepository;
            _checklistItemRepository = checklistItemRepository;
        }

        public List<ChecklistDTO> ListarChecklist(int IdTipoUsuario, int IdUsuarioLogado)
        {
            return _checklistRepository.ListarChecklist(IdTipoUsuario, IdUsuarioLogado);
        }

        public ChecklistDTO InserirChecklist(ChecklistRequestDTO checklistRequest)
        {
            var checklist = _mapper.Map<Checklist>(checklistRequest);
            checklist.IdStatus = (int)EnumStatus.PENDENTE;
            _repository.Inserir(checklist);
            _repository.SaveChanges();

            foreach (ItemDTO item in checklistRequest.ListaItens)
            {
                var checkListItem = new ChecklistItem();
                checkListItem.IdItem = item.Id;
                checkListItem.IdChecklist = checklist.Id;
                checkListItem.IdUsuarioAlteracao = checklist.IdUsuarioAlteracao;
                checkListItem.Executado = false;
                _checklistItemRepository.Inserir(checkListItem);
            }


            return _mapper.Map<ChecklistDTO>(checklist);
        }

        public RetornoApiDTO AssumeExecucaoChecklist(AssumeExecucaoChecklistRequestDTO request)
        {
            var checklist = _checklistRepository.ListarPorId(request.Id);
            if (checklist.IdUsuarioExecutor != null)
                return new RetornoApiDTO(false, "O checklist já está sendo executado por outro executor!");

            return _checklistRepository.AssumeExecucaoChecklist(request, checklist);
        }

        public RetornoApiDTO Atualizar(ChecklistAtualizarRequestDTO request)
        {
            return _checklistRepository.Atualizar(request);
        }

        public RetornoApiDTO AtualizarStatus(ChecklistAtualizarRequestDTO checklistRequest)
        {
            return _checklistRepository.AtualizarStatus(checklistRequest);
        }

        public RetornoApiDTO ExecutarChecklist(ChecklistExecutaRequestDTO checklistRequest)
        {
            return _checklistRepository.ExecutarChecklist(checklistRequest);
        }
    }
}