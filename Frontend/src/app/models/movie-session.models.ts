export interface MovieSession {
    id: number;
    movieId: number;
    cinemaHallId: number;
    startTime: Date;
    endTime: Date;
    price: number;
  }
  
  export interface CreateMovieSessionDto {
    movieId: number;
    cinemaHallId: number;
    startTime: Date;
    endTime: Date;
    price: number;
  }

  export interface UpdateMovieSessionDto {
    movieId: number;
    cinemaHallId: number;
    startTime: Date;
    endTime: Date;
    price: number;
  }
  