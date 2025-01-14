import { Component, inject, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  email: string = '';
  password: string = '';
  errorMessage: string = '';
  successMessage: string = '';
  countdown: number = 3;
  isAlreadyLoggedIn: boolean = false;

  private authService = inject(AuthService)
  private router = inject(Router)

  ngOnInit() {
    this.checkLoginStatus();
  }

  checkLoginStatus() {
    this.authService.checkLoginStatus().subscribe({
      next: (response: any) => {
        if (response.isValid) {
          this.successMessage = 'You are already logged in!';
          this.isAlreadyLoggedIn = true;
  
          // Start the countdown and redirect after 3 seconds
          const interval = setInterval(() => {
            if (this.countdown > 0) {
              this.countdown--;
            } else {
              clearInterval(interval);
              this.router.navigate(['/']);
            }
          }, 1000);
        } else {
          this.isAlreadyLoggedIn = false;
        }
      },
      error: (error: any) => {
        console.log('Error checking login status:', error);
        this.isAlreadyLoggedIn = false;
      }
    });
  }
  

  login() {
    this.authService.login(this.email, this.password).subscribe({
      next: (response: any) => {
        this.successMessage = response.message || 'Login successful!';
        this.errorMessage = '';
        console.log('JWT token stored in cookies.');
        this.router.navigate(['/']); // Redirect after login
      },
      error: (error: any) => {
        this.successMessage = '';
        this.errorMessage = error.error?.message || 'Login failed. Please try again.';
      }
    });
  }
}
