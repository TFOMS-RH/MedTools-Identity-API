using MedTools.Identity.API.Common.Extensions;
using MedTools.Identity.API.Database.Repositories;
using MedTools.Identity.API.Common.Models.Requests;

namespace MedTools.Identity.API.Endpoints.Handlers.Permissions;

public static class CreatePermissionHandler
{
    public static async Task<IResult> HandleAsync(
        CreatePermissionRequest request,
        PermissionRepository permissionRepository,
        CancellationToken cancellationToken)
    {
        var validationResult = Validate(request);

        if (validationResult.Count > 0)
        {
            return TypedResults.ValidationProblem(
                validationResult);
        }

        var permissionIdentifier = await permissionRepository.AddPermissionAsync(
            code: request.Code,
            name: request.Name,
            description: request.Description,
            cancellationToken: cancellationToken);

        return Results.CreatedAtRoute(
            routeName: "GetPermissionById",
            routeValues: new { id = permissionIdentifier });

    }

    private static Dictionary<string, string[]> Validate(
        CreatePermissionRequest request)
    {
        var errors = new Dictionary<string, List<string>>();

        if (string.IsNullOrWhiteSpace(request.Code))
        {
            errors.AddError(
                nameof(request.Code),
                "Код пустой либо содержит только пробелы.");
        }

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            errors.AddError(
                nameof(request.Name),
                "Имя разрешения не может быть null или содержать пробелы.");
        }

        if (request.Description != null && string.IsNullOrWhiteSpace(request.Description))
        {
            errors.AddError(
                nameof(request.Description),
                "Описание разрешения не должно содержать одних пробелов.");
        }

        return errors.ToDictionary(
            x => x.Key,
            x => x.Value.ToArray());
    }
}
