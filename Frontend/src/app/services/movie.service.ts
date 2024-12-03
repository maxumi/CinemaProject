import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Movie } from '../models/movie.models';
import { MovieSession } from '../models/movie-session.models';
import { CinemaHall } from '../models/cinema-hall.models';

@Injectable({
  providedIn: 'root',
})
export class MovieService {
  private baseUrl = environment.apiBaseUrl;

  constructor(private http: HttpClient) {}

  getFrontPage(moviesPage: number, moviesAmount: number, sessionsPage: number, sessionsAmount: number) {
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
    }>(`${this.baseUrl}/Movie/FrontPage`, { params });
  }
}