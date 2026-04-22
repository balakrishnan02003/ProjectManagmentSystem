using Microsoft.EntityFrameworkCore;
using PMS.Application.DTOs.Projects;
using PMS.Application.Interfaces;
using PMS.Domain.Entities;
using PMS.Infrastructure.Data;

namespace PMS.Infrastructure.Services;

public class ProjectService : IProjectService
{
    private readonly AppDbContext _context;

    public ProjectService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ProjectDto> CreateProjectAsync(CreateProjectDto dto)
    {
        // Use constructor (IMPORTANT)
        var project = new Project(dto.Name);

        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        return new ProjectDto
        {
            Id = project.Id,
            Name = project.Name,
            MemberCount = 0,
            TaskCount = 0
        };
    }

    public async Task<List<ProjectDto>> GetAllProjectsAsync()
    {
        return await _context.Projects
            .Include(p => p.Members)
            .Include(p => p.Tasks)
            .Select(p => new ProjectDto
            {
                Id = p.Id,
                Name = p.Name,
                MemberCount = p.Members.Count,
                TaskCount = p.Tasks.Count
            })
            .ToListAsync();
    }

    public async Task<ProjectDto?> GetProjectByIdAsync(Guid id)
    {
        var project = await _context.Projects
            .Include(p => p.Members)
            .Include(p => p.Tasks)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (project == null) return null;

        return new ProjectDto
        {
            Id = project.Id,
            Name = project.Name,
            MemberCount = project.Members.Count,
            TaskCount = project.Tasks.Count
        };
    }

    public async Task DeleteProjectAsync(Guid id)
    {
        var project = await _context.Projects.FindAsync(id);

        if (project == null) return;

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
    }
}