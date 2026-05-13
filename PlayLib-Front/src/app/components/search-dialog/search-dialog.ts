import { ChangeDetectorRef, Component, ViewEncapsulation } from '@angular/core';
import { MatDialogRef, MatDialogModule } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Router } from '@angular/router';
import { HomeService } from '../../services/home';
import { GamesCarrousel } from '../../model/games-carrousel';
import { GameSearchResult } from '../../model/game-search-result';

@Component({
  selector: 'app-search-dialog',
  standalone: true,
  imports: [MatDialogModule, FormsModule, MatFormFieldModule, MatInputModule, MatCardModule, MatButtonModule, MatIconModule],
  templateUrl: './search-dialog.html',
  styleUrl: './search-dialog.css',
  encapsulation: ViewEncapsulation.None
})
export class SearchDialog {
  searchText = '';
  games: GameSearchResult[] = [];

  constructor(
    private dialogRef: MatDialogRef<SearchDialog>,
    private homeSvc: HomeService,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) { }

  onSearch() {
    if (!this.searchText.trim()) return;
    this.homeSvc.searchVideogamesByName(this.searchText.trim()).subscribe({
      next: (results) => {
        this.games = results;
        console.log(this.games);
        this.cdr.detectChanges();
      },
      error: () => {
        this.games = [];
        this.cdr.detectChanges();
      }
    });
  }

  goToGame(game: GameSearchResult) {
    this.dialogRef.close();
    if (game.type == "Videogame") {
      this.router.navigate(['user/videogame', game.name], {
        state: { id: game.id }
      });
    } 
    else 
    {
      this.router.navigate(['user/tabletop-game', game.name], {
        state: { id: game.id }
      });
    }

  }
}
