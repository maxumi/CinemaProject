import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, of, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { CreateUserDto, Role } from '../models/user.models';
import { TokenServiceService } from './token-service.service';

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
  private roleSubject = new BehaviorSubject<Role | null>(null);

  isLoggedIn$ = this.loggedInSubject.asObservable();
  userRole$ = this.roleSubject.asObservable();

  constructor(private http: HttpClient, private tokenService: TokenServiceService) {}

  private setAuthState(isLoggedIn: boolean): void {
    this.loggedInSubject.next(isLoggedIn);
    if (isLoggedIn) {
      this.roleSubject.next(this.getRole());
    } else {
      this.roleSubject.next(null);
    }
  }

  getRole(): Role | null {
    return this.tokenService.getTokenRole();
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
          this.setAuthState(false);
          return of({ isValid: false, message: 'Unauthorized' } as AuthResponse);
        }
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

  register(user: CreateUserDto) {
    return this.http.post<{ isValid: boolean; message: string }>(
      `${this.baseUrl}/auth/register`,
      user
    ).pipe(
      tap((response) => this.setAuthState(response.isValid)),
      catchError((error) => this.handleError(error))
    );
  }
}
