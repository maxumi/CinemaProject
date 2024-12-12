import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Movie, CreateMovieDto, UpdateMovieDto, Genre, MovieItem } from '../models/movie.models';
import { MovieSession } from '../models/movie-session.models';
import { CinemaHall } from '../models/cinema-hall.models';

@Injectable({
  providedIn: 'root',
})
export class MovieService {
  private movieUrl = environment.apiBaseUrl+"/Movie";
  private genreUrl = environment.apiBaseUrl+"/Genre";

  constructor(private http: HttpClient) {}

  getMovieTitles() {
    return this.http.get<MovieItem[]>(`${this.movieUrl}/Titles`);
  }

  getMovieById(id: number){
    return this.http.get<Movie>(`${this.movieUrl}/${id}`);
  }

  createMovie(movie: CreateMovieDto) {
    return this.http.post<Movie>(`${this.movieUrl}`, movie);
  }

  updateMovie(id: number, movie: UpdateMovieDto): Observable<void> {
    return this.http.put<void>(`${this.movieUrl}/${id}`, movie);
  }

  deleteMovie(id: number) {
    return this.http.delete<void>(`${this.movieUrl}/${id}`);
  }

  getGenres() {
    return this.http.get<Genre[]>(`${this.genreUrl}`);
  }

  createGenre(genre: { name: string }) {
    return this.http.post<Genre>(`${this.movieUrl}/genres`, genre);
  }

  updateGenre(id: number, genre: { name: string }){
    return this.http.put<Genre>(`${this.movieUrl}/genres/${id}`, genre);
  }

  deleteGenre(id: number){
    return this.http.delete<void>(`${this.movieUrl}/genres/${id}`);
  }

  getFrontPage(
    moviesPage: number,
    moviesAmount: number,
    sessionsPage: number,
    sessionsAmount: number
  ) {
    const params = {
      moviesPage: moviesPage.toString(),
      moviesAmount: moviesAmount.toString(),
      sessionsPage: sessionsPage.toString(),
      sessionsAmount: sessionsAmount.toString(),
    };
    return this.http.get<{
      movies: { items: Movie[]; hasMore: boolean };
      movieSessions: { items: MovieSession[]; hasMore: boolean };
      cinemaHalls: CinemaHall[];
    }>(`${this.movieUrl}/FrontPage`, { params });
  }
}
