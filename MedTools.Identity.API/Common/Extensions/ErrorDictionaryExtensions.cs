namespace MedTools.Identity.API.Common.Extensions;

public static class ErrorDictionaryExtensions
{
    public static Dictionary<string, List<string>> AddError(
        this Dictionary<string, List<string>> errors,
        string field,
        string message)
    {
        if (!errors.TryGetValue(
            field, 
            out var messages))
        {
            messages = [];
            errors[field] = messages;
        }

        messages.Add(message);

        return errors;
    }
}
