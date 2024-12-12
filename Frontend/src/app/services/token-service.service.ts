import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { jwtDecode } from 'jwt-decode';
import { Role } from '../models/user.models';

@Injectable({
  providedIn: 'root'
})
export class TokenServiceService {

  constructor(private cookieService: CookieService) { }

  getToken(): string | null {
    return this.cookieService.get("jwt")
  }

  getTokenRole(): Role | null {
    const token = this.getToken();
    if (token) {
      try {
        const decodedToken: any = jwtDecode(token);
        // Microsoft uses very specfic naming.
        const roleString: string = decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
        if (roleString in Role) {
          return Role[roleString as keyof typeof Role];
        } else {
          console.error(`Not valid Role: "${roleString}".`);
          return null;
        }
      } catch (error) {
        console.error("Invalid token", error);
        return null;
      }
    }
    return null;
  }
}
