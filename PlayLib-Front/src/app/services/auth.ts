import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../model/environment';
import { userLogin } from '../model/userLogin';
import { userRegister } from '../model/userRegister';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private url = environment.apiURL + 'Auth'

  constructor(private http: HttpClient) { }

  register(user: userRegister): Observable<any> {
    return this.http.post(`${this.url}/register`, user)
  }

  login(user: userLogin): Observable<any> {
    return this.http.post(`${this.url}/login`, user)
  }

  isAuthenticated(): boolean {
    if (typeof window === 'undefined') {
      return false;
    }

    return !!localStorage.getItem('token');
  }

  resetPassword(email: string): Observable<any> {
    return this.http.put(`${this.url}/resetpassword?email=${email}`, {});
  }
}
