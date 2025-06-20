import { Component, inject } from '@angular/core';
import { AuthenticationService } from '../../../services/authentication.service';

@Component({
  selector: 'app-lista-produto',
  standalone: false,
  templateUrl: './lista-produto.component.html',
  styleUrl: './lista-produto.component.css'
})
export class ListaProdutoComponent {
  private authenticationService = inject(AuthenticationService);

  ngOnInit() {
    this.authenticationService.login('teste@teste.com', '@Aa12345') //Substituir com as credenciais do form
      .subscribe({
        next: (response) => { }
      });
  }
}
