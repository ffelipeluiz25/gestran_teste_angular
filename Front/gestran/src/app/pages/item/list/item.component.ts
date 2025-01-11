import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import Swal from 'sweetalert2';
import { Router, RouterModule } from '@angular/router';
import { ItemNewComponent } from '../new/item-new.component';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { LocalStorageService } from '../../../services/localstorage.service';
import { Item } from '../../../model/item.model';
import { ItemService } from '../../../services/item.service';

@Component({
  selector: 'app-item',
  templateUrl: './item.component.html',
  styleUrls: ['./item.component.css'],
  imports: [CommonModule, RouterModule, ItemNewComponent],
})

export class ItemComponent implements OnInit {

  public Itens: Array<Item> = [];
  public typeStartTest: number = 0;
  public modalNovoItem: NgbModalRef | null = null;
  public habilitaBotaoCadastro: boolean = false;
  constructor(private itemService: ItemService, private route: Router, private modalService: NgbModal, private localStorageService: LocalStorageService) { }

  ngOnInit(): void {

    let tipoUsuario = Number(this.localStorageService.getTipoUsuario());
    if (tipoUsuario == 1)
      this.habilitaBotaoCadastro = true;

    this.itemService.listarTodos().subscribe(
      res => { this.Itens = res; }
    )
  }

  public removerItem(itemId: any) {
    Swal.fire({
      title: 'Tem certeza?',
      text: `Deseja excluir o Item de id: ${itemId}`,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Sim, confirmar!',
      cancelButtonText: 'Cancelar',
    }).then((result) => {
      if (result.isConfirmed) {
        this.itemService.delete(itemId).subscribe(
          res => {
            Swal.fire('Confirmado!', `Cliente de Id ${itemId} excluído com sucesso!`, 'success');
            this.Itens = this.Itens.filter(e => e.id !== itemId);
          },
          err => {
            Swal.fire('Cancelado!', 'Erro ao excluir Item.', 'error');
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

  updateList($event: Item) {
    this.Itens.push($event)
    this.closeModal();
  }

  openModalNovoItem(newItem: any) {
    this.modalNovoItem = this.modalService.open(newItem, {
      centered: true,
      keyboard: false,
      backdrop: 'static',
    });
  }

  closeModal(): void {
    if (this.modalNovoItem) {
      this.modalNovoItem.close(); // Fecha a modal usando a referência global
    }
  }

}