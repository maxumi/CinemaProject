import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { UpdateUserDto, User } from '../models/user.models';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private UserSubject  = new BehaviorSubject<User | null>(null);
  loggedInUser$ = this.UserSubject.asObservable();
  
  private baseUrl = environment.apiBaseUrl;

  constructor(private http: HttpClient) {}

  getCurrentUser(): Observable<User> {
    return this.http.get<User>(`${this.baseUrl}/User/current-user`, { withCredentials: true });
  }

  updateUser(userData: UpdateUserDto): Observable<User> {
    return this.http.put<User>(`${this.baseUrl}/User/${userData.id}`, userData, { withCredentials: true });
  }
}