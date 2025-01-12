import { Component, OnInit } from '@angular/core';
import { Checklist } from '../../../model/checklist.model';
import { ChecklistService } from '../../../services/checklist.service';
import { CommonModule } from '@angular/common';
import Swal from 'sweetalert2';
import { Router, RouterModule } from '@angular/router';
import { ChecklistNewComponent } from '../new/checklist-new.component';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { LocalStorageService } from '../../../services/localstorage.service';

@Component({
  selector: 'app-checklist',
  templateUrl: './checklist.component.html',
  styleUrls: ['./checklist.component.css'],
  imports: [CommonModule, RouterModule, ChecklistNewComponent],
})

export class ChecklistComponent implements OnInit {

  public checklist: Array<Checklist> = [];
  public typeStartTest: number = 0;
  public modalNovoChecklist: NgbModalRef | null = null;
  public habilitaBotaoCadastro: boolean = false;
  public habilitaBotaoAssumeExecucao: boolean = false;
  public habilitaBotaoExclusao: boolean = false;
  constructor(private checklistService: ChecklistService, private route: Router, private modalService: NgbModal, private localStorageService: LocalStorageService) { }

  ngOnInit(): void {
    this.carregarTela();
  }

  public carregarTela() {
    let tipoUsuario = Number(this.localStorageService.getTipoUsuario());
    let idUsuarioLogado = Number(this.localStorageService.getIdUsuarioLogado());
    if (tipoUsuario == 1){
      this.habilitaBotaoCadastro = true;
      this.habilitaBotaoExclusao = true;
    }
    else
      this.habilitaBotaoAssumeExecucao = true;

    this.checklistService.listarchecklist(tipoUsuario, idUsuarioLogado).subscribe(
      res => { this.checklist = res; },
    )
  }

  public executarChecklist(check: Checklist) {

    Swal.fire({
      title: 'Tem certeza?',
      text: `Deseja assumir a execução do checklist de id: ${check.id}`,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Sim, confirmar!',
      cancelButtonText: 'Cancelar',
    }).then((result) => {
      if (result.isConfirmed) {
        this.checklistService.assumirExecucaoChecklist(check).subscribe(
          res => {
            Swal.fire('Confirmado!', `Você agora é responsável pelo checklist ${check.descricao}!`, 'success');
            this.carregarTela();
          },
          err => {
            Swal.fire('Cancelado!', 'Erro ao tentar assumir a execução do checklist.', 'error');
          }
        )
      } else {
        return;
      }
    });
  }

  public removerCheck(checkId: any) {
    Swal.fire({
      title: 'Tem certeza?',
      text: `Deseja excluir o checklist de id: ${checkId}`,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Sim, confirmar!',
      cancelButtonText: 'Cancelar',
    }).then((result) => {
      if (result.isConfirmed) {
        this.checklistService.delete(checkId).subscribe(
          res => {
            Swal.fire('Confirmado!', `Cliente de ID ${checkId} excluído com sucesso!`, 'success');
            this.checklist = this.checklist.filter(e => e.id !== checkId);
          },
          err => {
            Swal.fire('Cancelado!', 'Erro ao excluir checklist.', 'error');
          }
        )
      } else {
        return;
      }
    });
  }

  public onClickEdit() {
    var obj = new Object(11);
    this.route.navigate(['edit'], { queryParams: obj });
  }

  updateList($event: Checklist) {
    this.checklist.push($event)
    this.closeModal();
  }

  openModalNovoChecklist(newChecklist: any) {
    this.modalNovoChecklist = this.modalService.open(newChecklist, {
      centered: true,
      keyboard: false,
      backdrop: 'static',
    });
  }

  closeModal(): void {
    if (this.modalNovoChecklist) {
      this.modalNovoChecklist.close();
    }
  }

}