import { Component, Input } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { Videogame } from '../../model/videogame';
import { RouterModule } from '@angular/router';
import { Router } from '@angular/router';
@Component({
  selector: 'app-scrollable-list-vg',
  imports: [MatCardModule, MatIconModule, MatButtonModule, RouterModule],
  templateUrl: './scrollable-list-vg.html',
  styleUrl: './scrollable-list-vg.css',
})
export class ScrollableListVg {
  @Input() title: string = '';
  @Input() videogames: Videogame[] = [];

  constructor(private router: Router) {}

  scroll(container: HTMLElement, direction: number) {
    container.scrollBy({
      left: direction * 300,
      behavior: 'smooth'
    });
  }

  goToGame(game: Videogame) {
    this.router.navigate(['/videogame', game.name], {
      state: { game }  
    });
  }
}
