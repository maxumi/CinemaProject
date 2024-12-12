import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MovieItem, Genre } from '../../../models/movie.models';
import { MovieService } from '../../../services/movie.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-admin-movie',
  standalone: true,
  imports: [CommonModule, FormsModule,ReactiveFormsModule],
  templateUrl: './admin-movie.component.html',
  styleUrls: ['./admin-movie.component.css']
})
export class AdminMovieComponent {
  movieForm: FormGroup;
  movieTitles: MovieItem[] = [];
  genres: Genre[] = [];
  selectedMovieId: number | null = null;

  constructor(private movieService: MovieService, private fb: FormBuilder) {
    this.movieForm = this.fb.group({
      title: ['', [Validators.required]],
      durationMinutes: [0, [Validators.required, Validators.min(1)]],
      releaseDate: ['', [Validators.required]],
      description: ['', [Validators.required, Validators.minLength(10)]],
      genreIds: [[]]
    });
  }

  ngOnInit(): void {
    this.loadMovieTitles();
    this.loadGenres();
  }

  loadMovieTitles(): void {
    this.movieService.getMovieTitles().subscribe({
      next: (movies) => (this.movieTitles = movies),
      error: (err) => console.error('Failed to load movie titles:', err)
    });
  }

  loadGenres(): void {
    this.movieService.getGenres().subscribe({
      next: (genres) => (this.genres = genres),
      error: (err) => console.error('Failed to load genres:', err)
    });
  }

  selectMovie(movieId: number): void {
    this.selectedMovieId = movieId;
    this.movieService.getMovieById(movieId).subscribe({
      next: (movie) => {
        this.movieForm.setValue({
          title: movie.title,
          durationMinutes: movie.durationMinutes,
          releaseDate: movie.releaseDate,
          description: movie.description,
          genreIds: this.mapGenreNamesToIds(movie.genres)
        });
      },
      error: (err) => console.error(`Failed to load movie details for ID ${movieId}:`, err)
    });
  }

  updateMovie(): void {
    if (this.movieForm.valid && this.selectedMovieId !== null) {
      this.movieService.updateMovie(this.selectedMovieId, this.movieForm.value).subscribe({
        next: () => {
          this.loadMovieTitles();
          this.resetForm();
        },
        error: (err) => console.error('Failed to update movie:', err)
      });
    }
  }

  private mapGenreNamesToIds(genreNames: string[]): number[] {
    return genreNames
      .map((name) => this.genres.find((genre) => genre.name === name)?.id)
      .filter((id): id is number => id !== undefined);
  }

  private resetForm(): void {
    this.movieForm.reset({
      title: '',
      durationMinutes: 0,
      releaseDate: '',
      description: '',
      genreIds: []
    });
    this.selectedMovieId = null;
  }
}
