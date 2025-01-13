import { AfterViewInit, Component, ElementRef, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChecklistService } from '../../../services/checklist.service';
import { FormArray, FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Checklist } from '../../../model/checklist.model';
import Swal from 'sweetalert2';
import { ItemService } from '../../../services/item.service';
import { Item } from '../../../model/item.model';

@Component({
  selector: 'app-checklist-new',
  templateUrl: './checklist-new.component.html',
  styleUrls: ['./checklist-new.component.css'],
  imports: [CommonModule, ReactiveFormsModule, FormsModule]
})
export class ChecklistNewComponent implements OnInit, AfterViewInit {
  public formChecklist: FormGroup;
  public algumCheckFoiMarcado: boolean = false;
  public descricao: string = "";
  @Output() novoChecklist: EventEmitter<Checklist> = new EventEmitter();
  @ViewChild('descricaoField') descricaoField!: ElementRef;
  public Itens: Item[] = [];
  constructor(private fb: FormBuilder, private checklistService: ChecklistService, private itemService: ItemService) {
    this.formChecklist = this.buildFormChecklist();
  }

  ngOnInit(): void {
    this.itemService.listarTodos().subscribe(
      res => {
        this.Itens = res;
        this.Itens.forEach(item => {
          item.isSelected = false;
        });
      }
    )
  }

  ngAfterViewInit() {
    this.descricaoField.nativeElement.focus();
  }

  onCheckboxChange() {
    this.algumCheckFoiMarcado = this.Itens.filter(item => item.isSelected).length > 0;
  }


  private buildFormChecklist(): FormGroup {
    return this.fb.group({
      descricao: [null, [Validators.required]],
      algumCheckFoiMarcado: [null, [Validators.nullValidator]]
    })
  }

  public isFormControlInvalid(controlName: string): boolean {
    return !!(this.formChecklist.get(controlName)?.invalid && this.formChecklist.get(controlName)?.touched)
  }

  saveNovoChecklist() {
    var mensagem = this.validFormulario();
    if (mensagem != "") {
      Swal.fire({ title: 'Atenção!', html: mensagem, icon: 'error' });
      return;
    }

    const saveNovoChecklist: Checklist = this.formChecklist.value as Checklist;
    saveNovoChecklist.listaItens = this.Itens.filter(item => item.isSelected);
    this.checklistService.salvaNovo(saveNovoChecklist).subscribe(
      res => {
        Swal.fire('Confirmado!', `Checklist salvo com sucesso!`, 'success');
        this.formChecklist.reset();
        this.novoChecklist.emit(this.mapToChecklist(res));
      },
      err => {
        Swal.fire('Cancelado!', 'Erro! Verifique os campos obrigatórios ao salvar novo checklist.', 'error');
      }
    )

  }

  private validFormulario(): string {
    var mensagem: string = '';
    if (this.descricao == "")
      mensagem += `O campo descrição é obrigatório!<br>`;

    if (!this.algumCheckFoiMarcado)
      mensagem += `Selecione ao menos um Item!<br>`;

    return mensagem;
  }

  private mapToChecklist(data: any): Checklist {
    return data;
  }

}