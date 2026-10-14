using MedTools.Identity.API.Common.Extenstions;
using MedTools.Identity.API.Database.Repositories;
using MedTools.Identity.API.Common.Models.Requests;

namespace MedTools.Identity.API.Endpoints.Handlers.Users;

public static class CreateUserHandler
{
    public static async Task<IResult> HandleAsync(
        CreateUserRequest request,
        UserRepository userRepository,
        OrganizationRepository organizationRepository,
        CancellationToken cancellationToken)
    {
        var validationResult = Validate(request);

        if (validationResult.Count > 0)
        {
            return TypedResults.ValidationProblem(validationResult);
        }

        var isExistingOrganization = await organizationRepository.IsExistingOrganziationAsync(request.OrganizationId);

        if (!isExistingOrganization)
        {
            return TypedResults.ValidationProblem(new Dictionary<string, string[]>
            {
                [nameof(request.OrganizationId)] = ["Указанная организация не существует."]
            });
        }

        var userIdentifier = await userRepository.AddUserAsync(
            userName: request.UserName,
            password: request.Password,
            firstName: request.FirstName,
            lastName: request.LastName,
            middleName: request.MiddleName,
            email: request.Email,
            contactPhone: request.ContactPhone,
            birthDate: request.BirthDate,
            organizationId: request.OrganizationId,
            cancellationToken: cancellationToken);

        return Results.CreatedAtRoute(
            routeName: "GetUserById",
            routeValues: new { id = userIdentifier });
    }

    private static Dictionary<string, string[]> Validate(CreateUserRequest request)
    {
        var errors = new Dictionary<string, List<string>>();

        if (string.IsNullOrWhiteSpace(request.UserName))
        {
            errors.AddError(
                nameof(request.UserName), 
                "Имя пользователя пустое либо содержит пустые пробелы.");
        }

        return errors.ToDictionary(
            x => x.Key,
            x => x.Value.ToArray());
    }

    
}
