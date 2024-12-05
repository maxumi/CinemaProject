import { Component, OnInit } from '@angular/core';
import { MovieService } from '../../services/movie.service';
import { Movie } from '../../models/movie.models';
import { MovieSession } from '../../models/movie-session.models';
import { CinemaHall } from '../../models/cinema-hall.models';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-movie-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './movie-list.component.html',
  styleUrls: ['./movie-list.component.css'],
})
export class MovieListComponent {
  movies: Movie[] = [];
  hasMoreMovies = false;

  movieSessions: MovieSession[] = [];
  hasMoreSessions = false;

  cinemaHalls: CinemaHall[] = [];

  moviesPage = 1;
  moviesAmount = 5;
  sessionsPage = 1;
  sessionsAmount = 10;

  expandedState: { [movieId: number]: boolean } = {}; // State to handle more text.

  constructor(private movieService: MovieService) {}

  ngOnInit(): void {
    this.fetchFrontPageData();
  }

  fetchFrontPageData(): void {
    this.movieService.getFrontPage(this.moviesPage, this.moviesAmount, this.sessionsPage, this.sessionsAmount).subscribe(
      (data) => {
        this.movies = data.movies.items;
        this.hasMoreMovies = data.movies.hasMore;

        this.movieSessions = data.movieSessions.items;
        this.hasMoreSessions = data.movieSessions.hasMore;

        this.cinemaHalls = data.cinemaHalls;
      },
      (error) => {
        console.error('Error fetching front page data:', error);
      }
    );
  }

  goToNextMoviesPage(): void {
    if (this.hasMoreMovies) {
      this.moviesPage++;
      this.fetchFrontPageData();
    }
  }

  goToPreviousMoviesPage(): void {
    if (this.moviesPage > 1) {
      this.moviesPage--;
      this.fetchFrontPageData();
    }
  }

  getSessionsForMovie(movieId: number) {
    return this.movieSessions
      .filter((session) => session.movieId === movieId)
      .map((session) => ({
        session,
        hall: this.cinemaHalls.find((hall) => hall.id === session.cinemaHallId) || null,
      }));
  }

  // This is a placeholder
  viewMoreSessionsForMovie(movieId: number): void {
    console.log(`View more sessions for movie ID: ${movieId}`);
  }

  toggleDescription(movieId: number): void {
    this.expandedState[movieId] = !this.expandedState[movieId];
  }

  isExpanded(movieId: number): boolean {
    return !!this.expandedState[movieId];
  }
}
