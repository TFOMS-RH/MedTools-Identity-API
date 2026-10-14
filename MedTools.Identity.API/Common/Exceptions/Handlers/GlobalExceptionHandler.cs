using Microsoft.AspNetCore.Diagnostics;

using MedTools.Identity.API.Common.Exceptions.Models;

namespace MedTools.Identity.API.Common.Exceptions.Handlers;

public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {

        var (statusCode, title, detail) = exception switch
        {
            DuplicateEntityException => (
                StatusCodes.Status409Conflict,
                "Конфликт данных",
                exception.Message),

            UserNotFoundException => (
                StatusCodes.Status404NotFound,
                "Ресурс не найден",
                exception.Message),

            BadHttpRequestException ex => (
                ex.StatusCode,
                "Некорректный запрос",
                ex.Message),

            _ => (
                StatusCodes.Status500InternalServerError,
                "Внутренняя ошибка сервера",
                "При обработке запросы произошла непредвиденная ошибка.")
        };

        if (statusCode == StatusCodes.Status500InternalServerError)
        {
            logger.LogError(exception, "Необработанное исключение при выполнение запроса");
        }

        await Results.Problem(
            statusCode: statusCode,
            title: title,
            detail: detail)
        .ExecuteAsync(httpContext);

        return true;
    }
}