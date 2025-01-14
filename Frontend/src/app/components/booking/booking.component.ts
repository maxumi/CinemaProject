import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { CinemaHall } from '../../models/cinema-hall.models';
import { Seat } from '../../models/seat.models';
import { MovieItem } from '../../models/movie.models';
import { MovieSession } from '../../models/movie-session.models';
import { CreateDetailedBooking, PaymentDetail, PaymentMethod } from '../../models/bookings.models';

import { BookingService } from '../../services/booking.service';
import { CinemaHallService } from '../../services/cinema-hall.service';
import { MovieService } from '../../services/movie.service';
import { MovieSessionService } from '../../services/movie-session.service';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-booking',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './booking.component.html',
  styleUrls: ['./booking.component.css'],
})
export class BookingComponent implements OnInit {
  private fb = inject(FormBuilder);
  private route = inject(ActivatedRoute);
  private bookingService = inject(BookingService);
  private movieService = inject(MovieService);
  private userService = inject(UserService);
  private movieSessionService = inject(MovieSessionService);
  private cinemaHallService = inject(CinemaHallService);

  bookingForm: FormGroup;
  movies: MovieItem[] = [];
  movieSessions: MovieSession[] = [];
  cinemaHalls: CinemaHall[] = [];
  selectedMovie: MovieItem | null = null;
  selectedSession: MovieSession | null = null;
  selectedHall: CinemaHall | null = null;
  availableSeats: Seat[] = [];
  selectedSeats: Seat[] = [];
  currentUserId = 0;

  constructor() {
    this.bookingForm = this.fb.group({
      movieId: [null, Validators.required],
      movieSessionId: [null, Validators.required],
      paymentMethod: ['Online', Validators.required],
    });
  }

  ngOnInit(): void {
    this.movieService.getMovieTitles().subscribe((movies) => {
      this.movies = movies;
    });

    this.userService.getCurrentUser().subscribe((user) => {
      this.currentUserId = user.id;
    });

    const sessionId = +this.route.snapshot.paramMap.get('movieSessionId')!;
    if (sessionId) {
      this.loadSessionFromRoute(sessionId);
    }
  }

  loadSessionFromRoute(sessionId: number) {
    this.movieSessionService.getMovieSessionById(sessionId).subscribe((session) => {
      this.selectedSession = session;

      this.selectedMovie = this.movies.find((m) => m.id === session.movieId) || null;

      this.movieSessionService.getMovieSessionsByMovieId(session.movieId).subscribe((sessions) => {
        this.movieSessions = sessions;
      });

      this.cinemaHallService.getCinemaHallById(session.cinemaHallId).subscribe((hall) => {
        this.selectedHall = hall;
      });

      this.bookingForm.patchValue({
        movieId: session.movieId,
        movieSessionId: session.id,
      });

      this.movieSessionService.getSeatsBySessionId(session.id).subscribe((seats) => {
        this.availableSeats = seats;
      });
    });
  }

  onMovieChange(movieId: number) {
    if (!movieId) {
      this.resetForm();
      return;
    }

    this.selectedMovie = this.movies.find((m) => m.id === movieId) || null;

    this.movieSessionService.getMovieSessionsByMovieId(movieId).subscribe((sessions) => {
      this.movieSessions = sessions;
    });

    this.resetSessionAndSeats();
  }

  onMovieSessionChange(sessionId: number) {
    if (!sessionId) {
      this.resetSessionAndSeats();
      return;
    }

    this.movieSessionService.getMovieSessionById(sessionId).subscribe((session) => {
      this.selectedSession = session;
      this.selectedHall = null;
      this.availableSeats = [];
      this.selectedSeats = [];

      this.cinemaHallService.getCinemaHallById(session.cinemaHallId).subscribe((hall) => {
        this.selectedHall = hall;
      });

      this.movieSessionService.getSeatsBySessionId(session.id).subscribe((seats) => {
        this.availableSeats = seats;
      });
    });
  }

  // Reset everything except the payment method
  private resetSessionAndSeats() {
    this.bookingForm.patchValue(
      { movieSessionId: null },
      { emitEvent: false }
    );
    this.selectedSession = null;
    this.selectedHall = null;
    this.availableSeats = [];
    this.selectedSeats = [];
  }

  // Full reset
  resetForm() {
    if (!this.bookingForm.pristine) {
      this.bookingForm.reset(
        { paymentMethod: 'Online' },
        { emitEvent: false }
      );
    }

    this.selectedMovie = null;
    this.selectedSession = null;
    this.selectedHall = null;
    this.availableSeats = [];
    this.selectedSeats = [];
  }

  getTotalAmount(): number {
    if (!this.selectedSession || !this.selectedSeats.length) {
      return 0;
    }
    return this.selectedSession.price * this.selectedSeats.length;
  }

  toggleSeatSelection(seat: Seat) {
    const index = this.selectedSeats.findIndex((s) => s.id === seat.id);
    if (index > -1) {
      this.selectedSeats.splice(index, 1);
    } else {
      this.selectedSeats.push(seat);
    }
  }

  isSeatSelected(seat: Seat): boolean {
    return this.selectedSeats.some((s) => s.id === seat.id);
  }

  getSelectedSeatNames(): string {
    return this.selectedSeats.map((seat) => seat.seatNumber).join(', ');
  }

  getFilteredSessions() {
    if (!this.selectedMovie) {
      return [];
    }
    return this.movieSessions.filter((s) => s.movieId === this.selectedMovie!.id);
  }

  onSubmit() {
    if (this.bookingForm.invalid || !this.selectedSeats.length) {
      alert('The form is not complete.');
      return;
    }

    const paymentDetail: PaymentDetail = {
      amount: this.getTotalAmount(),
      method: PaymentMethod[
        this.bookingForm.value.paymentMethod as keyof typeof PaymentMethod
      ],
      date: new Date(),
    };

    const booking: CreateDetailedBooking = {
      userId: this.currentUserId,
      numberOfTickets: this.selectedSeats.length,
      movieSessionId: this.selectedSession?.id!,
      seatIds: this.selectedSeats.map((seat) => seat.id),
      totalAmount: paymentDetail.amount,
      bookingDate: new Date(),
      paymentDetail,
    };

    this.bookingService.createBooking(booking).subscribe({
      next: () => {
        alert('Booking submitted works!');
        this.resetForm();
      },
      error: (err) => {
        console.error('Error submitting booking:', err);
        alert('An error occurred.');
      },
    });
  }
}
