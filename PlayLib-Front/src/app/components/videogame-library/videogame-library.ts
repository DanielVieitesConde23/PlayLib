import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
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
import { ProfileService } from '../../services/profile';
import { GamesCarrousel } from '../../model/games-carrousel';

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
export class VideogameLibrary implements OnInit {

  searchText = '';
  filter = 'all';
  constructor(private router: Router, private profileSvc: ProfileService, private cdr: ChangeDetectorRef) { }
  videogames: GamesCarrousel[] = [];

  ngOnInit(): void {
    const userId = localStorage.getItem('userId');
    if (userId) {
      this.profileSvc.getLibraryVideogames(userId).subscribe((response: any[]) => {
        this.videogames = response.map((item: any) => ({
          id: item.id,
          name: item.name,
          image: item.image
        }));
        this.cdr.detectChanges();
      });
    }
  }

  get filteredGames(): GamesCarrousel[] {
    return this.videogames.filter(game =>
      game.name.toLowerCase().includes(this.searchText.toLowerCase())
    );
  }

  goToGame(game: GamesCarrousel) {
    this.router.navigate(['user/videogame', game.name], {
      state: { id: game.id }
    });
  }
}