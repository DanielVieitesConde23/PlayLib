import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../model/environment';
import { Request } from '../model/request';

@Injectable({
  providedIn: 'root',
})
export class RequestService {
  private urlRequest = environment.apiURL + 'Request';

  constructor(private http: HttpClient) { }

  getForUser(userId: string) {
    return this.http.get<Request[]>(`${this.urlRequest}/user/${userId}`);
  }

  getAll() {
    return this.http.get<Request[]>(`${this.urlRequest}/GetAll`);
  }

  create(request: Request) {
    return this.http.post<any>(`${this.urlRequest}`, request);
  }

  approve(requestId: string) {
    return this.http.post<any>(`${this.urlRequest}/approve/${requestId}`, {});
  }

  deny(requestId: string) {
    return this.http.post<any>(`${this.urlRequest}/deny/${requestId}`, {});
  }

  remove(requestId: string) {
    return this.http.delete<any>(`${this.urlRequest}/remove/${requestId}`);
  }
}
