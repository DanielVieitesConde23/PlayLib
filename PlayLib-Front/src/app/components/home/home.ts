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

  videogamesByTag: GamesCarrousel[] = [];

  tabletopsByTag: GamesCarrousel[] = [];

  constructor(private homeSvc: HomeService, private cdr: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.getPopularVideogames();
    this.getPopularTabletops();
    this.getVideogamesByTag();
    this.getTabletopsByTag();
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

  getVideogamesByTag(): void {
    this.homeSvc.getVideogamesByTag().subscribe((response) => {
      this.videogamesByTag = [...response];
      this.cdr.markForCheck();
    });
  }

  getTabletopsByTag(): void {
    this.homeSvc.getTabletopsByTag().subscribe((response) => {
      console.log('Tabletops by tag response:', response);
      this.tabletopsByTag = [...response];
      this.cdr.markForCheck();
    });
  }
}
