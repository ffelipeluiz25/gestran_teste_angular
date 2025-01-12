import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { LocalStorageService } from '../../services/localstorage.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css'],
  imports: [RouterModule, CommonModule]
})
export class LayoutComponent implements OnInit {

  public nomeUsuarioLoginSuperior: string | null = '';
  public isDropdownOpen: boolean = false;
  constructor(private route: Router, private localStorageService: LocalStorageService) { }

  ngOnInit(): void {
    this.nomeUsuarioLoginSuperior = this.localStorageService.getNomeUsuario();
  }

  toggleDropdown() {
    this.isDropdownOpen = !this.isDropdownOpen;
  }

  sair() {
    this.localStorageService.removerTokenLocalStorage();
    this.route.navigate(['login']);
  }

}
