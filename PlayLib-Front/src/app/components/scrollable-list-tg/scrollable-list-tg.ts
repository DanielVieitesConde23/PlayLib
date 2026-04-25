import { Component, Input } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { Tabletop } from '../../model/tabletop';
import { RouterModule } from '@angular/router';
import { Router } from '@angular/router';

@Component({
  selector: 'app-scrollable-list-tg',
  imports: [MatCardModule, MatIconModule, MatButtonModule, RouterModule],
  templateUrl: './scrollable-list-tg.html',
  styleUrl: './scrollable-list-tg.css',
})
export class ScrollableListTg {
  @Input() title: string = '';
  @Input() tabletopGames: Tabletop[] = [];

  constructor(private router: Router) {}

  scroll(container: HTMLElement, direction: number) {
    container.scrollBy({
      left: direction * 300,
      behavior: 'smooth'
    });
  }

  goToGame(game: Tabletop) {
      this.router.navigate(['/tabletop-game', game.id], {
        state: { game }  
      });
    }
}
