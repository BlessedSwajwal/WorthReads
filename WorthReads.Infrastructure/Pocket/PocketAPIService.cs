using Domain.Reads;
using Infrastructure.Pocket;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json;

namespace Infrastructure.Services;

public class PocketAPIService : IPocketAPIService
{
    private readonly HttpClient _httpClient;
    private readonly PocketAPISettings _pocketAPISettings;

    public PocketAPIService(HttpClient httpClient, IOptions<PocketAPISettings> pocketAPISettings)
    {
        _httpClient = httpClient;
        _pocketAPISettings = pocketAPISettings.Value;
    }

    public async Task<List<Reads>> GetPocketList()
    {
        var reads = new List<Reads>();
        var data = new
        {
            consumer_key = _pocketAPISettings.ConsumerKey,
            access_token = _pocketAPISettings.AccessToken,
            contentType = "article",
            count = _pocketAPISettings.ResultCount
        };
        var result = await _httpClient.PostAsJsonAsync("/v3/get", data);

        //TODO - Use proper flow control
        if (!result.IsSuccessStatusCode) throw new Exception("Pocket request failed.");


        string jsonResponse = await result.Content.ReadAsStringAsync();
        var json = JsonDocument.Parse(jsonResponse);
        JsonElement element = json.RootElement.GetProperty("list");

        foreach (JsonProperty property in element.EnumerateObject())
        {
            var itemElement = JsonDocument.Parse(property.Value.ToString());
            string resolvedTitle = itemElement.RootElement.GetProperty("resolved_title").GetString() ?? string.Empty;
            string resolvedUrl = itemElement.RootElement.GetProperty("resolved_url").GetString() ?? string.Empty;
            string excerpt = itemElement.RootElement.GetProperty("excerpt").GetString() ?? string.Empty;
            string topImageUrl = itemElement.RootElement.GetProperty("top_image_url").GetString() ?? string.Empty;

            reads.Add(Reads.Create(string.Empty, resolvedTitle, excerpt, resolvedUrl, topImageUrl));
        }

        return reads;
    }
}


