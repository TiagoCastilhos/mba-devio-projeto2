import { Component, input } from '@angular/core';

@Component({
  selector: 'app-base-button',
  imports: [],
  templateUrl: './base-button.component.html',
})
export class BaseButtonComponent {
  type = input('button');
  class = input('btn btn-primary');
  disabled = input(false);
}
