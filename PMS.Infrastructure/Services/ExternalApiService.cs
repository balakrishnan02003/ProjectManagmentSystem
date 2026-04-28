using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using PMS.Application.DTOs.External;
using PMS.Application.Interfaces;

namespace PMS.Infrastructure.Services
{
    public class ExternalApiService : IExternalApiService
    {
        public class ExternalApiException : Exception // Custom exception for external API errors
        {
            public ExternalApiException(string message) : base(message) { }
        }

        private readonly HttpClient _httpClient;

        public ExternalApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TodoDto> GetTodoAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://jsonplaceholder.typicode.com/todos/{id}");

                if (!response.IsSuccessStatusCode) // Errors like, 404 or 500
                {
                    throw new ExternalApiException($"API error: {response.StatusCode}");
                }

                var content = await response.Content.ReadAsStringAsync();

                // Convert JSON to C sharp object (TodoDto)
                // Use case-insensitive deserialization to handle potential casing issues in JSON properties
                var result = JsonSerializer.Deserialize<TodoDto>(
                    content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );

                return result ?? throw new ExternalApiException("Invalid API response"); // Handle null case and unexpected response formats.
            }
            catch (HttpRequestException) // Network-related errors (no internet, DNS failure, etc.)
            {
                throw new ExternalApiException("Network error while calling external API");
            }
            catch (TaskCanceledException) // Timeout errors
            {
                throw new ExternalApiException("External API timeout");
            }
        }

        public async Task<List<TodoDto>> GetMultipleTodosAsync(List<int> ids)
        {
            var tasks = ids.Select(id => GetTodoAsync(id)).ToList();

            try
            {
                var results = await Task.WhenAll(tasks);
                return results.ToList();
            }
            catch (Exception ex)
            {
                throw new ExternalApiException($"One or more external API calls failed: {ex.Message}");
            }
        }
    }
}