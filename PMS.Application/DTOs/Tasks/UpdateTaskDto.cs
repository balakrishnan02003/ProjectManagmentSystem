namespace PMS.Application.DTOs.Tasks;

public class UpdateTaskDto
{
    public string Title { get; set; }
    public string Description { get; set; }

    public Guid? AssignedUserId { get; set; }
    public DateTime DueDate { get; set; }
}