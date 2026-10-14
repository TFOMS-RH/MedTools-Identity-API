using System.Text.Json;
using System.Globalization;

using MedTools.Identity.API.Common.Models;
using MedTools.Identity.API.Common.Models.Params;
using MedTools.Identity.API.Database.Repositories;

namespace MedTools.Identity.API.Endpoints.Handlers.Users;

public static class UpdateUserHandler
{
    public static async Task<IResult> HandleAsync(
        Guid id,
        JsonElement request,
        UserRepository userRepository,
        CancellationToken cancellationToken)
    {
        if (request.ValueKind != JsonValueKind.Object)
        {
            return TypedResults.ValidationProblem(
                new Dictionary<string, string[]>()
                {
                    ["body"] = ["Ожидается JSON-объект."]
                });
        }

        if (!request.EnumerateObject().Any())
        {
            return Results.BadRequest(
                "Необходимо передать хотя бы одно поле для обновления");
        }

        var validationErrors = new Dictionary<string, string[]>();

        var updateParams = MapToUpdateUserParams(
            request,
            validationErrors);

        if (validationErrors.Count > 0)
        {
            return TypedResults.ValidationProblem(validationErrors);
        }

        await userRepository.UpdateUserAsync(
            id,
            updateParams,
            cancellationToken);

        return Results.NoContent();
    }

    private static UpdateUserParams MapToUpdateUserParams(
        JsonElement request,
        Dictionary<string, string[]> errors)
    {
        PatchField<string> firstName = default;
        PatchField<string> lastName = default;
        PatchField<string> middleName = default;
        PatchField<string> contactPhone = default;
        PatchField<string> email = default;
        PatchField<DateOnly?> birthDate = default;

        foreach (var property in request.EnumerateObject())
        {
            switch (property.Name)
            {
                case "firstName":

                    var firstNameValue = property.Value;

                    if (firstNameValue.ValueKind is not JsonValueKind.String)
                    {
                        errors.Add(
                            key: "firstName",
                            value: ["Поле firstName должно быть представлено строкой."]);

                        break;
                    }

                    firstName = new PatchField<string>(
                        IsSpecified: true,
                        Value: firstNameValue.GetString());

                    break;

                case "lastName":

                    var lastNameValue = property.Value;

                    if (lastNameValue.ValueKind is not JsonValueKind.String)
                    {
                        errors.Add(
                            key: "lastName",
                            value: ["Поле lastName должно быть представлено строкой."]);

                        break;
                    }

                    lastName = new PatchField<string>(
                        IsSpecified: true,
                        Value: lastNameValue.GetString());

                    break;

                case "middleName":

                    var middleNameValue = property.Value;

                    if (middleNameValue.ValueKind is not (JsonValueKind.String or JsonValueKind.Null))
                    {
                        errors.Add(
                            key: "middleName",
                            value: ["Поле middleName должно быть представлено строкой или null."]);

                        break;
                    }

                    middleName = new PatchField<string>(
                        IsSpecified: true,
                        Value: middleNameValue.GetString());

                    break;

                case "contactPhone":

                    var contactPhoneValue = property.Value;

                    if (contactPhoneValue.ValueKind is not (JsonValueKind.String or JsonValueKind.Null))
                    {
                        errors.Add(
                            key: "contactPhone",
                            value: ["Поле contactPhone должно быть представлено строкой или null."]);

                        break;
                    }

                    contactPhone = new PatchField<string>(
                        IsSpecified: true,
                        Value: contactPhoneValue.GetString());

                    break;

                case "email":

                    var emailValue = property.Value;

                    if (emailValue.ValueKind is not (JsonValueKind.String or JsonValueKind.Null))
                    {
                        errors.Add(
                            key: "email",
                            value: ["Поле email должно быть представлено строкой или null."]);

                        break;
                    }

                    email = new PatchField<string>(
                        IsSpecified: true,
                        Value: emailValue.GetString());

                    break;

                case "birthDate":

                    var birthDateValue = property.Value;

                    if (birthDateValue.ValueKind is JsonValueKind.Null)
                    {
                        birthDate = new(true, null);

                        break;
                    }

                    if (birthDateValue.ValueKind is not JsonValueKind.String
                        || !DateOnly.TryParseExact(
                            birthDateValue.GetString(),
                            "yyyy-MM-dd",
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.None,
                            out var dateOnly))
                    {
                        errors.Add(
                            key: "birthDate",
                            value: ["Переданная дата имеет неккоректный формат."]);

                        break;
                    }

                    birthDate = new(true, dateOnly);

                    break;

                default:

                    errors.Add(
                        key: property.Name,
                        value: ["Неизвестное поле для обновления."]);

                    break;
            }
        }

        return new UpdateUserParams(
            FirstName: firstName,
            LastName: lastName,
            MiddleName: middleName,
            ContactPhone: contactPhone,
            BirthDate: birthDate,
            Email: email);

    }
}
