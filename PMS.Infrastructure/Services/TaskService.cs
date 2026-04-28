using Microsoft.EntityFrameworkCore;
using PMS.Application.DTOs.Tasks;
using PMS.Application.Interfaces;
using PMS.Domain.Entities;
using PMS.Infrastructure.Data;
using PMS.Application.Common;

namespace PMS.Infrastructure.Services;

public class TaskService : ITaskService
{
    private readonly AppDbContext _context;

    public TaskService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TaskDto> CreateTaskAsync(CreateTaskDto dto)
    {
        var projectExists = await _context.Projects
            .AnyAsync(p => p.Id == dto.ProjectId);

        if (!projectExists)
            throw new Exception($"Project {Constants.NotFound}");

        var task = new TaskItem(dto.Title, dto.Description, dto.ProjectId, dto.DueDate);

        if (dto.AssignedUserId.HasValue)
        {
            var user = await _context.Users
                .FindAsync(dto.AssignedUserId.Value);

            if (user == null)
                throw new Exception($"Assigned user {Constants.NotFound}");

            task.AssignUser(user);
        }

        _context.TaskItems.Add(task);
        await _context.SaveChangesAsync();

        return new TaskDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            ProjectId = task.ProjectId,
            AssignedUserId = task.AssignedUserId,
            DueDate = task.DueDate,
            CommentCount = 0
        };
    }

    public async Task UpdateTaskAsync(Guid id, UpdateTaskDto dto)
    {
        var task = await _context.TaskItems.FindAsync(id);

        if (task == null)
            throw new KeyNotFoundException($"Task {Constants.NotFound}");

        task.UpdateDetails(dto.Title, dto.Description, dto.DueDate);

        if (dto.AssignedUserId.HasValue)
        {
            var user = await _context.Users.FindAsync(dto.AssignedUserId.Value);

            if (user == null)
                throw new Exception($"User {Constants.NotFound}");

            task.AssignUser(user);
        }

        await _context.SaveChangesAsync();
    }
    public async Task<List<TaskDto>> GetTasksByProjectIdAsync(Guid projectId)
    {
        return await _context.TaskItems
            .Where(t => t.ProjectId == projectId)
            .Select(t => new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                ProjectId = t.ProjectId,
                AssignedUserId = t.AssignedUserId,
                AssignedUserName = t.AssignedUser != null ? t.AssignedUser.Name : null,
                DueDate = t.DueDate,
                CommentCount = t.Comments.Count
            })
            .ToListAsync();
    }
    public async Task DeleteTaskAsync(Guid id)
    {
        var task = await _context.TaskItems.FindAsync(id);

        if (task == null) return;

        _context.TaskItems.Remove(task);
        await _context.SaveChangesAsync();
    }

    public async Task StartTaskAsync(Guid id)
    {
        var task = await _context.TaskItems.FindAsync(id);

        if (task == null)
            throw new Exception($"Task {Constants.NotFound}");

        task.Start(); 

        await _context.SaveChangesAsync();
    }

    public async Task CompleteTaskAsync(Guid id)
    {
        var task = await _context.TaskItems.FindAsync(id);

        if (task == null)
            throw new KeyNotFoundException($"Task {Constants.NotFound}");

        task.Complete(); 

        await _context.SaveChangesAsync();
    }

    public async Task ReopenTaskAsync(Guid id)
    {
        var task = await _context.TaskItems.FindAsync(id);

        if (task == null)
            throw new KeyNotFoundException($"Task {Constants.NotFound}");

        task.Reopen(); 

        await _context.SaveChangesAsync();
    }
}