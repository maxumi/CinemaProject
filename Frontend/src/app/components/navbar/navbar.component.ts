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

  ngOnInit() {
    this.isAuthenticated$ = this.authService.isLoggedIn$;
    this.verifyAuthStatus()
  }

  verifyAuthStatus() {
    this.authService.checkLoginStatus().subscribe({
      next: (response) => {
      },
      error: (error) => {
      },
    });
  }

  logout() {
    this.authService.logout().subscribe({
      next: () => {
      },
      error: (error) => {
        console.error('Error during logout:', error);
      },
    });
  }
}