import { Injectable, inject } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, Router, CanActivateFn, UrlTree } from '@angular/router';
import { AuthService } from '../services/auth';

@Injectable({
  providedIn: 'root',
})
class Guard  {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree {
    return this.authService.isAuthenticated()
    ? true
    : this.router.createUrlTree(['/login']);
    }
}

export const AuthGuard: CanActivateFn = (next: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree => {
    return inject(Guard).canActivate(next, state);
}