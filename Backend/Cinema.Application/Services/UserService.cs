using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cinema.Application.DTOs.User;
using Cinema.Domain.Entities;
using Cinema.Infrastructure.Repository;

namespace Cinema.Application.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(UserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            if (!users.Any())
                throw new InvalidOperationException("No users found.");

            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("User ID must be a positive integer.", nameof(id));

            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) throw new KeyNotFoundException($"User with ID {id} not found.");

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
        {
            if (createUserDto == null) throw new ArgumentNullException(nameof(createUserDto));

            ValidateCreateUserDto(createUserDto);

            var user = _mapper.Map<User>(createUserDto);
            await _userRepository.AddAsync(user);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<bool> UpdateUserAsync(int id, UpdateUserDto updateUserDto)
        {
            if (id != updateUserDto.Id) throw new ArgumentException("User ID mismatch.");

            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null) throw new KeyNotFoundException($"User with ID {id} not found.");

            _mapper.Map(updateUserDto, existingUser);
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

        private void ValidateCreateUserDto(CreateUserDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FirstName) || string.IsNullOrWhiteSpace(dto.LastName))
                throw new ArgumentException("User must have a valid first and last name.");
        }
    }
}
