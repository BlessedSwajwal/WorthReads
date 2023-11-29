using Domain.Reads;
using Infrastructure.Services;

namespace Infrastructure.Pocket;

public class PocketAPIMockService : IPocketAPIService
{

    public PocketAPIMockService(HttpClient httpClient)
    {
    }

    public async Task<List<Read>> GetPocketList()
    {
        return new List<Read>() {
            Read.Create("Kantipur", "Improve your attention span", "Improve your attention span", "https://bigthink.com/the-learning-curve/attention/", "imageurl"),
            Read.Create("Online", "Healing power", "Reminiscing", "http://getpocket.com/explore/item/the-healing-power-of-reminiscing", "imageurl"),
            Read.Create("Gizmodo", "US", "Alicia Keys", "http://thecut.com/2023/11/alicia-keys-new-york-first-album.html?utm_source=pocket_saves", "imageurl")
        };
    }
}
