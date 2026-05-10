import { Component, Input } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { RouterModule } from '@angular/router';
import { Router } from '@angular/router';
import { GamesCarrousel } from '../../model/games-carrousel';

@Component({
  selector: 'app-scrollable-list-vg',
  imports: [MatCardModule, MatIconModule, MatButtonModule, RouterModule],
  templateUrl: './scrollable-list-vg.html',
  styleUrl: './scrollable-list-vg.css',
})
export class ScrollableListVg {
  private _listVideogames: GamesCarrousel[] = [];
  @Input() title: string = '';
  @Input() 
  set listVideogames(value: GamesCarrousel[]) {
    this._listVideogames = value;
  }
  get listVideogames() {
    return this._listVideogames;
  }

  constructor(private router: Router) {}

  scroll(container: HTMLElement, direction: number) {
    container.scrollBy({
      left: direction * 300,
      behavior: 'smooth'
    });
  }

  goToGame(game: GamesCarrousel) {
    this.router.navigate(['user/videogame', game.name], {
      state: { id: game.id }
    });
  }
}
