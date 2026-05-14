import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../model/environment';
import { Tag } from '../model/tag';
import { Language } from '../model/language';

@Injectable({
  providedIn: 'root',
})
export class TagLanguageService {
  private urlTag = environment.apiURL + 'Tag';
  private urlLanguage = environment.apiURL + 'Language';

  constructor(private http: HttpClient) { }

  getAllTags() {
    return this.http.get<Tag[]>(`${this.urlTag}/GetAll`);
  }

  getAllLanguages() {
    return this.http.get<Language[]>(`${this.urlLanguage}/GetAll`);
  }
}
