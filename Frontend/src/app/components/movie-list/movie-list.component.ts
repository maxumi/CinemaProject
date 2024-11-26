import { Component, OnInit } from '@angular/core';
import { Movie, CreateMovieDto, UpdateMovieDto, Genre, CreateGenreDto, UpdateGenreDto } from '../../models/movie.models';
import { MovieService } from '../../services/movie.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-movie-list',
  imports: [CommonModule, FormsModule],
  standalone: true,
  templateUrl: './movie-list.component.html',
  styleUrls: ['./movie-list.component.css'],
})
export class MovieListComponent implements OnInit {
  // Movies-related properties
  movies: Movie[] = [];
  newMovie: CreateMovieDto = { title: '', durationMinutes: 0, releaseDate: new Date(), description: '', genreIds: [] };
  editMovie?: UpdateMovieDto;
  selectedMovieId?: number; // Store the ID of the movie being edited

  // Genres-related properties
  genres: Genre[] = [];
  newGenre: CreateGenreDto = { name: '' };
  editGenre?: UpdateGenreDto;
  selectedGenreId?: number; // Store the ID of the genre being edited

  constructor(private movieService: MovieService) {}

  ngOnInit(): void {
    this.loadMovies();
    this.loadGenres();
  }

  // Movies operations
  loadMovies(): void {
    this.movieService.getAllMovies().subscribe({
      next: (data) => (this.movies = data),
      error: (err) => console.error('Failed to load movies:', err),
    });
  }

  addMovie(): void {
    this.movieService.createMovie(this.newMovie).subscribe({
      next: () => {
        this.loadMovies();
        this.newMovie = { title: '', durationMinutes: 0, releaseDate: new Date(), description: '', genreIds: [] };
      },
      error: (err) => console.error('Failed to add movie:', err),
    });
  }

  startEditMovie(movie: Movie): void {
    // Since genres are now strings, we only need to work with genre names for display/edit
    // We assume genres is a list of strings and need to map to genre IDs to edit
    const genreIds = this.genres
      .filter(genre => movie.genres.includes(genre.name))
      .map(genre => genre.id);

    this.editMovie = {
      title: movie.title,
      durationMinutes: movie.durationMinutes,
      releaseDate: movie.releaseDate,
      description: movie.description,
      genreIds: genreIds,
    };
    this.selectedMovieId = movie.id; // Store the movie's ID separately
  }

  saveEditMovie(): void {
    if (this.editMovie && this.selectedMovieId !== undefined) {
      this.movieService.updateMovie(this.selectedMovieId, this.editMovie).subscribe({
        next: () => {
          this.loadMovies();
          this.editMovie = undefined;
          this.selectedMovieId = undefined; // Clear the selected ID after saving
        },
        error: (err) => console.error('Failed to edit movie:', err),
      });
    }
  }

  cancelEditMovie(): void {
    this.editMovie = undefined;
    this.selectedMovieId = undefined;
  }

  deleteMovie(id: number): void {
    this.movieService.deleteMovie(id).subscribe({
      next: () => this.loadMovies(),
      error: (err) => console.error('Failed to delete movie:', err),
    });
  }

  // Genres operations
  loadGenres(): void {
    this.movieService.getAllGenres().subscribe({
      next: (data) => (this.genres = data),
      error: (err) => console.error('Failed to load genres:', err),
    });
  }

  addGenre(): void {
    this.movieService.createGenre(this.newGenre).subscribe({
      next: () => {
        this.loadGenres();
        this.newGenre = { name: '' };
      },
      error: (err) => console.error('Failed to add genre:', err),
    });
  }

  startEditGenre(genre: Genre): void {
    this.editGenre = { name: genre.name };
    this.selectedGenreId = genre.id; // Store the genre's ID separately
  }

  saveEditGenre(): void {
    if (this.editGenre && this.selectedGenreId !== undefined) {
      this.movieService.updateGenre(this.selectedGenreId, this.editGenre).subscribe({
        next: () => {
          this.loadGenres();
          this.editGenre = undefined;
          this.selectedGenreId = undefined; // Clear the selected ID after saving
        },
        error: (err) => console.error('Failed to edit genre:', err),
      });
    }
  }

  cancelEditGenre(): void {
    this.editGenre = undefined;
    this.selectedGenreId = undefined;
  }

  deleteGenre(id: number): void {
    this.movieService.deleteGenre(id).subscribe({
      next: () => this.loadGenres(),
      error: (err) => console.error('Failed to delete genre:', err),
    });
  }
}
