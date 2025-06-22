import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function passwordMatchValidator(
    sourceKey = 'password',
    confirmKey = 'confirmPassword'
): ValidatorFn {
    return (group: AbstractControl): ValidationErrors | null => {
        const source = group.get(sourceKey);
        const confirm = group.get(confirmKey);

        if (!source || !confirm) {
            return null;
        }

        const mismatch = source.value !== confirm.value;
        confirm.setErrors(mismatch ? { passwordMismatch: true } : null);

        return mismatch ? { passwordMismatch: true } : null;
    };
};

export const passwordRegex: RegExp = /^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_])(?=.{6,})(?:(.)(?!.*\1)){6,}$/;