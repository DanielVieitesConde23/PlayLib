import { Routes } from '@angular/router';
import { profile } from 'console';


export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },


  { 
    path: 'login', 
    loadComponent: () => import('./components/login/login').then(m => m.Login),
  },

  {
    path: 'register',
    loadComponent: () => import('./components/register/register').then(m => m.Register),
  },
  { 
    path: '',
    loadComponent: () => import('./components/nav-bar/nav-bar').then(m => m.NavBar),
    children: [
      { path: 'home', loadComponent: () => import('./components/home/home').then(m => m.Home) },
      { path: 'videogame/:id', loadComponent: () => import('./components/videogame-component/videogame-component').then(m => m.VideogameComponent) },
      { path: 'tabletop-game/:id', loadComponent: () => import('./components/tabletop-game-component/tabletop-game-component').then(m => m.TabletopGameComponent) },
      { path: 'videogame-library', loadComponent: () => import('./components/videogame-library/videogame-library').then(m => m.VideogameLibrary) },
      { path: 'tabletop-game-library', loadComponent: () => import('./components/tabletop-game-library/tabletop-game-library').then(m => m.TabletopGameLibrary) },
      { path: 'request-list', loadComponent: () => import('./components/request-list/request-list').then(m => m.RequestList) },
      { path: 'profile', loadComponent: () => import('./components/profile/profile').then(m => m.Profile)},
    ]
  }
];
