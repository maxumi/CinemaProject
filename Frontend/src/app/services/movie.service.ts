import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { CreateGenreDto, CreateMovieDto, Genre, Movie, UpdateGenreDto, UpdateMovieDto } from '../models/movie.models';

@Injectable({
  providedIn: 'root',
})
export class MovieService {
  private readonly apiBaseUrl = `${environment.apiBaseUrl}`;

  constructor(private http: HttpClient) {}

  // Movie-related operations
  getAllMovies(): Observable<Movie[]> {
    return this.http.get<Movie[]>(`${this.apiBaseUrl}/Movie`);
  }

  getMovieById(id: number): Observable<Movie> {
    return this.http.get<Movie>(`${this.apiBaseUrl}/Movie/${id}`);
  }

  createMovie(createMovieDto: CreateMovieDto): Observable<Movie> {
    return this.http.post<Movie>(`${this.apiBaseUrl}/Movie`, createMovieDto);
  }

  updateMovie(id: number, updateMovieDto: UpdateMovieDto): Observable<void> {
    return this.http.put<void>(`${this.apiBaseUrl}/Movie/${id}`, updateMovieDto);
  }

  deleteMovie(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiBaseUrl}/Movie/${id}`);
  }

  // Genre-related operations
  getAllGenres(): Observable<Genre[]> {
    return this.http.get<Genre[]>(`${this.apiBaseUrl}/Genre`);
  }

  createGenre(createGenreDto: CreateGenreDto): Observable<Genre> {
    return this.http.post<Genre>(`${this.apiBaseUrl}/Genre`, createGenreDto);
  }

  updateGenre(id: number, updateGenreDto: UpdateGenreDto): Observable<void> {
    return this.http.put<void>(`${this.apiBaseUrl}/Genre/${id}`, updateGenreDto);
  }

  deleteGenre(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiBaseUrl}/Genre/${id}`);
  }
}
