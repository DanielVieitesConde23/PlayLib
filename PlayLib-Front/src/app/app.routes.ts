import { Routes } from '@angular/router';
import { AuthGuard } from './guards/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: 'user/home', pathMatch: 'full' },
  {
    path: 'login',
    loadComponent: () => import('./components/login/login').then(m => m.Login),
  },

  {
    path: 'register',
    loadComponent: () => import('./components/register/register').then(m => m.Register),
  },
  {
    path: 'user',
    canActivate: [AuthGuard],
    loadComponent: () =>
      import('./components/nav-bar/nav-bar').then(m => m.NavBar),
    children: [
      { path: 'home', loadComponent: () => import('./components/home/home').then(m => m.Home) },
      { path: 'videogame/:name', loadComponent: () => import('./components/videogame-component/videogame-component').then(m => m.VideogameComponent) },
      { path: 'tabletop-game/:name', loadComponent: () => import('./components/tabletop-game-component/tabletop-game-component').then(m => m.TabletopGameComponent) },
      { path: 'videogame-library', loadComponent: () => import('./components/videogame-library/videogame-library').then(m => m.VideogameLibrary) },
      { path: 'tabletop-game-library', loadComponent: () => import('./components/tabletop-game-library/tabletop-game-library').then(m => m.TabletopGameLibrary) },
      { path: 'request-list', loadComponent: () => import('./components/request-list/request-list').then(m => m.RequestList) },
      { path: 'profile', loadComponent: () => import('./components/profile/profile').then(m => m.ProfileComponent) },
      { path: 'admin-request-mail', loadComponent: () => import('./components/admin-request-mail/admin-request-mail').then(m => m.AdminRequestMail) },
    ]
  }
];