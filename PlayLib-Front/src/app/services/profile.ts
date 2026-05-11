import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../model/environment';
import { GamesCarrousel } from '../model/games-carrousel';
import { Profile } from '../model/profile';

@Injectable({
  providedIn: 'root',
})
export class ProfileService {

  private urlProfile = environment.apiURL + 'Profile';

  private urlFavourites = environment.apiURL + 'Favourites';

  private urlLibrary = environment.apiURL + 'Library';

  constructor(private http: HttpClient) { }

  getUserProfile(userId: string) {
    return this.http.get<Profile>(`${this.urlProfile}/GetUserProfile/${userId}`);
  }

  getFavouriteVideogames(userId: string) {
    return this.http.get<GamesCarrousel[]>(`${this.urlFavourites}/videogame/${userId}`);
  }

  getFavouriteTabletops(userId: string) {
    return this.http.get<GamesCarrousel[]>(`${this.urlFavourites}/tabletop/${userId}`);
  }

  getLibraryVideogames(userId: string) {
    return this.http.get<GamesCarrousel[]>(`${this.urlLibrary}/videogames/${userId}`);
  }

  getLibraryBoardgames(userId: string) {
    return this.http.get<GamesCarrousel[]>(`${this.urlLibrary}/tabletop/${userId}`);
  }
}
