using Microsoft.AspNetCore.Mvc;
using PMS.Application.DTOs.Comments;
using PMS.Application.Interfaces;

namespace PMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateComment(CreateCommentDto dto)
    {
        var result = await _commentService.CreateCommentAsync(dto);
        return Ok(result);
    }

    [HttpGet("task/{taskId}")]
    public async Task<IActionResult> GetCommentsByTaskId(Guid taskId)
    {
        var result = await _commentService.GetCommentsByTaskIdAsync(taskId);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment(Guid id)
    {
        await _commentService.DeleteCommentAsync(id);
        return NoContent();
    }
}