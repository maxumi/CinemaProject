import { Injectable } from '@angular/core';
import { Booking, CreateBookingDto, UpdateBookingDto } from '../models/bookings.models';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BookingService {
  private baseUrl = environment.apiBaseUrl+"/Booking";

  constructor(private http: HttpClient) {}

  getBookings() {
    return this.http.get<Booking[]>(this.baseUrl);
  }

  getBookingById(id: number) {
    return this.http.get<Booking>(`${this.baseUrl}/${id}`);
  }

  createBooking(booking: CreateBookingDto) {
    return this.http.post<Booking>(this.baseUrl, booking);
  }

  updateBooking(id: number, booking: UpdateBookingDto) {
    return this.http.put<void>(`${this.baseUrl}/${id}`, booking);
  }

  deleteBooking(id: number) {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
