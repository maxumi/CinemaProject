using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema.Application.DTOs.User;
using Cinema.Domain.Entities;
using Cinema.Infrastructure.Repository;

namespace Cinema.Application.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            if (!users.Any())
                throw new InvalidOperationException("No users found.");

            return users.Select(MapToUserDto);
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("User ID must be a positive integer.", nameof(id));

            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) throw new KeyNotFoundException($"User with ID {id} not found.");

            return MapToUserDto(user);
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
        {
            if (createUserDto == null) throw new ArgumentNullException(nameof(createUserDto));

            ValidateCreateUserDto(createUserDto);

            var user = new User
            {
                FirstName = createUserDto.FirstName,
                LastName = createUserDto.LastName,
                Email = createUserDto.Email,
                Password = createUserDto.Password,
                Role = createUserDto.Role
            };
            await _userRepository.AddAsync(user);

            return MapToUserDto(user);
        }

        public async Task<bool> UpdateUserAsync(int id, UpdateUserDto updateUserDto)
        {
            if (id != updateUserDto.Id) throw new ArgumentException("User ID mismatch.");

            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null) throw new KeyNotFoundException($"User with ID {id} not found.");

            UpdateUserFields(existingUser, updateUserDto);
            await _userRepository.UpdateAsync(existingUser);
            return true;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) throw new KeyNotFoundException($"User with ID {id} not found.");

            await _userRepository.DeleteAsync(user);
            return true;
        }

        // Helper methods
        private UserDto MapToUserDto(User user) => new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Role = user.Role
        };


        private void UpdateUserFields(User user, UpdateUserDto dto)
        {
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Email = dto.Email;
            user.Role = dto.Role;
        }

        private void ValidateCreateUserDto(CreateUserDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FirstName) || string.IsNullOrWhiteSpace(dto.LastName))
                throw new ArgumentException("User must have a valid first and last name.");
        }
    }
}
