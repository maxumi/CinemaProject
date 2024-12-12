import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { TokenServiceService } from '../../services/token-service.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Observable } from 'rxjs';
import { Role } from '../../models/user.models';

@Component({
  selector: 'app-navbar',
  imports: [CommonModule, RouterModule],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  isAuthenticated$!: Observable<boolean>;
  userRole$!: Observable<Role | null>;
  role = Role
  constructor(private authService: AuthService) {}

  ngOnInit() {
    this.isAuthenticated$ = this.authService.isLoggedIn$;
    this.userRole$ = this.authService.userRole$;
    this.verifyAuthStatus();
  }

  verifyAuthStatus() {
    this.authService.checkLoginStatus().subscribe({
      next: () => {},
      error: (error) => {
        console.error('Error verifying auth status:', error);
      },
    });
  }

  logout() {
    this.authService.logout().subscribe({
      next: () => {},
      error: (error) => {
        console.error('Error during logout:', error);
      },
    });
  }
}
