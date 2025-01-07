import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { UpdateUserDto, User, UserItem } from '../models/user.models';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private UserSubject  = new BehaviorSubject<User | null>(null);
  loggedInUser$ = this.UserSubject.asObservable();
  
  private baseUrl = environment.apiBaseUrl;

  constructor(private http: HttpClient) {}


  getSelectedUsers(query: string) {
    return this.http.get<UserItem[]>(`${this.baseUrl}/User/search`, {
      params: { query },
      withCredentials: true,
    });
  }

  getCurrentUser(){
    return this.http.get<User>(`${this.baseUrl}/User/current-user`, { withCredentials: true });
  }

  updateUser(userData: UpdateUserDto) {
    return this.http.put<User>(`${this.baseUrl}/User/${userData.id}`, userData, { withCredentials: true });
  }
}