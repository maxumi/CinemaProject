import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CinemaHall } from '../../models/cinema-hall.models';
import { Seat } from '../../models/seat.models';
import { ActivatedRoute } from '@angular/router';
import { Movie } from '../../models/movie.models';
import { MovieSession } from '../../models/movie-session.models';
import { CreateDetailedBooking, PaymentDetail } from '../../models/bookings.models';

@Component({
  selector: 'app-booking',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './booking.component.html',
  styleUrls: ['./booking.component.css'],
})
export class BookingComponent implements OnInit {
  bookingForm: FormGroup;

  movies: Movie[] = [
    {
      id: 1,
      title: 'Movie A',
      durationMinutes: 120,
      releaseDate: new Date('2025-01-01T00:00:00Z'),
      description: 'An epic adventure.',
      genres: ['Adventure', 'Drama'],
    },
    {
      id: 2,
      title: 'Movie B',
      durationMinutes: 90,
      releaseDate: new Date('2025-02-15T00:00:00Z'),
      description: 'A heartwarming comedy.',
      genres: ['Comedy'],
    },
    {
      id: 3,
      title: 'Movie C',
      durationMinutes: 140,
      releaseDate: new Date('2025-03-10T00:00:00Z'),
      description: 'A thrilling sci-fi story.',
      genres: ['Sci-Fi', 'Thriller'],
    },
    {
      id: 4,
      title: 'Movie D',
      durationMinutes: 110,
      releaseDate: new Date('2025-04-05T00:00:00Z'),
      description: 'A historical drama.',
      genres: ['Drama', 'History'],
    },
  ];
  
  movieSessions: MovieSession[] = [
    {
      id: 1,
      movieId: 1,
      cinemaHallId: 1,
      startTime: new Date('2025-01-01T10:00:00Z'),
      endTime: new Date('2025-01-01T12:00:00Z'),
      price: 14,
    },
    {
      id: 2,
      movieId: 2,
      cinemaHallId: 2,
      startTime: new Date('2025-01-01T15:30:00Z'),
      endTime: new Date('2025-01-01T17:30:00Z'),
      price: 14,
    },
    {
      id: 3,
      movieId: 3,
      cinemaHallId: 3,
      startTime: new Date('2025-02-01T14:00:00Z'),
      endTime: new Date('2025-02-01T16:20:00Z'),
      price: 16,
    },
    {
      id: 4,
      movieId: 4,
      cinemaHallId: 4,
      startTime: new Date('2025-02-05T18:00:00Z'),
      endTime: new Date('2025-02-05T19:50:00Z'),
      price: 15,
    },
    {
      id: 5,
      movieId: 1,
      cinemaHallId: 1,
      startTime: new Date('2025-03-01T10:30:00Z'),
      endTime: new Date('2025-03-01T12:30:00Z'),
      price: 14,
    },
    {
      id: 6,
      movieId: 2,
      cinemaHallId: 2,
      startTime: new Date('2025-03-10T15:00:00Z'),
      endTime: new Date('2025-03-10T17:10:00Z'),
      price: 14,
    },
    {
      id: 7,
      movieId: 3,
      cinemaHallId: 3,
      startTime: new Date('2025-04-01T18:00:00Z'),
      endTime: new Date('2025-04-01T20:10:00Z'),
      price: 16,
    },
    {
      id: 8,
      movieId: 4,
      cinemaHallId: 4,
      startTime: new Date('2025-04-10T14:30:00Z'),
      endTime: new Date('2025-04-10T16:20:00Z'),
      price: 15,
    },
  ];

  cinemaHalls: CinemaHall[] = [
    { id: 1, name: 'Hall A', capacity: 50, seatIds: [1, 2, 3, 4] },
    { id: 2, name: 'Hall B', capacity: 30, seatIds: [5, 6, 7, 8] },
    { id: 3, name: 'Hall C', capacity: 40, seatIds: [9, 10, 11, 12] },
    { id: 4, name: 'Hall D', capacity: 35, seatIds: [13, 14, 15, 16] },
  ];
  
  seats: Seat[] = [
    { id: 1, seatNumber: 'S1', cinemhallId: 1 },
    { id: 2, seatNumber: 'S2', cinemhallId: 1 },
    { id: 3, seatNumber: 'S3', cinemhallId: 1 },
    { id: 4, seatNumber: 'S4', cinemhallId: 1 },
    { id: 5, seatNumber: 'S5', cinemhallId: 2 },
    { id: 6, seatNumber: 'S6', cinemhallId: 2 },
    { id: 7, seatNumber: 'S7', cinemhallId: 2 },
    { id: 8, seatNumber: 'S8', cinemhallId: 2 },
    { id: 9, seatNumber: 'S9', cinemhallId: 3 },
    { id: 10, seatNumber: 'S10', cinemhallId: 3 },
    { id: 11, seatNumber: 'S11', cinemhallId: 3 },
    { id: 12, seatNumber: 'S12', cinemhallId: 3 },
    { id: 13, seatNumber: 'S13', cinemhallId: 4 },
    { id: 14, seatNumber: 'S14', cinemhallId: 4 },
    { id: 15, seatNumber: 'S15', cinemhallId: 4 },
    { id: 16, seatNumber: 'S16', cinemhallId: 4 },
  ];
  

  selectedMovie: Movie | null = null;
  selectedSession: MovieSession | null = null;
  selectedHall: CinemaHall | null = null;
  availableSeats: Seat[] = [];
  selectedSeats: Seat[] = [];

  constructor(private fb: FormBuilder, private route: ActivatedRoute) {
    this.bookingForm = this.fb.group({
      movieId: [null, Validators.required],
      movieSessionId: [null, Validators.required],
      paymentMethod: [null, Validators.required],
      cinemaHall: [null, Validators.required],
    });
  }

  getFilteredSessions() {
    if (!this.selectedMovie) {
      return [];
    }
    return this.movieSessions.filter((s) => s.movieId === this.selectedMovie!.id);
  }

  ngOnInit(): void {
    const movieSessionId = +this.route.snapshot.paramMap.get('movieSessionId')!;
    if (movieSessionId) {
      this.selectSession(movieSessionId);
    }

    this.bookingForm.get('movieId')?.valueChanges.subscribe((movieId) => {
      this.onMovieChange(movieId);
    });

    this.bookingForm.get('movieSessionId')?.valueChanges.subscribe((sessionId) => {
      this.onSessionChange(sessionId);
    });
  }

  getTotalAmount(): number {
    if (!this.selectedSession || !this.selectedSeats.length) {
      return 0;
    }
    return this.selectedSession.price * this.selectedSeats.length;
  }

  selectSession(sessionId: number) {
    const session = this.movieSessions.find((s) => s.id === sessionId);
    if (session) {
      this.selectedSession = session;
      this.selectedMovie = this.movies.find((m) => m.id === session.movieId) || null;
      this.selectedHall = this.cinemaHalls.find((h) => h.id === session.cinemaHallId) || null;

      this.bookingForm.patchValue({
        movieId: session.movieId,
        movieSessionId: session.id,
        cinemaHall: session.cinemaHallId,
      });

      this.availableSeats = this.seats.filter((seat) => seat.cinemhallId === session.cinemaHallId);
    }
  }

  onMovieChange(movieId: number) {
    this.selectedMovie = this.movies.find((m) => m.id === movieId) || null;
    this.bookingForm.patchValue({ movieSessionId: null, cinemaHall: null });
    this.selectedSession = null;
    this.selectedHall = null;
    this.availableSeats = [];
    this.selectedSeats = [];
  }

  onSessionChange(sessionId: number) {
    const session = this.movieSessions.find((s) => s.id === sessionId);
    if (session) {
      this.selectedSession = session;
      this.selectedHall = this.cinemaHalls.find((h) => h.id === session.cinemaHallId) || null;

      this.availableSeats = this.seats.filter((seat) => seat.cinemhallId === session.cinemaHallId);
    }
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

  onSubmit() {
    if (this.bookingForm.invalid || !this.selectedSeats.length) {
      alert('Please complete the form and select seats before submitting.');
      return;
    }

    const paymentDetail: PaymentDetail = {
      amount: this.getTotalAmount(),
      method: this.bookingForm.value.paymentMethod,
      date: new Date(), // Assuming the payment date is the current date
    };

    const booking: CreateDetailedBooking = {
      userId: 123, // Replace with the actual user ID
      numberOfTickets: this.selectedSeats.length,
      movieSessionId: this.selectedSession?.id!,
      seatIds: this.selectedSeats.map((seat) => seat.id),
      totalAmount: paymentDetail.amount,
      bookingDate: new Date(), // Assuming the booking date is the current date
      paymentDetail,
    };

    console.log('Booking Object:', booking);
    alert('Booking submitted successfully!');
  }
}
