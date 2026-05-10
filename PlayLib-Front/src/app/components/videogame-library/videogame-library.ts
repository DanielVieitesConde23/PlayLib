  import { Component } from '@angular/core';
  import { CommonModule } from '@angular/common';
  import { FormsModule } from '@angular/forms';
  import { Videogame } from '../../model/videogame';
  import { MatToolbarModule } from '@angular/material/toolbar';
  import { MatFormFieldModule } from '@angular/material/form-field';
  import { MatInputModule } from '@angular/material/input';
  import { MatSelectModule } from '@angular/material/select';
  import { MatCardModule } from '@angular/material/card';
  import { MatButtonModule } from '@angular/material/button';
  import { MatIconModule } from '@angular/material/icon';
  import { RouterModule, Router } from '@angular/router';

  @Component({
    selector: 'app-videogame-library',
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
    templateUrl: './videogame-library.html',
    styleUrl: './videogame-library.css',
  })
  export class VideogameLibrary {

    searchText = '';
    filter = 'all';
    constructor(private router: Router) { }
    videogames: Videogame[] = [];

  get filteredGames(): Videogame[] {
    return this.videogames.filter(game =>
      game.name.toLowerCase().includes(this.searchText.toLowerCase())
    );
  }
   goToGame(game: Videogame) {
      this.router.navigate(['/videogame', game.name], {
        state: { game }
      });
    }
  }