using AutoMapper;
using Cinema.Application.DTOs.Booking;
using Cinema.Application.DTOs.CinemaHall;
using Cinema.Application.DTOs.CinemaHall.Seat;
using Cinema.Application.DTOs.Movie;
using Cinema.Application.DTOs.Movie.Genre;
using Cinema.Application.DTOs.MovieSession;
using Cinema.Application.DTOs.Revjew;
using Cinema.Application.DTOs.User;
using Cinema.Domain.Entities;

namespace Cinema.Application
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // CinemaHall mappings
            CreateMap<CinemaHall, CinemaHallDto>().ReverseMap();
            CreateMap<CinemaHall, CreateCinemaHallDto>().ReverseMap();
            CreateMap<CinemaHall, UpdateCinemaHallDto>().ReverseMap();

            // Movie mappings
            CreateMap<Movie, MovieDto>()
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(g => g.Name).ToList())); // Map genre names
            CreateMap<CreateMovieDto, Movie>()
                .ForMember(dest => dest.Genres, opt => opt.Ignore()); // Explicitly handle genres in service
            CreateMap<UpdateMovieDto, Movie>()
                .ForMember(dest => dest.Genres, opt => opt.Ignore()); // Explicitly handle genres in service

            // Genre for movies mappings
            CreateMap<Genre, GenreDto>().ReverseMap();
            CreateMap<CreateGenreDto, Genre>().ReverseMap();
            CreateMap<UpdateGenreDto, Genre>().ReverseMap();

            // MovieSession mappings
            CreateMap<MovieSession, MovieSessionDto>().ReverseMap();
            CreateMap<MovieSession, CreateMovieSessionDto>().ReverseMap();
            CreateMap<MovieSession, UpdateMovieSessionDto>().ReverseMap();

            // Booking mappings (renamed from Reservation)
            CreateMap<Booking, BookingDto>()
                .ForMember(dest => dest.SeatIds, opt => opt.MapFrom(src => src.Seats.Select(seat => seat.Id)));
            CreateMap<CreateBookingDto, Booking>()
                .ForMember(dest => dest.Seats, opt => opt.Ignore()); // Map seats explicitly when needed
            CreateMap<UpdateBookingDto, Booking>().ReverseMap();

            CreateMap<Seat, SeatDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.SeatNumber, opt => opt.MapFrom(src => src.SeatNumber))
                .ForMember(dest => dest.CinemaHallId, opt => opt.MapFrom(src => src.CinemaHallId));

            // Review mappings
            CreateMap<Review, ReviewDto>().ReverseMap();
            CreateMap<Review, CreateReviewDto>().ReverseMap();
            CreateMap<Review, UpdateReviewDto>().ReverseMap();

            // User mappings
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<CreateUserDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()); // Hash password explicitly
            CreateMap<UpdateUserDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()); // Password should not be updated via this DTO
            CreateMap<Movie, MovieTitlesDto>().ReverseMap();
        }
    }

}
