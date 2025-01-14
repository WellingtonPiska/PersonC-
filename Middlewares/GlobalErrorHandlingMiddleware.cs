using System.Data;
using System.Net;
using System.Text.Json;
using Person.API.Exceptions;

namespace Person.API.Middlewares;

public class GlobalErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionsAsync(context, ex);
        }
    }

    private static Task HandleExceptionsAsync(HttpContext context, Exception exception)
    {
        HttpStatusCode status;
        string stackTrace;
        string mensagem;
        
        var exceptionType = exception.GetType();
        if (exceptionType == typeof(DBConcurrencyException))
        {
            mensagem = exception.Message;
            status = HttpStatusCode.BadRequest;
            stackTrace = exception.StackTrace ?? string.Empty;
        }
        else if (exceptionType == typeof(DuplicatePersonNameException))
        {
            mensagem = exception.Message;
            status = HttpStatusCode.Conflict;
            stackTrace = exception.StackTrace ?? string.Empty;
        }
        else if (exceptionType == typeof(NotFoundException))
        {
            mensagem = exception.Message;
            status = HttpStatusCode.NotFound;
            stackTrace = exception.StackTrace ?? string.Empty;
        }
        else
        {
            mensagem = exception.Message;
            status = HttpStatusCode.InternalServerError;
            stackTrace = exception.StackTrace ?? string.Empty;
        }
       

        var result = JsonSerializer.Serialize(new { status, mensagem, stackTrace });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)status;
        return context.Response.WriteAsync(result);
    }
}