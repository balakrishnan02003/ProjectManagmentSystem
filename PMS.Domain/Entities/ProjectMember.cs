namespace PMS.Domain.Entities;
// User -> ProjectMember -> Project
public class ProjectMember
{
    public Guid UserId { get; private set; }
    public User User { get; private set; }

    public Guid ProjectId { get; private set; }
    public Project Project { get; private set; }

    public string Role { get; private set; } // Admin / Member

    public ProjectMember(Guid userId, Guid projectId, string role)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("UserId is required");

        if (projectId == Guid.Empty)
            throw new ArgumentException("ProjectId is required");

        if (string.IsNullOrWhiteSpace(role))
            throw new ArgumentException("Role is required");

        UserId = userId;
        ProjectId = projectId;
        Role = role;
    }

    public void ChangeRole(string role)
    {
        if (string.IsNullOrWhiteSpace(role))
            throw new ArgumentException("Role cannot be empty");

        Role = role;
    }
}
