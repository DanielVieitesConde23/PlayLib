import { Component, Inject, ViewEncapsulation } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';

@Component({
  selector: 'app-edit-dialog',
  standalone: true,
  imports: [MatDialogModule, MatButtonModule, MatInputModule, MatFormFieldModule, FormsModule, CommonModule],
  templateUrl: './edit-dialog.html',
  styleUrl: './edit-dialog.css',
  encapsulation: ViewEncapsulation.None
})
export class EditDialog {
  input1: string = '';
  input2: string = '';
  input3: string = '';

  horizontalPosition: MatSnackBarHorizontalPosition = 'center';
  verticalPosition: MatSnackBarVerticalPosition = 'top';

  constructor(
    public dialogRef: MatDialogRef<EditDialog>,
    @Inject(MAT_DIALOG_DATA) public data: { mode: 'username' | 'password' | 'image' },
    private snackbar: MatSnackBar
  ) {}

  onSave(): void {
    if (this.data.mode === 'username') {
      if (!this.input1.trim()) return;
      this.dialogRef.close(this.input1);
    } else if (this.data.mode === 'password') {
      if (!this.input1 || !this.input2 || !this.input3) return;
      if (this.input2 !== this.input3) {
        this.snackbar.open("Las contraseñas nuevas no coinciden.", "Cerrar", {
          horizontalPosition: this.horizontalPosition,
          verticalPosition: this.verticalPosition
        });
        return;
      }
      if (this.input1.length < 8) {
        this.snackbar.open("La contraseña debe tener al menos 8 caracteres.", "Cerrar", {
          horizontalPosition: this.horizontalPosition,
          verticalPosition: this.verticalPosition
        });
        return;
      }
      this.dialogRef.close({ currentPassword: this.input1, newPassword: this.input2, repeatNewPassword: this.input3 });
    } else if (this.data.mode === 'image') {
      if (!this.input1.trim()) return;
      const lower = this.input1.toLowerCase();
      if (!lower.endsWith('.png') && !lower.endsWith('.jpg') && !lower.endsWith('.jpeg')) {
        this.snackbar.open("El enlace debe terminar en .png, .jpg o .jpeg", "Cerrar", {
                    horizontalPosition: this.horizontalPosition,
                    verticalPosition: this.verticalPosition
                  });
        return;
      }
      this.dialogRef.close(this.input1);
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
