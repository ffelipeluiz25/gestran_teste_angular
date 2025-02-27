﻿using GestranApi.DTOs;
using GestranApi.DTOs.Checklist;
using GestranApi.Models.Entidades;
namespace GestranApi.Service.Interface
{
    public interface IChecklistService : IBaseServices<Checklist>
    {
        List<ChecklistDTO> ListarChecklist(int IdTipoUsuario, int IdUsuarioLogado);
        ChecklistDTO InserirChecklist(ChecklistRequestDTO checklist);
        RetornoApiDTO AssumeExecucaoChecklist(AssumeExecucaoChecklistRequestDTO request);
        RetornoApiDTO Atualizar(ChecklistAtualizarRequestDTO request);
        RetornoApiDTO AtualizarStatus(ChecklistAtualizarRequestDTO checklistRequest);
        RetornoApiDTO ExecutarChecklist(ChecklistExecutaRequestDTO checklistRequest);
    }
}