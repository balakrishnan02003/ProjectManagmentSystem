using Microsoft.EntityFrameworkCore;
using PMS.Application.DTOs.Comments;
using PMS.Application.Interfaces;
using PMS.Domain.Entities;
using PMS.Infrastructure.Data;

namespace PMS.Infrastructure.Services;

public class CommentService : ICommentService
{
    private readonly AppDbContext _context;

    public CommentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CommentDto> CreateCommentAsync(CreateCommentDto dto)
    {
        // Check if task exists
        var task = await _context.TaskItems
            .FindAsync(dto.TaskItemId);

        if (task == null)
            throw new KeyNotFoundException("Task not found");

        // Create using constructor (IMPORTANT)
        var comment = new Comment(dto.Content, dto.TaskItemId);

        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();

        return new CommentDto
        {
            Id = comment.Id,
            Content = comment.Content,
            TaskItemId = comment.TaskItemId
        };
    }

    public async Task<List<CommentDto>> GetCommentsByTaskIdAsync(Guid taskId)
    {
        return await _context.Comments.AsNoTracking() // EF track entities by default. AsNoTracking reduce memory usage.
            .Where(c => c.TaskItemId == taskId) // get the comment by task item id matches
            .Select(c => new CommentDto
            {
                Id = c.Id,  
                Content = c.Content,
                TaskItemId = c.TaskItemId
            })
            .ToListAsync();
    }

    public async Task DeleteCommentAsync(Guid id)
    {
        var comment = await _context.Comments.FindAsync(id);

        if (comment == null) return;

        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
    }
}