using Application.Common.Interfaces.Repositories;
using Domain.Reads;

namespace Infrastructure.Persistence.Repositories;

public class ReadsRepositoryStatic : IReadsRepository
{
    private readonly static List<Read> _reads = new List<Read>() {
        Read.Create("Kantipur", "Improve your attention span", "Improve your attention span", new Uri("https://bigthink.com/the-learning-curve/attention/"), "imageurl"),
        Read.Create("Online", "Healing power", "Reminiscing", new Uri("http://getpocket.com/explore/item/the-healing-power-of-reminiscing"), "imageurl"),
        Read.Create("Gizmodo", "US", "Alicia Keys", new Uri("http://thecut.com/2023/11/alicia-keys-new-york-first-album.html?utm_source=pocket_saves"), "imageurl")
    };

    public async Task<Read> GetPdfDetailsAsync(Uri url)
    {
        var read = _reads.FirstOrDefault(r => r.Id == url);
        if (read is null) return Read.EmptyReads;
        return read;
    }

    public async Task<List<Read>> GetPdfsFromUrlListAsync(IReadOnlyList<Uri> urlList)
    {
        var read = _reads
                        .Where(r => urlList.Contains(r.Url))
                        .ToList();
        return read;
    }
}
