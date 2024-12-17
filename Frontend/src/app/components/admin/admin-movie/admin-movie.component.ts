import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MovieItem, Genre, CreateMovieDto, UpdateMovieDto } from '../../../models/movie.models';
import { MovieService } from '../../../services/movie.service';
import { CommonModule, DatePipe } from '@angular/common';

@Component({
  selector: 'app-admin-movie',
  standalone: true,
  imports: [CommonModule, FormsModule,ReactiveFormsModule],
  templateUrl: './admin-movie.component.html',
  styleUrls: ['./admin-movie.component.css'],
  providers: [DatePipe], 
})
export class AdminMovieComponent {
    movieForm: FormGroup;
    movieTitles: MovieItem[] = [];
    genres: Genre[] = [];
    selectedMovieId: number | null = null;
  
    private datePipe = inject(DatePipe)
  
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
            releaseDate: this.datePipe.transform(movie.releaseDate, 'yyyy-MM-dd'),
            description: movie.description,
            genreIds: this.mapGenreNamesToIds(movie.genres)
          });
        },
        error: (err) => console.error(`Failed to load movie details for ID ${movieId}:`, err)
      });
    }
  
    saveMovie(): void {
      if (!this.movieForm.valid) return;
  
      if (this.selectedMovieId === null) {
        this.createMovie();
      } else {
        this.updateMovie();
      }
    }
  
    createMovie(): void {
      const newMovie: CreateMovieDto = this.movieForm.value;
  
      this.movieService.createMovie(newMovie).subscribe({
        next: () => {
          this.loadMovieTitles();
          this.resetForm();
        },
        error: (err) => console.error('Failed to create movie:', err)
      });
    }
  
    updateMovie(): void {
      if (this.selectedMovieId !== null) {
        const updatedMovie: UpdateMovieDto = this.movieForm.value;
  
        this.movieService.updateMovie(this.selectedMovieId, updatedMovie).subscribe({
          next: () => {
            this.loadMovieTitles();
            this.resetForm();
          },
          error: (err) => console.error('Failed to update movie:', err)
        });
      }
    }
  
    deleteMovie(): void {
      if (this.selectedMovieId !== null && confirm('Are you sure you want to delete this movie?')) {
        this.movieService.deleteMovie(this.selectedMovieId).subscribe({
          next: () => {
            this.loadMovieTitles();
            this.resetForm();
          },
          error: (err) => console.error('Failed to delete movie:', err)
        });
      }
    }
  
    newMovie(): void {
      this.resetForm();
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