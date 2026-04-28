using Microsoft.EntityFrameworkCore;
using PMS.Application.DTOs.ProjectMembers;
using PMS.Application.Interfaces;
using PMS.Domain.Entities;
using PMS.Infrastructure.Data;
using PMS.Application.Common;

namespace PMS.Infrastructure.Services;

public class ProjectMemberService : IProjectMemberService
{
    private readonly AppDbContext _context;

    public ProjectMemberService(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddMemberAsync(AddProjectMemberDto dto)
    {
        // Validate user
        var user = await _context.Users.FindAsync(dto.UserId);
        if (user == null)
            throw new KeyNotFoundException($"User {Constants.NotFound}");

        // Load project with members
        var project = await _context.Projects
            .Include(p => p.Members)
            .FirstOrDefaultAsync(p => p.Id == dto.ProjectId);

        if (project == null)
            throw new KeyNotFoundException($"Project {Constants.NotFound}");

        // Create using constructor
        var member = new ProjectMember(dto.UserId, dto.ProjectId, dto.Role);

        // Use domain method
        project.AddMember(member);

        await _context.SaveChangesAsync();
    }

    public async Task RemoveMemberAsync(Guid userId, Guid projectId)
    {
        var member = await _context.ProjectMembers
            .FirstOrDefaultAsync(pm => pm.UserId == userId && pm.ProjectId == projectId);

        if (member == null)
            throw new KeyNotFoundException($"Project member {Constants.NotFound}");

        _context.ProjectMembers.Remove(member);
        await _context.SaveChangesAsync();
    }

    public async Task<List<ProjectMemberDto>> GetMembersByProjectIdAsync(Guid projectId)
    {
        return await _context.ProjectMembers
            .Where(pm => pm.ProjectId == projectId)
            .Include(pm => pm.User)
            .Select(pm => new ProjectMemberDto
            {
                UserId = pm.UserId,
                UserName = pm.User.Name,
                Role = pm.Role
            })
            .ToListAsync();
    }
}