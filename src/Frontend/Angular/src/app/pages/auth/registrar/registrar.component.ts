import { Component, inject } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationService } from '@services/authentication.service';
import { ToasterService } from '@services/toaster.service';
import { passwordMatchValidator, passwordRegex } from '../validators/password-match.validator';

@Component({
  selector: 'app-registrar',
  templateUrl: './registrar.component.html',
  styleUrl: './registrar.component.css',
  standalone: false,
})
export class RegistrarComponent {
  private fb = inject(FormBuilder);
  private router = inject(Router);
  private authenticationService = inject(AuthenticationService);
  private toasterService = inject(ToasterService);
  protected registerForm: FormGroup<RegisterForm> = this.fb.group<RegisterForm>(
    {
      email: this.fb.nonNullable.control<string>('', [
        Validators.required,
        Validators.email
      ]),
      password: this.fb.nonNullable.control<string>('', [
        Validators.required,
        Validators.pattern(passwordRegex)
      ]),
      confirmPassword: this.fb.nonNullable.control<string>(''),
    },
    {
      validators: passwordMatchValidator()
    }
  );

  enviarFormulario() {
    if (!this.estaValido()) {
      return;
    }

    const register = this.registerForm.getRawValue();
    this.authenticationService.registrar(register.email, register.password).subscribe({
      next: (res) => {
        if (res.success) {
          this.toasterService.sucesso('Usuário registrado com sucesso!');
          this.router.navigate(['']);
        }
      },
      error: (err) => {
        this.toasterService.erro(err.error.errors);
      },
    });
  }

  estaValido(): boolean {
    return this.registerForm.valid;
  }
}

interface RegisterForm {
  email: FormControl<string>;
  password: FormControl<string>;
  confirmPassword: FormControl<string>;
}
