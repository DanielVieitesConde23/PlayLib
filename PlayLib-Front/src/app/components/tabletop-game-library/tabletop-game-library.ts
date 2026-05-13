import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
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
import { ProfileService } from '../../services/profile';
import { GamesCarrousel } from '../../model/games-carrousel';
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
export class TabletopGameLibrary implements OnInit {
  searchText = '';
  filter = 'all';
  constructor(private router: Router, private profileSvc: ProfileService, private cdr: ChangeDetectorRef) { }
  tabletops: GamesCarrousel[] = [];

  ngOnInit(): void {
    const userId = localStorage.getItem('userId');
    if (userId) {
      this.profileSvc.getLibraryBoardgames(userId).subscribe((response: any[]) => {
        this.tabletops = response.map((item: any) => ({
          id: item.id,
          name: item.name,
          image: item.image,
        }));
        this.cdr.detectChanges();
      });
    }
  }

  get filteredGames(): GamesCarrousel[] {
    return this.tabletops.filter(game =>
      game.name.toLowerCase().includes(this.searchText.toLowerCase())
    );
  }

  goToGame(game: GamesCarrousel) {
    this.router.navigate(['user/tabletop-game', game.name], {
      state: { id: game.id }
    });
  }
}

