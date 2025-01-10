import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { LoginService } from '../../services/login.service';
import { CommonModule } from '@angular/common';
import swal from 'sweetalert2';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  imports: [CommonModule, ReactiveFormsModule],
})
export class LoginComponent implements OnInit {

  public formLogin: FormGroup;

  constructor(private fb: FormBuilder, private loginService: LoginService, private route: Router) {
    this.formLogin = this.criarFormLogin();
  }

  ngOnInit(): void {
  }


  public criarFormLogin(): FormGroup {
    return this.fb.group({
      username: ["", [Validators.required, Validators.minLength(6)]],
      password: ["", [Validators.required, Validators.minLength(6)]]
    })
  }


  public isFormControlInvalid(controlName: string): boolean {
    return !!(this.formLogin.get(controlName)?.invalid && this.formLogin.get(controlName)?.touched)
  }


  public submitForm() {
    const { username, password } = this.formLogin.value;
    this.formLogin.reset;

    this.loginService.login(username, password).subscribe(
      res => {
        swal.fire('', 'Login efetuado com sucesso', 'success')
          .then((result) => {
            this.route.navigate(['/home']);
          });

      },
      err => {
        swal.fire('', err, 'error');
      }
    )
  }

}
