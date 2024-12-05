export interface Movie {
    id: number;
    title: string;
    durationMinutes: number;
    releaseDate: Date;
    description: string;
    genres: string[];
  }
  
  export interface CreateMovieDto {
    title: string;
    durationMinutes: number;
    releaseDate: Date;
    description: string;
    genreIds: number[];
  }
  
  export interface UpdateMovieDto {
    title: string;
    durationMinutes: number;
    releaseDate: Date;
    description: string;
    genreIds: number[];
  }
  
  export interface Genre {
    id: number;
    name: string;
  }
  
  export interface CreateGenreDto {
    name: string;
  }
  
  export interface UpdateGenreDto {
    name: string;
  }
  