import { Component, inject, input } from '@angular/core';
import { ControlValueAccessor, FormsModule, NgControl } from '@angular/forms';

@Component({
  selector: 'app-base-input',
  imports: [FormsModule],
  templateUrl: './base-input.component.html',
})
export class BaseInputComponent implements ControlValueAccessor {
  label = input('');
  type = input('text');
  protected value: any;
  
  private ngControl = inject(NgControl, { optional: true });
  protected onChange?: (value: string) => {};
  protected onTouched?: () => {};
  protected isDisabled = false;

  constructor() {
    if (this.ngControl) this.ngControl.valueAccessor = this;
  }

  writeValue(obj: any): void {
    this.value = obj;
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  setDisabledState?(isDisabled: boolean): void {
    this.isDisabled = isDisabled;
  }
}
