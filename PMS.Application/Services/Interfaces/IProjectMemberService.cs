namespace PMS.Application.Interfaces;

using PMS.Application.DTOs.ProjectMembers;

public interface IProjectMemberService
{
    Task AddMemberAsync(AddProjectMemberDto dto);

    Task RemoveMemberAsync(Guid userId, Guid projectId);

    Task<List<ProjectMemberDto>> GetMembersByProjectIdAsync(Guid projectId);
}