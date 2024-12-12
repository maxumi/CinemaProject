import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { Role } from '../models/user.models';

export const roleGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const requiredRoles: Role[] = route.data["roles"];
  const userRole = authService.getRole();

  if (userRole !== null && requiredRoles?.includes(userRole)) {
    return true;
  }

  console.error('Access denied- Role not sufficed');
  return false;
};
