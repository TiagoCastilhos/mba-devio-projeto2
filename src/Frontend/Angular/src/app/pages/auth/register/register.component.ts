import { Component, inject } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationService } from '@services/authentication.service';
import { ToasterService } from '@services/toaster.service';
import { passwordMatchValidator, passwordRegex } from '../validators/password-match.validator';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
  standalone: false,
})
export class RegisterComponent {
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

  onSubmit() {
    if (!this.canSubmit()) {
      return;
    }

    const register = this.registerForm.getRawValue();
    this.authenticationService.register(register.email, register.password).subscribe({
      next: (res) => {
        if (res.success) {
          this.toasterService.success('Usuário registrado com sucesso!');
          this.router.navigate(['']);
        }
      },
      error: (err) => {
        this.toasterService.error(err.error.errors);
      },
    });
  }

  canSubmit(): boolean {
    return this.registerForm.valid;
  }
}

interface RegisterForm {
  email: FormControl<string>;
  password: FormControl<string>;
  confirmPassword: FormControl<string>;
}
