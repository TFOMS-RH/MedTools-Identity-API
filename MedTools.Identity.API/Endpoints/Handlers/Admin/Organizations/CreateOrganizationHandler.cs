using MedTools.Identity.API.Database.Repositories;
using MedTools.Identity.API.Common.Models.Requests;

namespace MedTools.Identity.API.Endpoints.Handlers.Admin.Organizations;

public static class CreateOrganizationHandler
{
    public static async Task<IResult> HandleAsync(
        CreateOrganizationRequest request,
        OrganizationRepository organizationRepository,
        CancellationToken cancellationToken)
    {
        var validationResult = Validate(request);

        if (validationResult.Count > 0)
        {
            return TypedResults.ValidationProblem(validationResult);
        }

        var organizationIdentifier = await organizationRepository.CreateOrganizationAsync(
            code: request.Code,
            type: request.Type,
            name: request.Name,
            cancellationToken: cancellationToken);

        return Results.CreatedAtRoute(
            routeName: "GetOrganizationById",
            routeValues: new { id = organizationIdentifier });

    }

    private static Dictionary<string, string[]> Validate(CreateOrganizationRequest request)
    {
        var errors = new Dictionary<string, List<string>>();

        if (string.IsNullOrWhiteSpace(request.Code))
        {
            AddError(errors, nameof(request.Code), "Код организации пустой либо содержит только пробелы.");
        }

        return errors.ToDictionary(
            x => x.Key,
            x => x.Value.ToArray());
    }

    private static void AddError(
        Dictionary<string, List<string>> errors,
        string field,
        string message)
    {
        if (!errors.TryGetValue(field, out var messages))
        {
            messages = [];
            errors[field] = messages;
        }

        messages.Add(message);
    }
}
