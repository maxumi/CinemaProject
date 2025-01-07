import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Subject, debounceTime, distinctUntilChanged, switchMap } from 'rxjs';
import { Booking, CreateBookingDto, UpdateBookingDto } from '../../../models/bookings.models';
import { BookingService } from '../../../services/booking.service';
import { UserItem } from '../../../models/user.models';
import { UserService } from '../../../services/user.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-admin-booking',
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './admin-booking.component.html',
  styleUrl: './admin-booking.component.css'
})
export class AdminBookingComponent {
  bookingForm: FormGroup;
  bookings: Booking[] = [];
  selectedBookingId: number | null = null;
  users: UserItem[] = [];
  searchUserSubject = new Subject<string>();

  constructor(
    private bookingService: BookingService,
    private userService: UserService,
    private fb: FormBuilder
  ) {
    this.bookingForm = this.fb.group({
      userId: ['', [Validators.required]],
      numberOfTickets: [0, [Validators.required, Validators.min(1)]],
      movieSessionId: ['', [Validators.required]],
      paymentDetailId: ['', [Validators.required]],
      seatIds: ['', [Validators.required]], // Comma-separated
    });
  }

  ngOnInit() {
    this.loadBookings();
    this.setupUserSearch();
  }

  loadBookings() {
    this.bookingService.getBookings().subscribe({
      next: (bookings) => (this.bookings = bookings),
      error: (err) => console.error('Failed to load bookings:', err),
    });
  }

  setupUserSearch() {
    this.searchUserSubject
      .pipe(
        debounceTime(300), // Wait for 300ms after the last input
        switchMap((query) => {
          if (!query.trim()) {
            // If query is empty, clear the dropdown
            this.users = [];
            return [];
          }
          return this.userService.getSelectedUsers(query);
        })
      )
      .subscribe({
        next: (users) => (this.users = users),
        error: (err) => console.error('Failed to search users:', err),
      });
  }
  

  onUserSearch(query: string) {
    this.searchUserSubject.next(query);
  }

  selectUser(user: UserItem) {
    this.bookingForm.patchValue({ userId: user.id }); // Set the user ID in the form
    this.users = []; // Clear the dropdown after selection
  }

  selectBooking(bookingId: number) {
    this.selectedBookingId = bookingId;
    this.bookingService.getBookingById(bookingId).subscribe({
      next: (booking) => {
        this.bookingForm.setValue({
          userId: booking.userId,
          numberOfTickets: booking.numberOfTickets,
          movieSessionId: booking.movieSessionId,
          paymentDetailId: booking.paymentDetailId,
          seatIds: booking.seatIds.join(','),
        });
      },
      error: (err) => console.error(`Failed to load booking details for ID ${bookingId}:`, err),
    });
  }

  saveBooking() {
    if (!this.bookingForm.valid) return;

    if (this.selectedBookingId === null) {
      this.createBooking();
    } else {
      this.updateBooking();
    }
  }

  createBooking() {
    const newBooking: CreateBookingDto = {
      ...this.bookingForm.value,
      seatIds: this.parseSeatIds(this.bookingForm.value.seatIds),
    };

    this.bookingService.createBooking(newBooking).subscribe({
      next: () => {
        this.loadBookings();
        this.resetForm();
      },
      error: (err) => console.error('Failed to create booking:', err),
    });
  }

  updateBooking() {
    if (this.selectedBookingId !== null) {
      const updatedBooking: UpdateBookingDto = {
        ...this.bookingForm.value,
        seatIds: this.parseSeatIds(this.bookingForm.value.seatIds),
      };

      this.bookingService.updateBooking(this.selectedBookingId, updatedBooking).subscribe({
        next: () => {
          this.loadBookings();
          this.resetForm();
        },
        error: (err) => console.error('Failed to update booking:', err),
      });
    }
  }

  deleteBooking() {
    if (this.selectedBookingId !== null && confirm('Are you sure you want to delete this booking?')) {
      this.bookingService.deleteBooking(this.selectedBookingId).subscribe({
        next: () => {
          this.loadBookings();
          this.resetForm();
        },
        error: (err) => console.error('Failed to delete booking:', err),
      });
    }
  }

  newBooking() {
    this.resetForm();
  }

  private parseSeatIds(seatIds: string): number[] {
    return seatIds.split(',').map((id) => parseInt(id.trim(), 10)).filter((id) => !isNaN(id));
  }

  private resetForm() {
    this.bookingForm.reset({
      userId: '',
      numberOfTickets: 0,
      movieSessionId: '',
      paymentDetailId: '',
      seatIds: '',
    });
    this.selectedBookingId = null;
    this.users = [];
  }
  
}
