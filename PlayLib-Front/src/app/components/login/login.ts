import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  constructor(private router: Router) {
    console.log("Login component initialized");
  }

  toHome(): void {
    this.router.navigate(['/home']);
  }

  toRegister(): void {
    this.router.navigate(['/register']);
  }
}
