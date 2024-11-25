using AutoMapper;
using Cinema.Application.DTOs.CinemaHall;
using Cinema.Application.DTOs.Movie;
using Cinema.Application.DTOs.MovieSession;
using Cinema.Application.DTOs.Reservation;
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
            CreateMap<Movie, MovieDto>().ReverseMap();
            CreateMap<Movie, CreateMovieDto>().ReverseMap();
            CreateMap<Movie, UpdateMovieDto>().ReverseMap();

            // MovieSession mappings
            CreateMap<MovieSession, MovieSessionDto>().ReverseMap();
            CreateMap<MovieSession, CreateMovieSessionDto>().ReverseMap();
            CreateMap<MovieSession, UpdateMovieSessionDto>().ReverseMap();

            // Reservation mappings
            CreateMap<Reservation, ReservationDto>()
                .ForMember(dest => dest.SeatIds, opt => opt.MapFrom(src => src.Seats.Select(seat => seat.Id)));
            CreateMap<CreateReservationDto, Reservation>()
                .ForMember(dest => dest.Seats, opt => opt.Ignore());
            CreateMap<Reservation, UpdateReservationDto>().ReverseMap();

            // Review mappings
            CreateMap<Review, ReviewDto>().ReverseMap();
            CreateMap<Review, CreateReviewDto>().ReverseMap();
            CreateMap<Review, UpdateReviewDto>().ReverseMap();

            // User mappings
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<CreateUserDto, User>().ReverseMap();
            CreateMap<UpdateUserDto, User>().ReverseMap();
        }
    }

}
