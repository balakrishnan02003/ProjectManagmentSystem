using Microsoft.AspNetCore.Mvc;
using PMS.Application.Interfaces;

namespace PMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExternalController : ControllerBase
    {
        private readonly IExternalApiService _externalApiService;

        public ExternalController(IExternalApiService externalApiService)
        {
            _externalApiService = externalApiService;
        }

        [HttpGet("external/{id}")]
        public async Task<IActionResult> GetExternalTodo(int id)
        {
            var result = await _externalApiService.GetTodoAsync(id);
            return Ok(result);
        }

        [HttpGet("multiple")]
        public async Task<IActionResult> GetMultipleExternalTodos([FromQuery] List<int> ids)
        {
            var result = await _externalApiService.GetMultipleTodosAsync(ids);
            return Ok(result);
        }
    }
}
