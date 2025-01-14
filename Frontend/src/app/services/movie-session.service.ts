import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CreateMovieSessionDto, MovieSession, UpdateMovieSessionDto } from '../models/movie-session.models';
import { environment } from '../../environments/environment';
import { Seat } from '../models/seat.models';

@Injectable({
  providedIn: 'root'
})
export class MovieSessionService {

  private baseUrl = environment.apiBaseUrl+"/MovieSession";

  constructor(private http: HttpClient) { }

  getSeatsBySessionId(sessionId: number) {
    return this.http.get<Seat[]>(`${this.baseUrl}/${sessionId}/available-seats`);
  }

  getAllMovieSessions() {
    return this.http.get<MovieSession[]>(`${this.baseUrl}`);
  }

  getMovieSessionsByMovieId(movieId:number){
    return this.http.get<MovieSession[]>(`${this.baseUrl}/movie/${movieId}`);
  }

  getMovieSessionById(id: number) {
    return this.http.get<MovieSession>(`${this.baseUrl}/${id}`);
  }

  createMovieSession(dto: CreateMovieSessionDto){
    return this.http.post<MovieSession>(`${this.baseUrl}`, dto);
  }

  updateMovieSession(id: number, dto: UpdateMovieSessionDto) {
    return this.http.put<void>(`${this.baseUrl}/${id}`, dto);
  }

  deleteMovieSession(id: number) {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}