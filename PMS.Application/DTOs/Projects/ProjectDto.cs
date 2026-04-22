namespace PMS.Application.DTOs.Projects;

public class ProjectDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    // Optional (can add later)
    public int MemberCount { get; set; }
    public int TaskCount { get; set; }
}