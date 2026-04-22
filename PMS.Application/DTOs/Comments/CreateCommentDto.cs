namespace PMS.Application.DTOs.Comments;

public class CreateCommentDto
{
    public string Content { get; set; }
    public Guid TaskItemId { get; set; }
}