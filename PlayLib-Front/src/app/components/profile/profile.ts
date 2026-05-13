import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ScrollableListVg } from "../scrollable-list-vg/scrollable-list-vg";
import { ScrollableListTg } from "../scrollable-list-tg/scrollable-list-tg";
import { GamesCarrousel } from '../../model/games-carrousel';
import { Profile } from '../../model/profile';
import { ProfileService } from '../../services/profile';
import { AuthService } from '../../services/auth';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { ConfirmDialog } from '../confirm-dialog/confirm-dialog';
import { EditDialog } from '../edit-dialog/edit-dialog';
import { Router } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [ScrollableListVg, ScrollableListTg, MatDialogModule, MatIconModule, MatButtonModule],
  templateUrl: './profile.html',
  styleUrl: './profile.css',
})
export class ProfileComponent implements OnInit {
  profile: Profile = {
    id: '',
    username: '',
    image_route: '',
    total_videogames: 0,
    total_tabletop_games: 0
  };

  horizontalPosition: MatSnackBarHorizontalPosition = 'center';
  verticalPosition: MatSnackBarVerticalPosition = 'top';

  videogames: GamesCarrousel[] = [];
  tabletops: GamesCarrousel[] = [];

  constructor(
    private profileSvc: ProfileService,
    private authSvc: AuthService,
    private cdr: ChangeDetectorRef,
    private dialog: MatDialog,
    private router: Router,
    private snackbar: MatSnackBar
  ) { }

  ngOnInit(): void {
    const userId = localStorage.getItem('userId');
    if (userId) {
      this.loadProfile(userId);
      this.loadFavouriteVideogames(userId);
      this.loadFavouriteTabletops(userId);
    }
  }

  loadProfile(userId: string): void {
    this.profileSvc.getUserProfile(userId).subscribe((data: any) => {
      this.profile = {
        id: userId,
        username: data.userName,
        image_route: data.image_Route,
        total_videogames: data.total_Videogames,
        total_tabletop_games: data.total_Tabletop_Games
      };
      this.cdr.detectChanges();
    });
  }

  loadFavouriteVideogames(userId: string): void {
    this.profileSvc.getFavouriteVideogames(userId).subscribe((response) => {
      this.videogames = [...response];
      this.cdr.detectChanges();
    });
  }

  loadFavouriteTabletops(userId: string): void {
    this.profileSvc.getFavouriteTabletops(userId).subscribe((response) => {
      this.tabletops = [...response];
      this.cdr.detectChanges();
    });
  }

  openEditDialog(mode: 'username' | 'password' | 'image'): void {
    const dialogRef = this.dialog.open(EditDialog, {
      width: '400px',
      data: { mode: mode }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        const confirmRef = this.dialog.open(ConfirmDialog, {
          data: { title: 'Confirmar Acción', message: '¿Estás seguro de que quieres guardar estos cambios?' }
        });

        confirmRef.afterClosed().subscribe(confirm => {
          if (confirm) {
            const userId = localStorage.getItem('userId');
            if (!userId) return;

            if (mode === 'username') {
              this.profileSvc.updateUsername(userId, result).subscribe({
                next: () => {
                  this.profile.username = result;
                  this.cdr.detectChanges();
                },
                error: (err) => {
                  this.snackbar.open(err.error.message, "Cerrar", {
                    horizontalPosition: this.horizontalPosition,
                    verticalPosition: this.verticalPosition
                  });
                }
              });
            } else if (mode === 'image') {
              this.profileSvc.updateImage(userId, result).subscribe({
                next: () => {
                  this.profile.image_route = result;
                  this.cdr.detectChanges();
                },
                error: (err) => {
                  this.snackbar.open(err.error.message, "Cerrar", {
                    horizontalPosition: this.horizontalPosition,
                    verticalPosition: this.verticalPosition
                  });
                }
              });
            } else if (mode === 'password') {
              this.authSvc.updatePassword(userId, result).subscribe({
                next: () => {},
                error: (err) => {
                  this.snackbar.open(err.error.message, "Cerrar", {
                    horizontalPosition: this.horizontalPosition,
                    verticalPosition: this.verticalPosition
                  });
                }
              });
            }
          }
        });
      }
    });
  }

  deleteAccount(): void {
    const confirmRef = this.dialog.open(ConfirmDialog, {
      data: { title: 'Eliminar Cuenta', message: '¿Estás seguro de que quieres eliminar tu cuenta?' }
    });

    confirmRef.afterClosed().subscribe(confirm => {
      if (confirm) {
        const userId = localStorage.getItem('userId');
        if (userId) {
          this.profileSvc.deleteAccount(userId).subscribe({
            next: () => {
              localStorage.clear();
              this.router.navigate(['/login']);
            },
            error: (err) => console.error(err)
          });
        }
      }
    });
  }
}
