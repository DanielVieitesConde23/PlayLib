import { Component, inject } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { userLogin } from '../../model/userLogin';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { DialogOneField } from '../dialog-one-field/dialog-one-field';
@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {

  userLogin: userLogin = {
    loginInfo: "",
    password: ""
  };

  horizontalPosition: MatSnackBarHorizontalPosition = 'center';
  verticalPosition: MatSnackBarVerticalPosition = 'top';
  readonly dialog = inject(MatDialog);

  constructor(private router: Router, private authService: AuthService, private snackbar: MatSnackBar) {
  }

  toHome(): void {
    this.authService.login(this.userLogin).subscribe({
      next: (res) => {
        if (!res) {
          this.snackbar.open("Error al iniciar sesión. Por favor, inténtalo de nuevo.", "Cerrar", {
            horizontalPosition: this.horizontalPosition,
            verticalPosition: this.verticalPosition
          });
          return;
        }
        if (res.success) {
          localStorage.setItem('token', res.accessToken);
          localStorage.setItem('userId', res.userId);
          this.router.navigate(['user/home']);
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
        } else {
          this.snackbar.open('Error de conexión con el servidor.', "Cerrar", {
            horizontalPosition: this.horizontalPosition,
            verticalPosition: this.verticalPosition
          });
        }
      }
    })
  }

  toRegister(): void {
    this.router.navigate(['/register']);
  }

  openDialog() {
    const dialogRef = this.dialog.open(DialogOneField, {
    width: '30vw',
    maxWidth: '70vw',
    data: {
      title: "Recuperar contraseña",
      label: "Correo electrónico",
      placeholder: "Introduce tu correo electrónico",
      type: "email"
    }
  });

  dialogRef.afterClosed().subscribe(result => {
    if (result != "" && result != null) {
      this.authService.resetPassword(result).subscribe({
        next: (res) => {
          if (res.success) {
            this.snackbar.open(res.message, "Cerrar", {
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
          } else {
            this.snackbar.open('Error de conexión con el servidor.', "Cerrar", {
              horizontalPosition: this.horizontalPosition,
              verticalPosition: this.verticalPosition
            });
          }
        }
      });
    }
  });
  }

}
