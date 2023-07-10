using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using GameLibrary.Domain.Exceptions;

namespace GameLibrary.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception error)
        {
            switch (error)
            {
                case GameException e:
                    await HandleExceptionAsync(httpContext, HttpStatusCode.BadRequest, e.Message,
                        "Problems with game creating, changing or deleting");
                    break;
                case Exception:

                    await HandleExceptionAsync(httpContext, HttpStatusCode.BadRequest, error.Message,
                        "Unhandled error");
                    break;
            }
        }
    }

    public async Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode,
        string exceptionMessage, string errorTitle)
    {
        var responce = context.Response;
        responce.StatusCode = (int)statusCode;
        var problemDetails = new ProblemDetails
        {
            Status = (int)statusCode,
            Detail = exceptionMessage,
            Title = errorTitle
        };
        var body = JsonSerializer.Serialize(problemDetails);
        var data = Encoding.UTF8.GetBytes(body);
        responce.ContentType = "application/json";
        await responce.Body.WriteAsync(data);
    }
}