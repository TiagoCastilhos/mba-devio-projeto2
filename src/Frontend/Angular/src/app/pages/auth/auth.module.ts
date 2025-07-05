import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { BaseButtonComponent } from "@components/base-button/base-button.component";
import { BaseInputComponent } from '@components/base-input/base-input.component';
import { authRoutes } from './auth.routes';
import { LoginComponent } from './login/login.component';
import { RegistrarComponent } from './registrar/registrar.component';

@NgModule({
  declarations: [LoginComponent, RegistrarComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(authRoutes),
    BaseInputComponent,
    BaseButtonComponent
  ],
  providers: [],
})
export class AuthModule { }
