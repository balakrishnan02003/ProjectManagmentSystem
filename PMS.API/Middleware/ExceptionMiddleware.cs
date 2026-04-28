using System.Net;
using System.Text.Json;
using PMS.Infrastructure.Services;

namespace PMS.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context); // calls the next middleware in the pipeline
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        if (context.Response.HasStarted)
        {
            return Task.CompletedTask;
        }

        context.Response.ContentType = "application/json"; // ensure the response is in JSON format
        context.Response.StatusCode = ex switch
        {
            KeyNotFoundException => (int)HttpStatusCode.NotFound, // 404 Not Found
            ArgumentException => (int)HttpStatusCode.BadRequest,
            InvalidOperationException => (int)HttpStatusCode.BadRequest,
            ExternalApiService.ExternalApiException => (int)HttpStatusCode.BadGateway,
            _ => (int)HttpStatusCode.InternalServerError
        };

        var response = new
        {
            message = ex.Message
        };

        return context.Response.WriteAsync(JsonSerializer.Serialize(response)); // Convert the response object to JSON string
    }
}