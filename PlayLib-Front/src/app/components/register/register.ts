import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
  constructor(private router: Router) {
    console.log("Login component initialized");
  }

  toHome(): void {
    this.router.navigate(['/home']);
  }

  toLogin(): void {
    this.router.navigate(['/login']);
  }
}
