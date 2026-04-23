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

    //Create Task
    [HttpPost]
    public async Task<IActionResult> CreateTask(CreateTaskDto dto)
    {
        var result = await _taskService.CreateTaskAsync(dto);
        return Ok(result);
    }

    //Get Tasks by Project
    [HttpGet("project/{projectId}")]
    public async Task<IActionResult> GetTasksByProjectId(Guid projectId)
    {
        var result = await _taskService.GetTasksByProjectIdAsync(projectId);
        return Ok(result);
    }

    //Update Task
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(Guid id, UpdateTaskDto dto)
    {
        await _taskService.UpdateTaskAsync(id, dto);
        return NoContent();
    }

    //Delete Task
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(Guid id)
    {
        await _taskService.DeleteTaskAsync(id);
        return NoContent();
    }
}