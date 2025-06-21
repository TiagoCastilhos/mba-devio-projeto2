import { Component, input } from '@angular/core';

@Component({
  selector: 'app-base-button',
  imports: [],
  templateUrl: './base-button.component.html',
})
export class BaseButtonComponent {
  type = input('type');
  class = input('btn btn-primary');
}
