import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { userLogin } from '../../model/userLogin';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
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

  constructor(private router: Router, private authService: AuthService, private snackbar: MatSnackBar) {
    console.log("Login component initialized");
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
        this.router.navigate(['/home']);
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
}
