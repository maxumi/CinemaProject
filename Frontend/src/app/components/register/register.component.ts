import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Role } from '../../models/user.models';

@Component({
  selector: 'app-register',
  imports: [FormsModule, CommonModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  firstName: string = '';
  lastName: string = '';
  email: string = '';
  password: string = '';
  role: Role = Role.Customer; // Default to Customer
  errorMessage: string = '';
  successMessage: string = '';
  isLoggedIn: boolean = false; // Check if user is logged in
  alreadyLoggedInMessage: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit(): void {
    this.checkLoginStatus();
  }

  checkLoginStatus(): void {
    this.authService.checkLoginStatus().subscribe({
      next: (response: any) => {
        if (response.isValid) {
          this.isLoggedIn = true;
          this.alreadyLoggedInMessage =
            'You are already logged in! Redirecting to the home page...';
          // Redirect after 3 seconds
          setTimeout(() => {
            this.router.navigate(['/']);
          }, 3000);
        } else {
          this.isLoggedIn = false;
        }
      },
      error: () => {
        this.isLoggedIn = false;
      },
    });
  }

  register(): void {
    if (this.isLoggedIn) {
      return; // Prevent registration if already logged in
    }

    const userPayload = {
      firstName: this.firstName,
      lastName: this.lastName,
      email: this.email,
      password: this.password,
      role: this.role, // Use Role enum
    };

    this.authService.register(userPayload).subscribe({
      next: (response: any) => {
        this.successMessage = response.message || 'Registration successful!';
        this.errorMessage = '';
        setTimeout(() => {
          this.router.navigate(['/login']); // Redirect to login page
        }, 2000); // 2-second delay before redirect
      },
      error: (error: any) => {
        this.successMessage = '';
        this.errorMessage = error.error?.message || 'Registration failed.';
      },
    });
  }
}
