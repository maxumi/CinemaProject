export interface CinemaHall {
    id: number;
    name: string;
    capacity: number;
    seatIds: number[];
  }
  export interface CreateCinemaHallDto {
    name: string;
    capacity: number;
    seatIds: number[];
  }
  export interface UpdateCinemaHallDto {
    name: string;
    capacity: number;
    seatIds: number[];
  }