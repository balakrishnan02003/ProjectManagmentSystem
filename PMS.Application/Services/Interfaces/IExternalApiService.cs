using PMS.Application.DTOs.External;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMS.Application.Interfaces
{
    public interface IExternalApiService
    {
        Task<TodoDto> GetTodoAsync(int id);

        Task<List<TodoDto>> GetMultipleTodosAsync(List<int> ids);
    }
}
