import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChecklistService } from '../../../services/checklist.service';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import Swal from 'sweetalert2';
import { Checklist } from '../../../model/checklist.model';
import { ChecklistItemService } from '../../../services/checklistitem.service';
import { ChecklistItem } from '../../../model/checklistItem.model';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-checklist-edit',
  templateUrl: './checklist-edit.component.html',
  styleUrls: ['./checklist-edit.component.css'],
  imports: [CommonModule, ReactiveFormsModule, RouterModule]
})
export class ChecklistEditComponent implements OnInit {
  public formChecklist: FormGroup;
  public isDisabled = true;
  public listaChecklistItem: ChecklistItem[] = [];
  public modalDetalhesItem: NgbModalRef | null = null;
  public observacaoItem?: string = '';

  constructor(private checklistService: ChecklistService, private modalService: NgbModal, private checklistItemService: ChecklistItemService, private router: Router, private fb: FormBuilder, private activedRoute: ActivatedRoute) {
    this.formChecklist = this.buildFormChecklist();
  }

  ngOnInit(): void {
    const Idchecklist = Number(this.activedRoute.snapshot.paramMap.get('id'));
    this.checklistService.listarPorId(Idchecklist).subscribe(
      res => {
        this.formChecklist.patchValue(res);
        this.listarChecklistItens();
      },
      err => {
        Swal.fire('Atenção!', err, 'error');
      }
    )
  }

  private listarChecklistItens() {
    const Idchecklist = Number(this.activedRoute.snapshot.paramMap.get('id'));
    this.checklistItemService.listarPorIdChecklist(Idchecklist).subscribe(
      res => {
        this.listaChecklistItem = res;
      },
      err => {
        Swal.fire('Atenção!', err, 'error');
      }
    )

  }

  private buildFormChecklist(): FormGroup {
    return this.fb.group({
      id: [null, Validators.required],
      descricao: [null, Validators.required]
    })
  }

  public isFormControlInvalid(controlName: string): boolean {
    return !!(this.formChecklist.get(controlName)?.invalid && this.formChecklist.get(controlName)?.touched)
  }

  public updateChecklist() {
    const checklist: Checklist = this.formChecklist.value as Checklist;

    this.checklistService.update(checklist).subscribe(
      res => {
        //this.formClient.reset();
        //this.toastr.success(`Client ${client.name} atualizado.`)
        //this.router.navigate(['clients'])
      },
      err => {
        //this.toastr.error(`Falha ao atualziar ${client.name}.`)
      }
    )

  }

  public openModalDetalhesItem(detalhesItemModal: any, chkItem: ChecklistItem | null) {

    this.modalDetalhesItem = this.modalService.open(detalhesItemModal, {
      centered: true,
      keyboard: false,
      backdrop: 'static',
    });
    this.observacaoItem = chkItem?.observacaoItem;
  }

  closeModal(): void {
    if (this.modalDetalhesItem) {
      this.modalDetalhesItem.close();
    }
  }

}