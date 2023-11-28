namespace Infrastructure.Pocket;

public class PocketAPISettings
{
    public static string SectionName = "Pocket";
    public string AccessToken { get; set; } = null!;
    public string ConsumerKey { get; set; } = null!;
    public int ResultCount = 3;
}
