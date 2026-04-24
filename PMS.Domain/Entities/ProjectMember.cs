using PMS.Domain.Enums;

namespace PMS.Domain.Entities;
//TODO:User -> ProjectMember -> Project
public class ProjectMember
{
    public Guid UserId { get; private set; }
    public User User { get; private set; }

    public Guid ProjectId { get; private set; }
    public Project Project { get; private set; }

    public ProjectRole Role { get; private set; }

    public ProjectMember(Guid userId, Guid projectId, ProjectRole role)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("UserId is required");

        if (projectId == Guid.Empty)
            throw new ArgumentException("ProjectId is required");

        UserId = userId;
        ProjectId = projectId;
        Role = role;
    }

    public void ChangeRole(ProjectRole role)
    {
        Role = role;
    }
}
