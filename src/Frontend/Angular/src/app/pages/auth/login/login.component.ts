import { Component, inject } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationService } from '@services/authentication.service';
import { ToasterService } from '@services/toaster.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  standalone: false,
})
export class LoginComponent {
  private fb = inject(FormBuilder);
  private router = inject(Router);
  private authenticationService = inject(AuthenticationService);
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

  onSubmit() {
    if (!this.canSubmit()) {
      return;
    }

    const login = this.loginForm.getRawValue();
    this.authenticationService.login(login.email, login.password).subscribe({
      next: (res) => {
        if (res.success) {
          this.toasterService.success('Login efetuado com sucesso!');
          this.router.navigate(['']);
        }
      },
      error: (err) => {
        this.toasterService.error(err.error.errors);
      },
    });
  }

  canSubmit(): boolean {
    return this.loginForm.valid;
  }
}

interface LoginForm {
  email: FormControl<string>;
  password: FormControl<string>;
}
