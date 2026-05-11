import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ScrollableListVg } from "../scrollable-list-vg/scrollable-list-vg";
import { ScrollableListTg } from "../scrollable-list-tg/scrollable-list-tg";
import { GamesCarrousel } from '../../model/games-carrousel';
import { Profile } from '../../model/profile';
import { ProfileService } from '../../services/profile';

@Component({
  selector: 'app-profile',
  imports: [ScrollableListVg, ScrollableListTg],
  templateUrl: './profile.html',
  styleUrl: './profile.css',
})
export class ProfileComponent implements OnInit {
  profile: Profile = {
    id: '',
    username: '',
    image_route: '',
    total_videogames: 0,
    total_tabletop_games: 0
  };

  videogames: GamesCarrousel[] = [];

  tabletops: GamesCarrousel[] = [];

  constructor(private profileSvc: ProfileService, private cdr: ChangeDetectorRef) { }

  ngOnInit(): void {
    const userId = localStorage.getItem('userId');
    if (userId) {
      this.loadProfile(userId);
      this.loadFavouriteVideogames(userId);
      this.loadFavouriteTabletops(userId);
    }
  }

  loadProfile(userId: string): void {
    this.profileSvc.getUserProfile(userId).subscribe((data: any) => {
      this.profile = {
        id: userId,
        username: data.userName,
        image_route: data.image_Route,
        total_videogames: data.total_Videogames,
        total_tabletop_games: data.total_Tabletop_Games
      };
      this.cdr.detectChanges();
    });
  }

  loadFavouriteVideogames(userId: string): void {
    this.profileSvc.getFavouriteVideogames(userId).subscribe((response) => {
      this.videogames = [...response];
      this.cdr.detectChanges();
    });
  }

  loadFavouriteTabletops(userId: string): void {
    this.profileSvc.getFavouriteTabletops(userId).subscribe((response) => {
      this.tabletops = [...response];
      this.cdr.detectChanges();
    });
  }
}
