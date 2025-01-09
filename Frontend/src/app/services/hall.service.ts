import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CinemaHall, CreateCinemaHallDto, UpdateCinemaHallDto } from '../models/cinema-hall.models';
import { Seat } from '../models/seat.models';

@Injectable({
  providedIn: 'root'
})
export class HallService {
  private baseUrl = '/api/CinemaHall';

  constructor(private http: HttpClient) {}

  getHalls() {
    return this.http.get<CinemaHall[]>(this.baseUrl);
  }

  getSeatsByHallId(hallId: number) {
    return this.http.get<Seat[]>(`${this.baseUrl}/${hallId}/seats`);
  }

  getHallById(id: number){
    return this.http.get<CinemaHall>(`${this.baseUrl}/${id}`);
  }

  createHall(hall: CreateCinemaHallDto) {
    return this.http.post<CinemaHall>(this.baseUrl, hall);
  }

  updateHall(id: number, hall: UpdateCinemaHallDto) {
    return this.http.put<void>(`${this.baseUrl}/${id}`, hall);
  }

  deleteHall(id: number) {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}