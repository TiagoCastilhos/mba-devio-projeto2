import { Component, inject } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { AuthenticationService } from '../../../services/authentication.service';
import { ToasterService } from '../../../services/toaster.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  standalone: false,
})
export class LoginComponent {
  fb = inject(FormBuilder);
  authenticationService = inject(AuthenticationService);
  toasterService = inject(ToasterService);
  loginForm: FormGroup<LoginForm> = this.fb.group<LoginForm>({
    email: this.fb.nonNullable.control<string>('cliente@teste.com', [
      Validators.required,
    ]),
    password: this.fb.nonNullable.control<string>('Senha@123', [
      Validators.required,
    ]),
  });

  onSubmit() {
    if (!this.loginForm.valid) {
      console.log('invalid');
      return;
    }
    const login = this.loginForm.getRawValue();
    this.authenticationService.login(login.email, login.password).subscribe({
      next: (res) => {
        if (res.success)
          this.toasterService.success('Login efetuado com sucesso!');
      },
      error: (err) => {
        this.toasterService.success(err);
      },
    });
  }
}

interface LoginForm {
  email: FormControl<string>;
  password: FormControl<string>;
}
