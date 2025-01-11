import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { LocalStorageService } from '../../services/localstorage.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css'],
  imports: [RouterModule]
})
export class LayoutComponent implements OnInit {

  constructor(private route: Router, private localStorageService: LocalStorageService) { }

  ngOnInit(): void {
  }

  sair() {
    this.localStorageService.removerTokenLocalStorage();
    this.route.navigate(['login']);
  }

}
