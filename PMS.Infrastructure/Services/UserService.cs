using Microsoft.EntityFrameworkCore;
using PMS.Application.DTOs.Users;
using PMS.Application.Interfaces;
using PMS.Domain.Entities;
using PMS.Infrastructure.Data;

namespace PMS.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UserDto> CreateUserAsync(CreateUserDto dto)
    {
        var user = new User(dto.Name, dto.Email);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            ProjectCount = 0,
            AssignedTaskCount = 0
        };
    }

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        return await _context.Users
            .Include(u => u.ProjectMembers)
            .Include(u => u.AssignedTasks)
            .Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                ProjectCount = u.ProjectMembers.Count,
                AssignedTaskCount = u.AssignedTasks.Count
            })
            .ToListAsync();
    }

    public async Task<UserDto?> GetUserByIdAsync(Guid id)
    {
        var user = await _context.Users
            .Include(u => u.ProjectMembers)
            .Include(u => u.AssignedTasks)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null) return null;

        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            ProjectCount = user.ProjectMembers.Count,
            AssignedTaskCount = user.AssignedTasks.Count
        };
    }

    public async Task DeleteUserAsync(Guid id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null) return;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}