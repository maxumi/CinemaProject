import { Component } from '@angular/core';
import { UpdateUserDto, User } from '../../models/user.models';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserService } from '../../services/user.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-profile',
  imports: [FormsModule, CommonModule,ReactiveFormsModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent {
  user!: User;
  profileForm!: FormGroup;
  isEditing = false;

  constructor(private userService: UserService, private fb: FormBuilder) {}

  ngOnInit(): void {
    this.userService.getCurrentUser().subscribe({
      next: (userData) => {
        this.user = userData;
        this.initForm();
      },
      error: (error) => {
        console.error('Error fetching data:', error);
      }
    });
  }

  private initForm(): void {
    // Creates a reactive form using formbuilder which looks cleaner when coding
    this.profileForm = this.fb.group({
      firstName: [this.user.firstName, Validators.required],
      lastName: [this.user.lastName, Validators.required],
      email: [this.user.email, [Validators.required, Validators.email]],
      role: [{ value: this.user.role, disabled: true }]
    });
  }

  enableEditing(): void {
    this.isEditing = true;
  }

  saveChanges(): void {
    if (this.profileForm.invalid) {
      return; 
    }

    const updatedUserData: UpdateUserDto = {
      id: this.user.id,
      firstName: this.profileForm.value.firstName,
      lastName: this.profileForm.value.lastName,
      email: this.profileForm.value.email,
      role: this.user.role
    };

    this.userService.updateUser(updatedUserData).subscribe({
      next: (updatedUser) => {
        this.user = updatedUser; 
        this.isEditing = false;
        console.log('User profile updated successfully.');
      },
      error: (error) => {
        console.error('Error updating user profile:', error);
      }
    });
  }

  cancelEditing(): void {
    this.isEditing = false;
    this.initForm();
  }
}