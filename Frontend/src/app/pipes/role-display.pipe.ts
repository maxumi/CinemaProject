import { Pipe, PipeTransform } from '@angular/core';
import { Role } from '../models/user.models';

@Pipe({
  name: 'roleDisplay'
})
export class RoleDisplayPipe implements PipeTransform {
  transform(value: Role): string {
    switch (value) {
      case Role.Customer:
        return 'Customer';
      case Role.Administrator:
        return 'Administrator';
      default:
        return 'Unknown Role';
    }
  }
}
