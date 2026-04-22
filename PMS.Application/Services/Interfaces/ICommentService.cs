namespace PMS.Application.Interfaces;

using PMS.Application.DTOs.Comments;

public interface ICommentService
{
    Task<CommentDto> CreateCommentAsync(CreateCommentDto dto);

    Task<List<CommentDto>> GetCommentsByTaskIdAsync(Guid taskId);

    Task DeleteCommentAsync(Guid id);
}