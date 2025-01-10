import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { LoginService } from '../../services/login.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css'],
  imports: [RouterModule]
})
export class LayoutComponent implements OnInit {

  constructor(private loginService: LoginService, private route: Router) { }

  ngOnInit(): void {
  }

  sair() {
    this.loginService.removerTokenLocalStorage();
    this.route.navigate(['login']);
  }

}
