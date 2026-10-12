namespace MedTools.Identity.API.Options;

public sealed class DatabaseOption
{
    public const string SectionName = "Database";

    public string ConnectionString { get; set; } = string.Empty;
    public int CommandTimeout { get; set; }
}
