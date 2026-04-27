using Microsoft.AspNetCore.Mvc;
using PMS.Application.DTOs.Tasks;
using PMS.Application.Interfaces;

namespace PMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask(CreateTaskDto dto)
    {
        var result = await _taskService.CreateTaskAsync(dto);
        return Ok(result);
    }

    [HttpGet("project/{projectId}")]
    public async Task<IActionResult> GetTasksByProjectId(Guid projectId)
    {
        var result = await _taskService.GetTasksByProjectIdAsync(projectId);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(Guid id, UpdateTaskDto dto)
    {
        await _taskService.UpdateTaskAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(Guid id)
    {
        await _taskService.DeleteTaskAsync(id);
        return NoContent();
    }

    [HttpPost("{id}/start")]
    public async Task<IActionResult> StartTask(Guid id)
    {
        await _taskService.StartTaskAsync(id);
        return NoContent();
    }

    [HttpPost("{id}/complete")]
    public async Task<IActionResult> CompleteTask(Guid id)
    {
        await _taskService.CompleteTaskAsync(id);
        return NoContent();
    }

    [HttpPost("{id}/reopen")]
    public async Task<IActionResult> ReopenTask(Guid id)
    {
        await _taskService.ReopenTaskAsync(id);
        return NoContent();
    }
}