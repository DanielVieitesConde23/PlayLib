import { Injectable } from '@angular/core';
import { environment } from '../model/environment';
import { HttpClient } from '@angular/common/http';
import { GamesCarrousel } from '../model/games-carrousel';
import { GameSearchResult } from '../model/game-search-result';

@Injectable({
  providedIn: 'root',
})
export class HomeService {

  private urlTabletop = environment.apiURL + 'Tabletop';

  private urlVideogame = environment.apiURL + 'Videogame';

  constructor(private http: HttpClient) { }

  getPopularTabletopGames() {
    return this.http.get<GamesCarrousel[]>(`${this.urlTabletop}/getpopulartabletops?userId=${localStorage.getItem('userId')}`);
  }

  getPopularVideogames() {
    return this.http.get<GamesCarrousel[]>(`${this.urlVideogame}/getpopulargames?userId=${localStorage.getItem('userId')}`);
  }

  getVideogamesByTag() {
    return this.http.get<GamesCarrousel[]>(`${this.urlVideogame}/GetVideogameByTag?userId=${localStorage.getItem('userId')}`);
  }

  getTabletopsByTag() {
    return this.http.get<GamesCarrousel[]>(`${this.urlTabletop}/gettabletopbytag?userId=${localStorage.getItem('userId')}`);
  }

  searchVideogamesByName(name: string) {
    return this.http.get<GameSearchResult[]>(`${this.urlVideogame}/GetGamesbySearch/${name}`);
  }
}
