import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../model/environment';

@Injectable({
  providedIn: 'root',
})
export class Details {

  private urlTabletop = environment.apiURL + 'Tabletop';

  private urlVideogame = environment.apiURL + 'Videogame';

  constructor(private http: HttpClient) { }

  getTabletopGameDetails(id: string) {
    return this.http.get(`${this.urlTabletop}/${id}?userId=${localStorage.getItem('userId')}`);
  }

  getVideogameDetails(id: string) {
    return this.http.get(`${this.urlVideogame}/${id}?userId=${localStorage.getItem('userId')}`);
  }
}
