import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-step-indicator',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './step-indicator.component.html',
  styleUrl: './step-indicator.component.css',
})
export class StepIndicatorComponent {
  @Input() currentStep = 1;
  @Input() form?: FormGroup;
  @Output() currentStepChange = new EventEmitter<number>();

  nextStep() {
    if (this.isStepValid(1)) {
      this.currentStep = 2;
      this.currentStepChange.emit(this.currentStep);
    } else {
      this.markStepTouched(1);
    }
  }

  prevStep() {
    this.currentStep = 1;
    this.currentStepChange.emit(this.currentStep);
  }

  isStepValid(step: number): boolean {
    if (!this.form) return false;
    if (step === 1) {
      return (
        (this.form.get('firstName')?.valid &&
          this.form.get('lastName')?.valid &&
          this.form.get('email')?.valid) ??
        false
      );
    }
    return this.form.valid;
  }

  private markStepTouched(step: number) {
    if (!this.form) return;
    if (step === 1) {
      this.form.get('firstName')?.markAsTouched();
      this.form.get('lastName')?.markAsTouched();
      this.form.get('email')?.markAsTouched();
    }
  }
}
