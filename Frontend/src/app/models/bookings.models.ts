export interface Booking {
    id: number;
    userId: number;
    numberOfTickets: number;
    movieSessionId: number;
    paymentDetailId: number;
    seatIds: number[];
  }

  // Simple but useless for now.
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

  export enum PaymentMethod {
    Online,
    OnPremise
  }
  export interface PaymentDetail {
    amount: number; // Total payment amount
    method: PaymentMethod
    date: Date;
  }
  
  export interface CreateDetailedBooking {
    userId: number;
    numberOfTickets: number;
    movieSessionId: number;
    seatIds: number[];
    totalAmount: number;
    bookingDate: Date;
    paymentDetail: PaymentDetail;
  }