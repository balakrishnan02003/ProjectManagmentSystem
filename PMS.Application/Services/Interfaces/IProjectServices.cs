namespace PMS.Application.Interfaces;

using PMS.Application.DTOs.Projects;

public interface IProjectService
{
    Task<ProjectDto> CreateProjectAsync(CreateProjectDto dto);

    Task<List<ProjectDto>> GetAllProjectsAsync();

    Task<ProjectDto?> GetProjectByIdAsync(Guid id);

    Task DeleteProjectAsync(Guid id);
}