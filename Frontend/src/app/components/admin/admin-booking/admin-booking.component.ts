import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Subject, debounceTime, switchMap } from 'rxjs';
import { BookingService } from '../../../services/booking.service';
import { UserService } from '../../../services/user.service';
import { MovieSessionService } from '../../../services/movie-session.service';
import { UserItem } from '../../../models/user.models';
import { Booking, CreateDetailedBooking } from '../../../models/bookings.models';
import { MovieSession } from '../../../models/movie-session.models';
import { Seat } from '../../../models/seat.models';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-admin-booking',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './admin-booking.component.html',
  styleUrls: ['./admin-booking.component.css'],
})
export class AdminBookingComponent implements OnInit {
  // Reactive form for booking creation/edit
  bookingForm: FormGroup;
  
  // Lists & variables for tracking data
  bookings: Booking[] = [];
  users: UserItem[] = [];
  movieSessions: MovieSession[] = [];
  seats: Seat[] = [];
  selectedSeatIds: number[] = [];

  // For user search with debounce
  private searchUserSubject = new Subject<string>();

  // Track which booking is selected (for "Edit" vs "Add")
  selectedBookingId: number | null = null;

  constructor(
    private bookingService: BookingService,
    private userService: UserService,
    private movieSessionService: MovieSessionService,
    private fb: FormBuilder
  ) {
    // Initialize form controls
    this.bookingForm = this.fb.group({
      userId: ['', [Validators.required]],
      numberOfTickets: [0, [Validators.required, Validators.min(1)]],
      movieSessionId: ['', [Validators.required]],
      seatIds: ['', [Validators.required]], // Comma-separated
      paymentDetail: this.fb.group({
        amount: [0, [Validators.required, Validators.min(0.01)]],
        method: ['', [Validators.required]],
        date: [new Date(), [Validators.required]],
      }),
    });
  }

  ngOnInit(): void {
    this.loadBookings();
    this.loadMovieSessions();
    this.setupUserSearch();
  }

  // -----------------------
  // Data Loading
  // -----------------------
  private loadBookings(): void {
    this.bookingService.getBookings().subscribe({
      next: (bookings) => (this.bookings = bookings),
      error: (err) => console.error('Failed to load bookings:', err),
    });
  }

  private loadMovieSessions(): void {
    this.movieSessionService.getAllMovieSessions().subscribe({
      next: (sessions) => (this.movieSessions = sessions),
      error: (err) => console.error('Failed to load movie sessions:', err),
    });
  }

  private loadSeats(sessionId: number): void {
    this.movieSessionService.getSeatsBySessionId(sessionId).subscribe({
      next: (seats) => (this.seats = seats),
      error: (err) => console.error('Failed to load seats:', err),
    });
  }

  // -----------------------
  // User Search
  // -----------------------
  private setupUserSearch(): void {
    this.searchUserSubject
      .pipe(
        debounceTime(300),
        switchMap((query) => {
          if (!query.trim()) {
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

  onUserSearch(query: string): void {
    this.searchUserSubject.next(query);
  }

  selectUser(user: UserItem): void {
    this.bookingForm.patchValue({ userId: user.id });
    this.users = [];
  }

  // -----------------------
  // Session & Seat Management
  // -----------------------
  onSessionChange(sessionId: string): void {
    const parsedSessionId = Number(sessionId);
    if (!isNaN(parsedSessionId)) {
      this.bookingForm.patchValue({ movieSessionId: parsedSessionId });
      this.loadSeats(parsedSessionId);
    } else {
      console.error('Invalid session ID:', sessionId);
    }
  }

  toggleSeatSelection(seatId: number): void {
    if (this.selectedSeatIds.includes(seatId)) {
      this.selectedSeatIds = this.selectedSeatIds.filter((id) => id !== seatId);
    } else {
      this.selectedSeatIds.push(seatId);
    }
    this.bookingForm.patchValue({ seatIds: this.selectedSeatIds.join(',') });
  }

  // -----------------------
  // Booking Management
  // -----------------------
  selectBooking(bookingId: number): void {
    this.selectedBookingId = bookingId;
    // TODO: If you want to edit an existing booking, fetch it and populate the form:
    // e.g., this.bookingService.getBookingById(bookingId).subscribe(...)
    // For now, we'll only highlight the selected booking in the list
  }

  createBooking(): void {
    if (!this.bookingForm.valid) return;

    const formValue = this.bookingForm.value;
    const newBooking: CreateDetailedBooking = {
      userId: formValue.userId,
      numberOfTickets: formValue.numberOfTickets,
      movieSessionId: formValue.movieSessionId,
      seatIds: this.selectedSeatIds,
      totalAmount: formValue.paymentDetail.amount,
      bookingDate: formValue.paymentDetail.date,
      paymentDetail: {
        amount: formValue.paymentDetail.amount,
        method: formValue.paymentDetail.method,
        date: formValue.paymentDetail.date,
      },
    };

    this.bookingService.createBooking(newBooking).subscribe({
      next: () => {
        this.loadBookings();
        this.resetForm();
      },
      error: (err) => console.error('Failed to create booking:', err),
    });
  }

  newBooking(): void {
    this.resetForm();
  }

  deleteBooking(): void {
    if (this.selectedBookingId !== null) {
      if (confirm('Are you sure you want to delete this booking?')) {
        this.bookingService.deleteBooking(this.selectedBookingId).subscribe({
          next: () => {
            this.loadBookings();
            this.resetForm();
          },
          error: (err) => console.error('Failed to delete booking:', err),
        });
      }
    }
  }

  private resetForm(): void {
    this.bookingForm.reset({
      userId: '',
      numberOfTickets: 0,
      movieSessionId: '',
      seatIds: '',
      paymentDetail: {
        amount: 0,
        method: '',
        date: new Date(),
      },
    });
    this.selectedSeatIds = [];
    this.seats = [];
    this.selectedBookingId = null;
  }
}
