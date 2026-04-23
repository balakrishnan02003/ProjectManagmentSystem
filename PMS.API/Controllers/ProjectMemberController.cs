using Microsoft.AspNetCore.Mvc;
using PMS.Application.DTOs.ProjectMembers;
using PMS.Application.Interfaces;

namespace PMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectMemberController : ControllerBase
{
    private readonly IProjectMemberService _projectMemberService;

    public ProjectMemberController(IProjectMemberService projectMemberService)
    {
        _projectMemberService = projectMemberService;
    }

    // Add member to project
    [HttpPost]
    public async Task<IActionResult> AddMember(AddProjectMemberDto dto)
    {
        await _projectMemberService.AddMemberAsync(dto);
        return Ok();
    }

    // Get members of a project
    [HttpGet("project/{projectId}")]
    public async Task<IActionResult> GetMembersByProject(Guid projectId)
    {
        var result = await _projectMemberService.GetMembersByProjectIdAsync(projectId);
        return Ok(result);
    }

    // Remove member from project
    [HttpDelete]
    public async Task<IActionResult> RemoveMember(Guid userId, Guid projectId)
    {
        await _projectMemberService.RemoveMemberAsync(userId, projectId);
        return NoContent();
    }
}