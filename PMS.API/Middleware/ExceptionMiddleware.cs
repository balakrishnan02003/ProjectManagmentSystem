//using System.Net;
//using System.Text.Json;

//namespace PMS.API.Middleware;

//public class ExceptionMiddleware
//{
//    private readonly RequestDelegate _next;

//    public ExceptionMiddleware(RequestDelegate next)
//    {
//        _next = next;
//    }

//    public async Task InvokeAsync(HttpContext context)
//    {
//        try
//        {
//            await _next(context);
//        }
//        catch (Exception ex)
//        {
//            await HandleExceptionAsync(context, ex);
//        }
//    }

//    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
//    {
//        context.Response.ContentType = "application/json";

//        context.Response.StatusCode = ex switch
//        {
//            _ => (int)HttpStatusCode.BadRequest
//        };

//        var response = new
//        {
//            message = ex.Message
//        };

//        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
//    }
//}