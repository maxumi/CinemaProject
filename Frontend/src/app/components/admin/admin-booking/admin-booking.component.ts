import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Subject, debounceTime, switchMap } from 'rxjs';
import { BookingService } from '../../../services/booking.service';
import { UserService } from '../../../services/user.service';
import { MovieSessionService } from '../../../services/movie-session.service';
import { UserItem } from '../../../models/user.models';
import { Booking, CreateDetailedBooking, PaymentMethod } from '../../../models/bookings.models';
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
  bookingForm: FormGroup;
  bookings: Booking[] = [];
  users: UserItem[] = [];
  movieSessions: MovieSession[] = [];
  seats: Seat[] = [];
  selectedSeatIds: number[] = [];
  selectedBookingId: number | null = null;
  private searchUserSubject = new Subject<string>();

  constructor(
    private bookingService: BookingService,
    private userService: UserService,
    private movieSessionService: MovieSessionService,
    private fb: FormBuilder
  ) {
    this.bookingForm = this.fb.group({
      userId: ['', Validators.required],
      numberOfTickets: [{ value: 0, disabled: true }, [Validators.required, Validators.min(1)]],
      movieSessionId: ['', Validators.required],
      seatIds: ['', Validators.required],
      paymentDetail: this.fb.group({
        amount: [{ value: 0, disabled: true }, Validators.required],
        method: ['', Validators.required],
        date: [new Date(), Validators.required],
      }),
    });
  }

  ngOnInit(): void {
    this.loadBookings();
    this.loadMovieSessions();
    this.setupUserSearch();
  }

  loadBookings() {
    this.bookingService.getBookings().subscribe({
      next: (b) => (this.bookings = b),
    });
  }

  loadMovieSessions() {
    this.movieSessionService.getAllMovieSessions().subscribe({
      next: (ms) => (this.movieSessions = ms),
    });
  }

  loadSeats(id: number) {
    this.movieSessionService.getSeatsBySessionId(id).subscribe({
      next: (s) => (this.seats = s),
    });
  }

  setupUserSearch() {
    this.searchUserSubject
      .pipe(
        debounceTime(300),
        switchMap((q) => (q.trim() ? this.userService.getSelectedUsers(q) : []))
      )
      .subscribe({
        next: (u) => (this.users = u),
      });
  }

  onUserSearch(q: string) {
    this.searchUserSubject.next(q);
  }

  selectUser(u: UserItem) {
    this.bookingForm.patchValue({ userId: u.id });
    this.users = [];
  }

  onSessionChange(v: string) {
    const parsed = +v;
    if (!isNaN(parsed)) {
      this.bookingForm.patchValue({ movieSessionId: parsed });
      this.loadSeats(parsed);
      this.selectedSeatIds = [];
      this.bookingForm.patchValue({ seatIds: '' });
      this.updateBookingFields(); // Single function call
    }
  }

  toggleSeatSelection(id: number) {
    if (this.selectedSeatIds.includes(id)) {
      this.selectedSeatIds = this.selectedSeatIds.filter(x => x !== id);
    } else {
      this.selectedSeatIds.push(id);
    }
    this.bookingForm.patchValue({ seatIds: this.selectedSeatIds.join(',') }, { emitEvent: false });
    this.updateBookingFields(); // Single function call
  }

  /** Single function that updates tickets & amount. */
  private updateBookingFields() {
    const val = this.bookingForm.getRawValue();
    const msId = val.movieSessionId;

    // 1) Update numberOfTickets based on selected seats
    const count = this.selectedSeatIds.length;
    this.bookingForm.get('numberOfTickets')?.patchValue(count, { emitEvent: false });

    // 2) Calculate price
    if (!msId || count < 1) {
      this.bookingForm.get('paymentDetail')?.patchValue({ amount: 0 }, { emitEvent: false });
      return;
    }
    const session = this.movieSessions.find(s => s.id === msId);
    if (!session) {
      this.bookingForm.get('paymentDetail')?.patchValue({ amount: 0 }, { emitEvent: false });
      return;
    }
    const total = session.price * count;
    this.bookingForm.get('paymentDetail')?.patchValue({ amount: total }, { emitEvent: false });
  }

  createBooking() {
    if (this.bookingForm.invalid) return;

    const fv = this.bookingForm.getRawValue();
    const paymentMethodNumber = PaymentMethod[fv.paymentDetail.method as keyof typeof PaymentMethod];


    const dto: CreateDetailedBooking = {
      userId: fv.userId,
      numberOfTickets: fv.numberOfTickets,
      movieSessionId: fv.movieSessionId,
      seatIds: this.selectedSeatIds,
      totalAmount: fv.paymentDetail.amount,
      bookingDate: fv.paymentDetail.date,
      paymentDetail: {
        amount: fv.paymentDetail.amount,
        method: paymentMethodNumber,
        date: fv.paymentDetail.date,
      },
    };
    this.bookingService.createBooking(dto).subscribe(() => {
      this.loadBookings();
      this.resetForm();
    });
  }

  newBooking() {
    this.resetForm();
  }

  deleteBooking() {
    if (this.selectedBookingId !== null) {
      this.bookingService.deleteBooking(this.selectedBookingId).subscribe(() => {
        this.loadBookings();
        this.resetForm();
      });
    }
  }

  selectBooking(id: number) {
    this.selectedBookingId = id;
  }

  resetForm() {
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
