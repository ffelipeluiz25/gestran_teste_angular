import { Router } from '@angular/router';
import { Component, EventEmitter, HostListener, OnInit, Output } from '@angular/core';
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
  @Output() enterPress = new EventEmitter<void>();
  
  constructor(private fb: FormBuilder, private loginService: LoginService, private route: Router) {
    this.formLogin = this.criarFormLogin();
  }

  ngOnInit(): void {
  }

  @HostListener('keydown.enter')
  onEnterPress() {
    this.enterPress.emit();
    this.submitForm();
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

    this.loginService.login(username, password).subscribe(res => {
      this.route.navigate(['home']);
    }, err => {
      swal.fire('', err, 'error');
    });
  }

}
