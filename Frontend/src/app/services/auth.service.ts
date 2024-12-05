import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, of, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { CreateUserDto, Role } from '../models/user.models';

interface AuthResponse {
  isValid: boolean;
  message: string;
}

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private baseUrl = environment.apiBaseUrl;

  private loggedInSubject = new BehaviorSubject<boolean>(false);
  
  isLoggedIn$ = this.loggedInSubject.asObservable();

  constructor(private http: HttpClient) {}

  private setAuthState(isLoggedIn: boolean): void {
    this.loggedInSubject.next(isLoggedIn);
  }

  private handleError(error: any): Observable<never> {
    console.error('AuthService Error:', error);
    return throwError(() => new Error(error?.message || 'An error occurred'));
  }

  checkLoginStatus() {
    return this.http.get<AuthResponse>(`${this.baseUrl}/auth/verify-token`, { withCredentials: true }).pipe(
      tap((response) => this.setAuthState(response.isValid)),
      catchError((error) => {
        if (error.status === 401) {
          // Handle 401 as to not log every time
          this.setAuthState(false);
          return of({ isValid: false, message: 'Unauthorized' } as AuthResponse);
        }
        // For other errors, handle them normally
        return this.handleError(error);
      })
    );
  }

  login(email: string, password: string) {
    const loginPayload = { email, password };
    return this.http.post(`${this.baseUrl}/auth/login`, loginPayload, { withCredentials: true }).pipe(
      tap(() => this.setAuthState(true)),
      catchError((error) => this.handleError(error))
    );
  }

  logout() {
    return this.http.post(`${this.baseUrl}/auth/logout`, {}, { withCredentials: true }).pipe(
      tap(() => this.setAuthState(false)),
      catchError((error) => this.handleError(error))
    );
  }

  register(user: CreateUserDto ) {
    return this.http.post<{ isValid: boolean; message: string }>(
      `${this.baseUrl}/auth/register`,
      user
    ).pipe(
      tap((response) => {
        this.setAuthState(response.isValid);
      }),
      catchError((error) => this.handleError(error))
    );
  }
}
