import { Component } from '@angular/core';
import { ScrollableListVg } from "../scrollable-list-vg/scrollable-list-vg";
import { ScrollableListTg } from "../scrollable-list-tg/scrollable-list-tg";
import { GamesCarrousel } from '../../model/games-carrousel';

@Component({
  selector: 'app-profile',
  imports: [ScrollableListVg, ScrollableListTg],
  templateUrl: './profile.html',
  styleUrl: './profile.css',
})
export class Profile {
  profile = {
    id: '1',
    name: 'JuanYManoli',
    image_route: 'https://imgur.com/LHs55ZV.png',
    total_videogames: 10,
    total_tabletop_games: 15,
  }

  videogames: GamesCarrousel[] = [];

  tabletops: GamesCarrousel[] = [];
}
