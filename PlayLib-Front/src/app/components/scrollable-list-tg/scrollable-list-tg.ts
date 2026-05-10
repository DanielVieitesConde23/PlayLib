import { ChangeDetectionStrategy, ChangeDetectorRef, Component, Input } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { RouterModule } from '@angular/router';
import { Router } from '@angular/router';
import { GamesCarrousel } from '../../model/games-carrousel';

@Component({
  selector: 'app-scrollable-list-tg',
  imports: [MatCardModule, MatIconModule, MatButtonModule, RouterModule],
  templateUrl: './scrollable-list-tg.html',
  styleUrl: './scrollable-list-tg.css',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ScrollableListTg {
  private _listTabletops: GamesCarrousel[] = [];
  @Input() title: string = '';
  @Input()
  set listTabletops(value: GamesCarrousel[]) {
    this._listTabletops = value;
    this.cdr.markForCheck();
  }
  get listTabletops() {
    return this._listTabletops;
  }

  constructor(private router: Router, private cdr: ChangeDetectorRef) { }

  scroll(container: HTMLElement, direction: number) {
    container.scrollBy({
      left: direction * 300,
      behavior: 'smooth'
    });
  }

  goToGame(game: GamesCarrousel) {
    this.router.navigate(['user/tabletop-game', game.name], {
      state: { id: game.id }
    });
  }
}
