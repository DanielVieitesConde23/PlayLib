import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Tabletop } from '../../model/tabletop';
import { RouterModule, Router } from '@angular/router';
@Component({
  selector: 'app-tabletop-game-library',
  imports: [
    CommonModule,
    FormsModule,
    MatToolbarModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    RouterModule
  ],
  templateUrl: './tabletop-game-library.html',
  styleUrl: './tabletop-game-library.css',
})
export class TabletopGameLibrary {
  searchText = '';
  filter = 'all';
  constructor(private router: Router) { }
  tabletops: Tabletop[] = [];

  get filteredGames(): Tabletop[] {
    return this.tabletops.filter(game =>
      game.name.toLowerCase().includes(this.searchText.toLowerCase())
    );
  }
  goToGame(game: Tabletop) {
    this.router.navigate(['/tabletop-game', game.name], {
      state: { game }
    });
  }
}
