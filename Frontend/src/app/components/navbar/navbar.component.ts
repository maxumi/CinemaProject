import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-navbar',
  imports: [CommonModule, RouterModule],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent {
  isAuthenticated$!: Observable<boolean>;

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.isAuthenticated$ = this.authService.isLoggedIn$;
    this.verifyAuthStatus()
  }

  verifyAuthStatus(): void {
    this.authService.checkLoginStatus().subscribe({
      next: (response) => {
        console.log('Verification successful:', response.message);
      },
      error: (error) => {
        console.error('Verification failed:', error);
      },
    });
  }

  logout(): void {
    this.authService.logout().subscribe({
      next: () => {
        console.log('Successfully logged out');
      },
      error: (error) => {
        console.error('Error during logout:', error);
      },
    });
  }
}