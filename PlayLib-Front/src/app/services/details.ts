import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../model/environment';

@Injectable({
  providedIn: 'root',
})
export class Details {

  private urlTabletop = environment.apiURL + 'Tabletop';

  private urlVideogame = environment.apiURL + 'Videogame';

  private urlLibrary = environment.apiURL + 'Library';

  private urlFavourites = environment.apiURL + 'Favourites';

  constructor(private http: HttpClient) { }

  getTabletopGameDetails(id: string) {
    return this.http.get(`${this.urlTabletop}/${id}?userId=${localStorage.getItem('userId')}`);
  }

  getVideogameDetails(id: string) {
    return this.http.get(`${this.urlVideogame}/${id}?userId=${localStorage.getItem('userId')}`);
  }

  addVideogameToLibrary(videogameId: string) {
    const userId = localStorage.getItem('userId');
    return this.http.post(`${this.urlLibrary}/add/videogame`, { VideogameId: videogameId, UserId: userId });
  }


  removeVideogameFromLibrary(videogameId: string) {
    const userId = localStorage.getItem('userId');
    return this.http.delete(`${this.urlLibrary}/delete/videogame`, { body: { VideogameId: videogameId, UserId: userId } });
  }

  addTabletopToLibrary(boardgameId: string) {
    const userId = localStorage.getItem('userId');
    return this.http.post(`${this.urlLibrary}/add/tabletop`, { BoardgameId: boardgameId, UserId: userId });
  }

  removeTabletopFromLibrary(boardgameId: string) {
    const userId = localStorage.getItem('userId');
    return this.http.delete(`${this.urlLibrary}/delete/tabletop`, { body: { BoardgameId: boardgameId, UserId: userId } });
  }

  addVideogameToFavourites(videogameId: string) {
    const userId = localStorage.getItem('userId');
    return this.http.post(`${this.urlFavourites}/videogame/${userId}/${videogameId}`, {});
  }

  removeVideogameFromFavourites(videogameId: string) {
    const userId = localStorage.getItem('userId');
    return this.http.delete(`${this.urlFavourites}/videogame/${userId}/${videogameId}`);
  }

  addTabletopToFavourites(tabletopId: string) {
    const userId = localStorage.getItem('userId');
    return this.http.post(`${this.urlFavourites}/tabletop/${userId}/${tabletopId}`, {});
  }

  removeTabletopFromFavourites(tabletopId: string) {
    const userId = localStorage.getItem('userId');
    return this.http.delete(`${this.urlFavourites}/tabletop/${userId}/${tabletopId}`);
  }

  createVideogame(videogameDTO: any) {
    return this.http.post(`${this.urlVideogame}/Create`, videogameDTO);
  }

  createTabletopGame(tabletopDTO: any) {
    return this.http.post(`${this.urlTabletop}/Create`, tabletopDTO);
  }

  updateVideogameState(videogameId: string, newState: string) {
    const userId = localStorage.getItem('userId');
    return this.http.put(`${this.urlVideogame}/UpdateState?videogameId=${videogameId}&userId=${userId}&newState=${newState}`, {});
  }

  updateVideogameFormat(videogameId: string, newFormat: string) {
    const userId = localStorage.getItem('userId');
    return this.http.put(`${this.urlVideogame}/UpdateFormat?videogameId=${videogameId}&userId=${userId}&newFormat=${newFormat}`, {});
  }

  updateTabletopPlayedGames(tabletopId: string, playedGames: number) {
    const userId = localStorage.getItem('userId');
    return this.http.put(`${this.urlTabletop}/UpdateTabletopPlayedGames?tabletopId=${tabletopId}&userId=${userId}&playedGames=${playedGames}`, {});
  }
}
