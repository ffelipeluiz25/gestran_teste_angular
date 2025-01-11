import { AfterViewInit, Component, ElementRef, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import Swal from 'sweetalert2';
import { Item } from '../../../model/item.model';
import { ItemService } from '../../../services/item.service';

@Component({
  selector: 'app-item-new',
  templateUrl: './item-new.component.html',
  styleUrls: ['./item-new.component.css'],
  imports: [CommonModule, ReactiveFormsModule],
})
export class ItemNewComponent implements OnInit, AfterViewInit {
  public formItem: FormGroup;
  @Output() novoItem: EventEmitter<Item> = new EventEmitter();
  @ViewChild('nomeField') nomeField!: ElementRef;

  constructor(private fb: FormBuilder, private ItemService: ItemService) {
    this.formItem = this.buildFormItem();
  }

  ngOnInit(): void {
  }

  ngAfterViewInit() {
    this.nomeField.nativeElement.focus();
  }

  private buildFormItem(): FormGroup {
    return this.fb.group({
      nome: [null, [Validators.required]],
      observacao: [null, [Validators.required]]
    })
  }

  public isFormControlInvalid(controlName: string): boolean {
    return !!(this.formItem.get(controlName)?.invalid && this.formItem.get(controlName)?.touched)
  }

  saveNovoItem() {
    const saveNovoItem: Item = this.formItem.value as Item;
    this.ItemService.salvaNovo(saveNovoItem).subscribe(
      res => {
        Swal.fire('Confirmado!', `Item salvo com sucesso!`, 'success');
        this.formItem.reset();
        this.novoItem.emit(this.mapToItem(res));
      },
      err => {
        console.log(err);
        Swal.fire('Cancelado!', 'Erro! Verifique os campos obrigatórios ao salvar novo item.', 'error');
      }
    )

    console.log(this.formItem.value);
  }


  private mapToItem(data: any): Item {
    return data;
  }

}