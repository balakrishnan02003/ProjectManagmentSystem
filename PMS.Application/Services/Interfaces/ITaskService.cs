namespace PMS.Application.Interfaces;

using PMS.Application.DTOs.Tasks;

public interface ITaskService
{
    Task<TaskDto> CreateTaskAsync(CreateTaskDto dto);

    Task<List<TaskDto>> GetTasksByProjectIdAsync(Guid projectId);

    Task UpdateTaskAsync(Guid id, UpdateTaskDto dto);

    Task DeleteTaskAsync(Guid id);

    Task StartTaskAsync(Guid id);

    Task CompleteTaskAsync(Guid id);

    Task ReopenTaskAsync(Guid id);
}