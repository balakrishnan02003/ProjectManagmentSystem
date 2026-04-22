namespace PMS.Application.Interfaces;

using PMS.Application.DTOs.Users;

public interface IUserService
{
    Task<UserDto> CreateUserAsync(CreateUserDto dto);

    Task<List<UserDto>> GetAllUsersAsync();

    Task<UserDto?> GetUserByIdAsync(Guid id);

    Task DeleteUserAsync(Guid id);
}