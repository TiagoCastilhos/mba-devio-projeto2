import { Component, inject } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  standalone: false,
})
export class LoginComponent {
  fb = inject(FormBuilder);
  loginForm: FormGroup<LoginForm> = this.fb.group<LoginForm>({
    email: this.fb.nonNullable.control<string>('teste@teste.com', [Validators.required]),
    senha:  this.fb.nonNullable.control<string>('Senha@123', [Validators.required]),
  });
}

interface LoginForm {
  email: FormControl<string>;
  senha: FormControl<string>;
}
