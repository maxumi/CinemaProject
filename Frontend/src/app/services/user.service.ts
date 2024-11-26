import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User, CreateUserDto, UpdateUserDto } from '../models/user.models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private readonly apiBaseUrl = `${environment.apiBaseUrl}/User`;

  constructor(private http: HttpClient) {}

  getAllUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.apiBaseUrl);
  }

  getUserById(id: number): Observable<User> {
    return this.http.get<User>(`${this.apiBaseUrl}/${id}`);
  }

  createUser(createUserDto: CreateUserDto): Observable<User> {
    return this.http.post<User>(this.apiBaseUrl, createUserDto);
  }

  updateUser(id: number, updateUserDto: UpdateUserDto): Observable<void> {
    return this.http.put<void>(`${this.apiBaseUrl}/${id}`, updateUserDto);
  }

  deleteUser(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiBaseUrl}/${id}`);
  }
}
