using Microsoft.AspNetCore.Mvc;
using PMS.Application.DTOs.Projects;
using PMS.Application.Interfaces;

namespace PMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject(CreateProjectDto dto)
    {
        var result = await _projectService.CreateProjectAsync(dto);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProject(Guid id, UpdateProjectDto dto)
    {
        await _projectService.UpdateProjectAsync(id, dto);
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProjects()
    {
        var result = await _projectService.GetAllProjectsAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProjectById(Guid id)
    {
        var result = await _projectService.GetProjectByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(Guid id)
    {
        await _projectService.DeleteProjectAsync(id);
        return NoContent();
    }
}