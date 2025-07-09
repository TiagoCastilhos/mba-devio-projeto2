import { Component, inject } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { AutenticacaoService } from '@services/autenticacao.service';
import { ToasterService } from '@services/toaster.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  standalone: false,
})
export class LoginComponent {
  private fb = inject(FormBuilder);
  private authenticationService = inject(AutenticacaoService);
  private toasterService = inject(ToasterService);
  protected loginForm: FormGroup<LoginForm> = this.fb.group<LoginForm>({
    email: this.fb.nonNullable.control<string>('', [
      Validators.required,
      Validators.email
    ]),
    password: this.fb.nonNullable.control<string>('', [
      Validators.required
    ]),
  });

  enviarFormulario() {
    if (!this.estaValido()) {
      return;
    }

    const login = this.loginForm.getRawValue();
    this.authenticationService.logar(login.email, login.password).subscribe({
      next: (res) => {
        if (res.success) {
          this.toasterService.sucesso('Login efetuado com sucesso!');
        }
      },
      error: (err) => {
        this.toasterService.erro(err.error.errors);
      },
    });
  }

  estaValido(): boolean {
    return this.loginForm.valid;
  }
}

interface LoginForm {
  email: FormControl<string>;
  password: FormControl<string>;
}
