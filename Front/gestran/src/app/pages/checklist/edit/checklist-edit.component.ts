import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChecklistService } from '../../../services/checklist.service';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { FormArray, FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import Swal from 'sweetalert2';
import { Checklist } from '../../../model/checklist.model';
import { ChecklistItemService } from '../../../services/checklistitem.service';
import { ChecklistItem } from '../../../model/checklistItem.model';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { LocalStorageService } from '../../../services/localstorage.service';
import { ChecklistExecutaRequest, ChecklistItemExecutaRequest } from '../../../model/ChecklistExecutaRequest.model';

@Component({
  selector: 'app-checklist-edit',
  templateUrl: './checklist-edit.component.html',
  styleUrls: ['./checklist-edit.component.css'],
  imports: [CommonModule, ReactiveFormsModule, RouterModule]
})
export class ChecklistEditComponent implements OnInit {
  public formChecklist: FormGroup;
  public nomeBotaoAcaoTelaPrincipal: string | null = null;
  public isDisabled = true;
  public listaChecklistItem: ChecklistItem[] = [];
  public modalDetalhesItem: NgbModalRef | null = null;
  public observacaoItem?: string = '';
  public modoVisualizacao: string | null = null;
  public modoVisualizacaoBool: boolean = false;
  public mostraCheckbox: boolean = true;
  public habilitaCheckbox: boolean = true;
  public desabilitarCampoDescricao: boolean = true;
  public idStatus?: Number;
  public mostrarBotaoReprovar: boolean = false;
  public mostrarBotaoPrincipal: boolean = true;

  constructor(private checklistService: ChecklistService,
    private route: Router,
    private modalService: NgbModal,
    private checklistItemService: ChecklistItemService,
    private fb: FormBuilder,
    private activedRoute: ActivatedRoute,
    private localStorageService: LocalStorageService
  ) {
    this.formChecklist = this.buildFormChecklist();
  }

  ngOnInit(): void {
    this.carregaTela();
  }

  private verificaModoVisualizacao() {
    this.modoVisualizacao = this.activedRoute.snapshot.queryParamMap.get('visualiza');
    if (this.modoVisualizacao != null && this.modoVisualizacao == '1')
      this.modoVisualizacaoBool = true;
    else {
      this.modoVisualizacaoBool = false;
    }

  }

  private verificaMostraCheckbox() {
    this.modoVisualizacao = this.activedRoute.snapshot.queryParamMap.get('visualiza');
    if (this.modoVisualizacao != null && this.modoVisualizacao == '1') {
      this.mostraCheckbox = false;
    }

    if (this.idStatus == 1)
      this.mostraCheckbox = false;
  }

  private verificaHabilitaCheckbox() {
    this.modoVisualizacao = this.activedRoute.snapshot.queryParamMap.get('visualiza');
    if (this.modoVisualizacao != null && this.modoVisualizacao == '1')
      this.habilitaCheckbox = false;

    if (this.idStatus == 3)
      this.habilitaCheckbox = false;
  }

  private carregaTela() {
    const Idchecklist = Number(this.activedRoute.snapshot.paramMap.get('id'));
    this.checklistService.listarPorId(Idchecklist).subscribe(
      res => {
        this.formChecklist.patchValue(res);
        this.listarChecklistItens();
        this.formataNomeBotaoAcaoTela(res);
        this.idStatus = res.idStatus;
        if (res.idStatus == 1)
          this.desabilitarCampoDescricao = false;

        this.mostrarBotaoPrincipal = this.visibilidadeBotaoPrincipal();
        this.mostrarBotaoReprovar = this.visibilidadeBotaoReprovar();

        this.verificaModoVisualizacao();
        this.verificaMostraCheckbox();
      },
      err => {
        Swal.fire('Atenção!', err, 'error');
      }
    )
  }

  public regraDesabilitarCampoDescricao(): boolean {
    if (this.modoVisualizacaoBool)
      return true;
    if (this.desabilitarCampoDescricao)
      return true;

    return false;
  }

  private formataNomeBotaoAcaoTela(result: Checklist) {
    let tipoUsuario = Number(this.localStorageService.getTipoUsuario());
    if (tipoUsuario == 1) {
      if (result.idStatus == 1) {
        this.nomeBotaoAcaoTelaPrincipal = 'Salvar';
      }
      else
        this.nomeBotaoAcaoTelaPrincipal = 'Aprovar';
    }
    else
      this.nomeBotaoAcaoTelaPrincipal = 'Salvar';
  }

  public visibilidadeBotaoPrincipal(): boolean {
    if (this.modoVisualizacaoBool)
      return false;

    return true;
  }

  public visibilidadeBotaoReprovar(): boolean {
    if (this.modoVisualizacaoBool)
      return false;

    let tipoUsuario = Number(this.localStorageService.getTipoUsuario());
    if (tipoUsuario == 2 && this.idStatus == 2)
      return false;
    else if (tipoUsuario == 1 && this.idStatus == 1)
      return false;

    return true;
  }

  private listarChecklistItens() {
    const Idchecklist = Number(this.activedRoute.snapshot.paramMap.get('id'));
    this.checklistItemService.listarPorIdChecklist(Idchecklist).subscribe(
      res => {
        this.listaChecklistItem = res;
        this.createChecklistForm();
      },
      err => {
        Swal.fire('Atenção!', err, 'error');
      }
    )

  }
  private createChecklistForm(): void {
    const checklistArray = this.fb.array(
      this.listaChecklistItem.map(item => this.fb.group({
        id: [item.id],
        nomeItem: [item.nomeItem],
        observacaoItem: [item.observacaoItem],
        executado: [item.executado]
      }))
    );
    this.formChecklist.setControl('checklist', checklistArray);
  }

  private buildFormChecklist(): FormGroup {
    return this.fb.group({
      id: [null, Validators.required],
      descricao: [null, Validators.required],
      checklist: this.fb.array([])
    })
  }

  get checklistFormArray(): FormArray {
    return this.formChecklist.get('checklist') as FormArray;
  }

  public isFormControlInvalid(controlName: string): boolean {
    return !!(this.formChecklist.get(controlName)?.invalid && this.formChecklist.get(controlName)?.touched)
  }

  public salvar() {
    debugger;
    const checklist: Checklist = this.formChecklist.value as Checklist;

    if (this.idStatus == 1)
      this.atualizacaoChecklist(checklist);
    else if (this.idStatus == 2)
      this.salvarChecklist(checklist);
    else if (this.idStatus == 3)
      this.aprovarChecklist(checklist);
  }

  public atualizacaoChecklist(checklist: Checklist) {
    if (checklist.descricao == '') {
      Swal.fire('Atenção!', `O campo descrição é obrigatório.`, 'warning');
      return;
    }

    checklist.idStatus = this.idStatus;
    this.checklistService.atualizar(checklist).subscribe(
      res => {
        Swal.fire('Confirmado!', `Cliente de Id ${checklist.id} alterado com sucesso!`, 'success');
        this.route.navigate(['checklist']);
      },
      err => {
        Swal.fire('Atenção!', `Falha ao atualziar ${checklist.descricao}`, 'warning');
      }
    )
  }

  public salvarChecklist(checklist: Checklist) {
    const itensMarcados = this.checklistFormArray.value.filter((item: ChecklistItem) => item.executado);
    if (itensMarcados.length == 0) {
      Swal.fire({ title: 'Atenção!', html: `Selecione ao menos um Item!`, icon: 'error' });
      return;
    }

    if (this.listaChecklistItem.length == itensMarcados.length)
      this.verificaExecucaoCompletaChecklist(checklist, itensMarcados);
    else
      this.executarChecklist(checklist, itensMarcados);
  }

  private verificaExecucaoCompletaChecklist(checklist: Checklist, itensMarcados: ChecklistItem[]) {

    Swal.fire({
      title: 'Tem certeza?',
      text: `Ao selecionar todos os itens o checklist será finalizado, deseja continuar?`,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Sim, confirmar!',
      cancelButtonText: 'Cancelar',
    }).then((result) => {
      if (result.isConfirmed) {
        debugger;
        checklist.idStatus = 3;
        this.executarChecklist(checklist, itensMarcados);
      } else {
        return;
      }
    });
  }

  private executarChecklist(checklist: Checklist, itensMarcados: ChecklistItem[]) {
    var idUsuarioAlteracao = Number(this.localStorageService.getTipoUsuario());
    var checklistExe = new ChecklistExecutaRequest(checklist.id, this.idStatus, idUsuarioAlteracao, []);

    itensMarcados.forEach((item: ChecklistItem) => {
      checklistExe.listaItens.push(new ChecklistItemExecutaRequest(item.id, true));
    });

    this.checklistService.executarChecklist(checklistExe).subscribe(
      res => {
        Swal.fire('Confirmado!', `Checklist salvo com sucesso!`, 'success');
        this.formChecklist.reset();
        this.route.navigate(['checklist']);
      },
      err => {
        Swal.fire('Cancelado!', 'Erro! Verifique os campos obrigatórios ao salvar novo checklist.', 'error');
      }
    )

  }

  public aprovarChecklist(checklist: Checklist) {
    checklist.idStatus = 4;
    this.checklistService.atualizarStatus(checklist).subscribe(
      res => {
        Swal.fire('Confirmado!', `Cliente de Id ${checklist.id} alterado com sucesso!`, 'success');
        this.route.navigate(['checklist']);
      },
      err => {
        Swal.fire('Atenção!', `Falha ao atualziar ${checklist.descricao}`, 'warning');
      }
    )
  }

  public reprovarChecklist() {
    const checklist: Checklist = this.formChecklist.value as Checklist;
    checklist.idStatus = 5;
    this.checklistService.atualizarStatus(checklist).subscribe(
      res => {
        Swal.fire('Confirmado!', `Cliente de Id ${checklist.id} alterado com sucesso!`, 'success');
        this.route.navigate(['checklist']);
      },
      err => {
        Swal.fire('Atenção!', `Falha ao atualziar ${checklist.descricao}`, 'warning');
      }
    )
  }

  public openModalDetalhesItem(detalhesItemModal: any, chkItem: any | null) {

    this.modalDetalhesItem = this.modalService.open(detalhesItemModal, {
      centered: true,
      keyboard: false,
      backdrop: 'static',
    });
    this.observacaoItem = chkItem.controls.observacaoItem.value
  }

  closeModal(): void {
    if (this.modalDetalhesItem) {
      this.modalDetalhesItem.close();
    }
  }

}