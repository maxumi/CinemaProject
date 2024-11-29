import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Role } from '../models/user.models';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private baseUrl = environment.apiBaseUrl;

  constructor(private http: HttpClient) {}

  // Method to check login status (verify token)
  checkLoginStatus(): Observable<any> {
    return this.http.get(`${this.baseUrl}/auth/verify-token`, {
      withCredentials: true,
    });
  }

  // Method to log in
  login(email: string, password: string): Observable<any> {
    const loginPayload = { email, password };
    return this.http.post(`${this.baseUrl}/auth/login`, loginPayload, {
      withCredentials: true,
    });
  }

  register(user: {
    firstName: string;
    lastName: string;
    email: string;
    password: string;
    role: Role;
  }): Observable<any> {
    return this.http.post(`${this.baseUrl}/auth/register`, user);
  }

  // Method to log out the user
  logout(): Observable<any> {
    return this.http.post(`${this.baseUrl}/auth/logout`, {}, {
      withCredentials: true,
    });
  }
}
