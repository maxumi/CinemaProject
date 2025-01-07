import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { MovieService } from '../../../services/movie.service';
import { CreateMovieSessionDto, MovieSession, UpdateMovieSessionDto } from '../../../models/movie-session.models';
import { CinemaHall } from '../../../models/cinema-hall.models';
import { MovieSessionService } from '../../../services/movie-session.service';
import { MovieItem } from '../../../models/movie.models';
import { CinemaHallService } from '../../../services/cinema-hall.service';

@Component({
  selector: 'app-admin-movie-session',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './admin-movie-session.component.html',
  styleUrls: ['./admin-movie-session.component.css']
})
export class AdminMovieSessionComponent {
  sessionForm: FormGroup;
  sessions: MovieSession[] = [];
  selectedSessionId: number | null = null;

  movies: MovieItem[] = [];
  cinemaHalls: CinemaHall[] = [];

  constructor(
    private sessionService: MovieSessionService,
    private movieService: MovieService,
    private cinemaHallService: CinemaHallService,
    private fb: FormBuilder
  ) {
    this.sessionForm = this.fb.group({
      movieId: [null, [Validators.required]],
      cinemaHallId: [null, [Validators.required]],
      startTime: ['', [Validators.required]],
      endTime: ['', [Validators.required]],
      price: [0, [Validators.required, Validators.min(0)]]
    });
  }

  ngOnInit(): void {
    this.loadSessions();
    this.loadMovies();
    this.loadCinemaHalls();
  }

  loadSessions(): void {
    this.sessionService.getAllMovieSessions().subscribe({
      next: (sessions) => (this.sessions = sessions),
      error: (err) => console.error('Failed to load movie sessions:', err)
    });
  }

  loadMovies(): void {
    this.movieService.getMovieTitles().subscribe({
      next: (movies) => (this.movies = movies),
      error: (err) => console.error('Failed to load movies:', err)
    });
  }

  loadCinemaHalls(): void {
    this.cinemaHallService.getAllCinemaHalls().subscribe({
      next: (halls) => (this.cinemaHalls = halls),
      error: (err) => console.error('Failed to load cinema halls:', err)
    });
  }

  selectSession(sessionId: number): void {
    this.selectedSessionId = sessionId;
    this.sessionService.getMovieSessionById(sessionId).subscribe({
      next: (session) => {
        this.sessionForm.setValue({
          movieId: session.movieId,
          cinemaHallId: session.cinemaHallId,
          startTime: this.formatDateForInput(session.startTime.toString()),
          endTime: this.formatDateForInput(session.endTime.toString()),
          price: session.price
        });
      },
      error: (err) => console.error(`Failed to load session ID ${sessionId}:`, err)
    });
  }

  saveSession(): void {
    if (!this.sessionForm.valid) return;

    if (this.selectedSessionId === null) {
      this.createSession();
    } else {
      this.updateSession();
    }
  }

  createSession(): void {
    const newSession: CreateMovieSessionDto = this.getSessionDtoFromForm();
    this.sessionService.createMovieSession(newSession).subscribe({
      next: () => {
        this.loadSessions();
        this.resetForm();
      },
      error: (err) => console.error('Failed to create session:', err)
    });
  }

  updateSession(): void {
    if (this.selectedSessionId !== null) {
      const updatedSession: UpdateMovieSessionDto = this.getSessionDtoFromForm();
      this.sessionService.updateMovieSession(this.selectedSessionId, updatedSession).subscribe({
        next: () => {
          this.loadSessions();
          this.resetForm();
        },
        error: (err) => console.error('Failed to update session:', err)
      });
    }
  }

  deleteSession(sessionId: number): void {
    if (confirm('Are you sure you want to delete this session?')) {
      this.sessionService.deleteMovieSession(sessionId).subscribe({
        next: () => {
          this.loadSessions();
          if (this.selectedSessionId === sessionId) {
            this.resetForm();
          }
        },
        error: (err) => console.error('Failed to delete session:', err)
      });
    }
  }

  newSession(): void {
    this.resetForm();
  }

  private resetForm(): void {
    this.sessionForm.reset({
      movieId: null,
      cinemaHallId: null,
      startTime: '',
      endTime: '',
      price: 0
    });
    this.selectedSessionId = null;
  }

  private getSessionDtoFromForm(): CreateMovieSessionDto | UpdateMovieSessionDto {
    const formValue = this.sessionForm.value;
    return {
      movieId: formValue.movieId,
      cinemaHallId: formValue.cinemaHallId,
      startTime: new Date(formValue.startTime),
      endTime: new Date(formValue.endTime),
      price: formValue.price
    };
  }

  // Helper to format the date/time string if you use datetime-local input
  private formatDateForInput(dateStr: string): string {
    const date = new Date(dateStr);
    const year = date.getFullYear();
    const month = String(date.getMonth()+1).padStart(2,'0');
    const day = String(date.getDate()).padStart(2,'0');
    const hours = String(date.getHours()).padStart(2,'0');
    const minutes = String(date.getMinutes()).padStart(2,'0');
    return `${year}-${month}-${day}T${hours}:${minutes}`;
  }
}