namespace PMS.Application.DTOs.Comments;

public class CommentDto
{
    public Guid Id { get; set; }
    public string Content { get; set; }

    public Guid TaskItemId { get; set; }
}