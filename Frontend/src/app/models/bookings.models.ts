export interface Booking {
    id: number;
    userId: number;
    numberOfTickets: number;
    movieSessionId: number;
    paymentDetailId: number;
    seatIds: number[];
  }

  export interface CreateBookingDto {
    userId: number;
    numberOfTickets: number;
    movieSessionId: number;
    paymentDetailId: number;
    seatIds: number[];
  }

  export interface UpdateBookingDto {
    numberOfTickets: number;
    movieSessionId: number;
    paymentDetailId: number;
    seatIds: number[];
  }