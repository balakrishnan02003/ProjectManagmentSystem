namespace PMS.Application.DTOs.Tasks;

public class TaskDto
{
    public Guid Id { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }

    public Guid ProjectId { get; set; }

    public Guid? AssignedUserId { get; set; }
    public string? AssignedUserName { get; set; }

    public DateTime DueDate { get; set; }

    public int CommentCount { get; set; }
}