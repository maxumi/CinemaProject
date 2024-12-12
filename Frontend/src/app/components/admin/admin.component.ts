import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { AdminMovieComponent } from './admin-movie/admin-movie.component';
import { AdminShowingComponent } from './admin-showing/admin-showing.component';
import { AdminHallComponent } from './admin-hall/admin-hall.component';
import { AdminBookingComponent } from './admin-booking/admin-booking.component';


enum EditSelection {
  Movies,
  Showings,
  Bookings,
  Halls
}

@Component({
  selector: 'app-admin',
  imports: [CommonModule, AdminMovieComponent, AdminShowingComponent, AdminHallComponent, AdminBookingComponent],
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
