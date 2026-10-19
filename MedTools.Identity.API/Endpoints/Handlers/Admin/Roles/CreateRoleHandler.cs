using MedTools.Identity.API.Database.Repositories;
using MedTools.Identity.API.Common.Models.Requests;
using MedTools.Identity.API.Common.Extensions.Collections;

namespace MedTools.Identity.API.Endpoints.Handlers.Admin.Roles;

public static class CreateRoleHandler
{
    public static async Task<IResult> HandleAsync(
        CreateRoleRequest request,
        RoleRepository roleRepository,
        CancellationToken cancellationToken)
    {
        var validationResult = Validate(request);

        if (validationResult.Count > 0)
        {
            return TypedResults.ValidationProblem(validationResult);
        }

        var roleIdentifier = await roleRepository.AddRoleAsync(
            name: request.Name,
            cancellationToken: cancellationToken);

        return Results.CreatedAtRoute(
            routeName: "GetRoleById",
            routeValues: new { id = roleIdentifier });
    }

    private static Dictionary<string, string[]> Validate(
        CreateRoleRequest request)
    {
        var errors = new Dictionary<string, List<string>>();

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            errors.AddError(
                nameof(request.Name),
                "Имя роли не может быть пустым.");
        }

        return errors.ToDictionary(
            x => x.Key,
            x => x.Value.ToArray());
    }
}
