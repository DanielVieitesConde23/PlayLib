import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { userRegister } from '../../model/userRegister';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
  userRegister: userRegister = {
    username: "",
    email: "",
    password: "",
    repeatPassword: ""
  };
  horizontalPosition: MatSnackBarHorizontalPosition = 'center';
  verticalPosition: MatSnackBarVerticalPosition = 'top';

  constructor(private router: Router, private authService: AuthService, private snackbar: MatSnackBar) {
    console.log("Login component initialized");
  }

  toHome(): void {
    this.authService.register(this.userRegister).subscribe({
      next: (res) => {
        if (res.success) {
          this.router.navigate(['/login']);
           this.snackbar.open("Registro exitoso. Por favor, inicia sesión.", "Cerrar", {
            horizontalPosition: this.horizontalPosition,
            verticalPosition: this.verticalPosition
          });
        } else {
          this.snackbar.open(res.message, "Cerrar", {
            horizontalPosition: this.horizontalPosition,
            verticalPosition: this.verticalPosition
          });
        }
      },
      error: (err) => {
        if (err.status === 400 && err.error?.errors) {
          const messages = Object.values(err.error.errors).flat();
          this.snackbar.open(messages.join('\n'), "Cerrar", {
            horizontalPosition: this.horizontalPosition,
            verticalPosition: this.verticalPosition
          });
        } else if (err.status === 409) {
          this.snackbar.open('El nombre de usuario o correo electrónico ya están en uso.', "Cerrar", {
            horizontalPosition: this.horizontalPosition,
            verticalPosition: this.verticalPosition
          });
        } else {
          this.snackbar.open('Error de conexión con el servidor.', "Cerrar", {
            horizontalPosition: this.horizontalPosition,
            verticalPosition: this.verticalPosition
          });
        }
      }
    });
  }

  returnToLogin(): void {
    this.router.navigate(['/login']);
  }
}
