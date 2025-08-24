using FluentValidation;
using System.Net;
using System.Text.Json;
using CartaoVacina.Domain.Exceptions;

namespace CartaoVacina.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        object response = new
        {
            error = new
            {
                message = exception.Message,
                details = (object?)null
            }
        };

        switch (exception)
        {
            case ValidationException validationEx:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                response = new
                {
                    error = new
                    {
                        message = "Validation failed",
                        details = validationEx.Errors.Select(e => new { Field = e.PropertyName, Error = e.ErrorMessage })
                    }
                };
                break;

            case DomainException:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;

            default:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response = new
                {
                    error = new
                    {
                        message = "Internal server error",
                        details = (object?)null
                    }
                };
                break;
        }

        var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(jsonResponse);
    }
}