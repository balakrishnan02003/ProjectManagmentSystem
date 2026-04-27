using PMS.Domain.Enums;

namespace PMS.Application.DTOs.ProjectMembers;

public class AddProjectMemberDto
{
    public Guid UserId { get; set; }
    public Guid ProjectId { get; set; }
    public ProjectRole Role { get; set; } // Admin / Member
}