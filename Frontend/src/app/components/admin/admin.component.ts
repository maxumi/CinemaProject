import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { AdminMovieComponent } from './admin-movie/admin-movie.component';
import { AdminHallComponent } from './admin-hall/admin-hall.component';
import { AdminBookingComponent } from './admin-booking/admin-booking.component';
import { AdminMovieSessionComponent } from './admin-movie-session/admin-movie-session.component';


enum EditSelection {
  Movies,
  Showings,
  Bookings,
  Halls
}

@Component({
  selector: 'app-admin',
  imports: [CommonModule, AdminMovieComponent, AdminMovieSessionComponent, AdminHallComponent, AdminBookingComponent],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.css'
})
export class AdminComponent {
  editSelection = EditSelection
  currentSelection: EditSelection = EditSelection.Movies;

  setSelection(selection: EditSelection): void {
    this.currentSelection = selection;
  }
}
