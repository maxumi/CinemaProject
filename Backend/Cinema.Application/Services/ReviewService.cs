using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema.Application.DTOs;
using Cinema.Application.DTOs.Revjew;
using Cinema.Domain.Entities;
using Cinema.Infrastructure.Repository;

namespace Cinema.Application.Services
{
    public class ReviewService
    {
        private readonly ReviewRepository _reviewRepository;

        public ReviewService(ReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<IEnumerable<ReviewDto>> GetAllReviewsAsync()
        {
            var reviews = await _reviewRepository.GetAllAsync();
            return reviews.Select(MapToReviewDto);
        }

        public async Task<ReviewDto> GetReviewByIdAsync(int id)
        {
            var review = await _reviewRepository.GetByIdAsync(id);
            if (review == null) throw new KeyNotFoundException($"Review with ID {id} not found.");

            return MapToReviewDto(review);
        }

        public async Task<ReviewDto> CreateReviewAsync(CreateReviewDto createReviewDto)
        {
            var review = MapToReview(createReviewDto);
            await _reviewRepository.AddAsync(review);
            return MapToReviewDto(review);
        }

        public async Task UpdateReviewAsync(int id, UpdateReviewDto updateReviewDto)
        {
            var existingReview = await _reviewRepository.GetByIdAsync(id);
            if (existingReview == null) throw new KeyNotFoundException($"Review with ID {id} not found.");

            existingReview.Comment = updateReviewDto.Comment;
            existingReview.Rating = updateReviewDto.Rating;

            await _reviewRepository.UpdateAsync(existingReview);
        }

        public async Task DeleteReviewAsync(int id)
        {
            var review = await _reviewRepository.GetByIdAsync(id);
            if (review == null) throw new KeyNotFoundException($"Review with ID {id} not found.");

            await _reviewRepository.DeleteAsync(review);
        }

        // Mapping methods
        private ReviewDto MapToReviewDto(Review review) => new ReviewDto
        {
            Id = review.Id,
            MovieId = review.MovieId,
            UserId = review.UserId,
            Comment = review.Comment,
            Rating = review.Rating,
            ReviewDate = review.ReviewDate
        };

        private Review MapToReview(CreateReviewDto dto) => new Review
        {
            MovieId = dto.MovieId,
            UserId = dto.UserId,
            Comment = dto.Comment,
            Rating = dto.Rating,
            ReviewDate = DateTime.UtcNow
        };
    }
}
