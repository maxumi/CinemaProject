import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Movie } from '../../models/movie.models';
import { CinemaHall } from '../../models/cinema-hall.models';
import { MovieSession } from '../../models/movie-session.models';

@Component({
  selector: 'app-movie-list',
  imports: [CommonModule],
  standalone: true,
  templateUrl: './movie-list.component.html',
  styleUrls: ['./movie-list.component.css'],
})
export class MovieListComponent {
  // Mock data for movies
  movies: Movie[] = [
    {
      id: 1,
      title: 'Inception',
      durationMinutes: 148,
      releaseDate: new Date('2010-07-16'),
      description:
        'A thief who steals corporate secrets using dream-sharing technology is given the inverse task of planting an idea.',
      genres: ['Sci-Fi', 'Thriller'],
    },
    {
      id: 2,
      title: 'The Dark Knight',
      durationMinutes: 152,
      releaseDate: new Date('2008-07-18'),
      description:
        'Batman sets out to dismantle the remaining criminal organizations in Gotham, only to be faced with the Joker.',
      genres: ['Action', 'Drama'],
    },
    {
      id: 3,
      title: 'Pulp Fiction',
      durationMinutes: 154,
      releaseDate: new Date('1994-10-14'),
      description:
        'The lives of two mob hitmen, a boxer, and a gangster’s wife intertwine in a series of incidents.',
      genres: ['Crime', 'Drama'],
    },
    {
      id: 4,
      title: 'The Matrix',
      durationMinutes: 136,
      releaseDate: new Date('1999-03-31'),
      description:
        'A computer hacker learns about the true nature of his reality and his role in the war against its controllers.',
      genres: ['Sci-Fi', 'Action'],
    },
    {
      id: 5,
      title: 'Forrest Gump',
      durationMinutes: 142,
      releaseDate: new Date('1994-07-06'),
      description:
        'The story of a man with a low IQ who achieves extraordinary things in his life while meeting historical figures.',
      genres: ['Drama', 'Romance'],
    },
    {
      id: 6,
      title: 'Gladiator',
      durationMinutes: 155,
      releaseDate: new Date('2000-05-01'),
      description:
        'A betrayed Roman general fights his way back to seek vengeance against the corrupt emperor who murdered his family.',
      genres: ['Action', 'Drama'],
    },
  ];

  // Mock data for cinema halls
  cinemaHalls: CinemaHall[] = [
    { id: 1, name: 'Hall A', capacity: 100 },
    { id: 2, name: 'Hall B', capacity: 120 },
    { id: 3, name: 'Hall C', capacity: 80 },
    { id: 4, name: 'Hall D', capacity: 150 },
  ];

  // Mock data for movie sessions
  movieSessions: MovieSession[] = [
    {
      id: 101,
      movieId: 1,
      cinemaHallId: 1,
      startTime: new Date('2024-11-27T10:00:00'),
      endTime: new Date('2024-11-27T12:30:00'),
      price: 12.99,
    },
    {
      id: 102,
      movieId: 1,
      cinemaHallId: 2,
      startTime: new Date('2024-11-27T15:00:00'),
      endTime: new Date('2024-11-27T17:30:00'),
      price: 14.99,
    },
    {
      id: 103,
      movieId: 1,
      cinemaHallId: 3,
      startTime: new Date('2024-11-27T18:00:00'),
      endTime: new Date('2024-11-27T20:30:00'),
      price: 13.99,
    },
    {
      id: 104,
      movieId: 1,
      cinemaHallId: 4,
      startTime: new Date('2024-11-27T21:00:00'),
      endTime: new Date('2024-11-27T23:30:00'),
      price: 15.99,
    },
    {
      id: 201,
      movieId: 2,
      cinemaHallId: 1,
      startTime: new Date('2024-11-27T12:00:00'),
      endTime: new Date('2024-11-27T14:30:00'),
      price: 10.99,
    },
    {
      id: 301,
      movieId: 3,
      cinemaHallId: 3,
      startTime: new Date('2024-11-27T18:00:00'),
      endTime: new Date('2024-11-27T20:30:00'),
      price: 11.99,
    },
    {
      id: 401,
      movieId: 4,
      cinemaHallId: 4,
      startTime: new Date('2024-11-28T14:00:00'),
      endTime: new Date('2024-11-28T16:30:00'),
      price: 15.99,
    },
    {
      id: 501,
      movieId: 5,
      cinemaHallId: 2,
      startTime: new Date('2024-11-29T10:00:00'),
      endTime: new Date('2024-11-29T12:30:00'),
      price: 9.99,
    },
    {
      id: 601,
      movieId: 6,
      cinemaHallId: 3,
      startTime: new Date('2024-11-30T18:00:00'),
      endTime: new Date('2024-11-30T20:30:00'),
      price: 12.99,
    },
  ];
  

  // Expanded state for movies
  expandedState: { [movieId: number]: boolean } = {};

  // Pagination variables
  currentPage = 1;
  itemsPerPage = 5; // Show 5 movies per page
  totalPages = Math.ceil(this.movies.length / this.itemsPerPage);

  // Get paginated movies
  get paginatedMovies() {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    return this.movies.slice(startIndex, startIndex + this.itemsPerPage);
  }

  // Get sessions for a movie
  getSessionsForMovie(movieId: number) {
    return this.movieSessions.filter((session) => session.movieId === movieId).map((session) => ({
      ...session,
      cinemaHall: this.cinemaHalls.find((hall) => hall.id === session.cinemaHallId),
    }));
  }

  // Toggle the expanded state for a movie
  toggleDescription(movieId: number): void {
    this.expandedState[movieId] = !this.expandedState[movieId];
  }

  // Check if a movie is expanded
  isExpanded(movieId: number): boolean {
    return !!this.expandedState[movieId];
  }

  // Pagination methods
  goToNextPage() {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
    }
  }

  goToPreviousPage() {
    if (this.currentPage > 1) {
      this.currentPage--;
    }
  }
}
