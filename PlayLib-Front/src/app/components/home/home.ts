import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ScrollableListVg } from "../scrollable-list-vg/scrollable-list-vg";
import { ScrollableListTg } from "../scrollable-list-tg/scrollable-list-tg";
import { GamesCarrousel } from '../../model/games-carrousel';
import { HomeService } from '../../services/home';

@Component({
  selector: 'app-home',
  imports: [
    ScrollableListVg,
    ScrollableListTg
  ],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home implements OnInit {

  popularVideogames: GamesCarrousel[] = [];

  popularTabletops: GamesCarrousel[] = [];

  constructor(private homeSvc: HomeService, private cdr: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.getPopularVideogames();
    this.getPopularTabletops();
  }

  getPopularVideogames(): void {
    this.homeSvc.getPopularVideogames().subscribe((response) => {
      this.popularVideogames = [...response];
      this.cdr.markForCheck();
    });
  }

  getPopularTabletops(): void {
    this.homeSvc.getPopularTabletopGames().subscribe((response) => {
      this.popularTabletops = [...response];
      this.cdr.markForCheck();
    });
  }
}
