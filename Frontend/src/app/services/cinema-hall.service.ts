import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CinemaHall, CreateCinemaHallDto, UpdateCinemaHallDto } from '../models/cinema-hall.models';
import { environment } from '../../environments/environment';
import { MovieSession } from '../models/movie-session.models';

@Injectable({
  providedIn: 'root'
})
export class CinemaHallService {
  private baseUrl = environment.apiBaseUrl+"/CinemaHall";

  constructor(private http: HttpClient) {}

  getAllCinemaHalls() {
    return this.http.get<CinemaHall[]>(`${this.baseUrl}`);
  }

  getCinemaHallById(id: number) {
    return this.http.get<CinemaHall>(`${this.baseUrl}/${id}`);
  }

  createCinemaHall(dto: CreateCinemaHallDto) {
    return this.http.post<CinemaHall>(`${this.baseUrl}`, dto);
  }

  updateCinemaHall(id: number, dto: UpdateCinemaHallDto){
    return this.http.put<void>(`${this.baseUrl}/${id}`, dto);
  }

  deleteCinemaHall(id: number) {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}