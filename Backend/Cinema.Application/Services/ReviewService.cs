using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cinema.Application.DTOs;
using Cinema.Application.DTOs.Revjew;
using Cinema.Domain.Entities;
using Cinema.Infrastructure.Repository;

namespace Cinema.Application.Services
{
    public class ReviewService
    {
        private readonly ReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewService(ReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReviewDto>> GetAllReviewsAsync()
        {
            var reviews = await _reviewRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
        }

        public async Task<ReviewDto> GetReviewByIdAsync(int id)
        {
            var review = await _reviewRepository.GetByIdAsync(id);
            if (review == null) throw new KeyNotFoundException($"Review with ID {id} not found.");

            return _mapper.Map<ReviewDto>(review);
        }

        public async Task<ReviewDto> CreateReviewAsync(CreateReviewDto createReviewDto)
        {
            var review = _mapper.Map<Review>(createReviewDto);
            review.ReviewDate = DateTime.UtcNow; // Set the review date explicitly

            await _reviewRepository.AddAsync(review);
            return _mapper.Map<ReviewDto>(review);
        }

        public async Task UpdateReviewAsync(int id, UpdateReviewDto updateReviewDto)
        {
            var existingReview = await _reviewRepository.GetByIdAsync(id);
            if (existingReview == null) throw new KeyNotFoundException($"Review with ID {id} not found.");

            _mapper.Map(updateReviewDto, existingReview);
            await _reviewRepository.UpdateAsync(existingReview);
        }

        public async Task DeleteReviewAsync(int id)
        {
            var review = await _reviewRepository.GetByIdAsync(id);
            if (review == null) throw new KeyNotFoundException($"Review with ID {id} not found.");

            await _reviewRepository.DeleteAsync(review);
        }
    }
}
