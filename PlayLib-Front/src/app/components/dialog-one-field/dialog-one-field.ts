import { Component, inject, ViewEncapsulation } from '@angular/core';
import {MatDialogTitle, MatDialogContent, MatDialogActions, MatDialogClose, MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import { MatInput } from "@angular/material/input";
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-dialog-one-field',
  imports: [MatDialogTitle, MatDialogContent, MatDialogActions, MatDialogClose, MatInput, FormsModule],
  templateUrl: './dialog-one-field.html',
  styleUrl: './dialog-one-field.css',
  encapsulation: ViewEncapsulation.None
})
export class DialogOneField {

  data = inject(MAT_DIALOG_DATA);
  dialogRef = inject(MatDialogRef<DialogOneField>);

  value: string = '';
  error: string = '';

  private emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

  close() {
  if (!this.emailRegex.test(this.value)) {
    this.error = 'Introduce un email válido';
    return;
  }
  
  this.dialogRef.close(this.value);
}
}
