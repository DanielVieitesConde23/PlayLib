import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ScrollableListVg } from "../scrollable-list-vg/scrollable-list-vg";
import { ScrollableListTg } from "../scrollable-list-tg/scrollable-list-tg";
import { GamesCarrousel } from '../../model/games-carrousel';
import { HomeService } from '../../services/home';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { SearchDialog } from '../search-dialog/search-dialog';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    ScrollableListVg,
    ScrollableListTg,
    MatDialogModule,
    MatIconModule,
    MatButtonModule
  ],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home implements OnInit {

  popularVideogames: GamesCarrousel[] = [];
  popularTabletops: GamesCarrousel[] = [];
  videogamesByTag: GamesCarrousel[] = [];
  tabletopsByTag: GamesCarrousel[] = [];

  constructor(private homeSvc: HomeService, private cdr: ChangeDetectorRef, private dialog: MatDialog) { }

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
      this.tabletopsByTag = [...response];
      this.cdr.markForCheck();
    });
  }

  openSearch(): void {
    this.dialog.open(SearchDialog, {
      width: '850px',
      maxWidth: '95vw'
    });
  }
}
