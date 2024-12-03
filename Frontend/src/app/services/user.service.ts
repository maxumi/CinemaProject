import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { UpdateUserDto, User } from '../models/user.models';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private baseUrl = environment.apiBaseUrl;

  constructor(private http: HttpClient) {}

  // Fetch the current user's profile
  getCurrentUser(): Observable<User> {
    return this.http.get<User>(`${this.baseUrl}/User/current-user`, { withCredentials: true });
  }

  // Update the user's profile
  updateUser(userData: UpdateUserDto): Observable<User> {
    return this.http.put<User>(`${this.baseUrl}/User/${userData.id}`, userData, { withCredentials: true });
  }
}