import { Component, OnInit } from '@angular/core';
import { User, CreateUserDto, UpdateUserDto, Role } from '../../models/user.models';
import { UserService } from '../../services/user.service';
import { CommonModule } from '@angular/common';
import { RoleDisplayPipe } from '../../pipes/role-display.pipe';
import { FormsModule, NgModel } from '@angular/forms';

@Component({
  selector: 'app-user-list',
  imports: [CommonModule, RoleDisplayPipe,FormsModule],
  standalone: true,
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css'],
})
export class UserListComponent implements OnInit {
  users: User[] = [];
  newUser: CreateUserDto = { firstName: '', lastName: '', email: '', password: '', role: Role.Customer };
  editUser?: UpdateUserDto;

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    this.userService.getAllUsers().subscribe({
      next: (data) => (this.users = data),
      error: (err) => console.error('Failed to load users:', err),
    });
  }

  addUser(): void {
    // Needed as bug will make it a string for some reason after first time
    this.newUser.role = +this.newUser.role;
    this.userService.createUser(this.newUser).subscribe({
      next: () => {
        this.loadUsers();
        this.newUser = { firstName: '', lastName: '', email: '', password: '', role: Role.Customer };
      },
      error: (err) => console.error('Failed to add user:', err),
    });
  }

  startEditUser(user: User): void {
    this.editUser = { ...user };
  }

  saveEditUser(): void {
    if (this.editUser) {
      this.userService.updateUser(this.editUser.id, this.editUser).subscribe({
        next: () => {
          this.loadUsers();
          this.editUser = undefined;
        },
        error: (err) => console.error('Failed to edit user:', err),
      });
    }
  }

  deleteUser(id: number): void {
    this.userService.deleteUser(id).subscribe({
      next: () => this.loadUsers(),
      error: (err) => console.error('Failed to delete user:', err),
    });
  }
}
