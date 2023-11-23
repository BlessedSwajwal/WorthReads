using Application.Common.Interfaces.Repositories;
using Domain.Reads;
using Domain.Reads.ValueObjects;

namespace Infrastructure.Repositories;

public class ReadsRepository : IReadsRepository
{
    static Source source;
    public ReadsRepository()
    {
        source = new Source() { id = "Gizmodo", name = "Gizmodo" };
    }
    private static List<Reads> _reads = new()
    {
        new Reads(ReadsId.CreateUnique(), new Source() { id = "Gizmodo", name = "Gizmodo" }, "Sajal", "Title 1", "Random Description", "url1", "imageUrl1", DateTime.Parse("2023-11-02T13:31:44Z"), "Content 1"),
        new Reads(ReadsId.CreateUnique(), new Source() { id = "Gizmodo", name = "Gizmodo" }, "Ghimire", "Title 2", "Random Description", "url1", "imageUrl1", DateTime.Parse("2023-11-02T13:31:44Z"), "Content 1"),
        new Reads(ReadsId.CreateUnique(), new Source() { id = "Gizmodo", name = "Gizmodo" }, "Satoshi", "Title 3", "Random Description", "url1", "imageUrl1", DateTime.Parse("2023-11-02T13:31:44Z"), "Content 1"),
        new Reads(ReadsId.CreateUnique(), source, "Sajal", "Title 1", "Random Description", "url1", "imageUrl1", DateTime.Parse("2023-11-02T13:31:44Z"), "Content 1"),
        new Reads(ReadsId.CreateUnique(), source, "Sajal", "Title 1", "Random Description", "url1", "imageUrl1", DateTime.Parse("2023-11-02T13:31:44Z"), "Content 1")
    };

    public List<Reads> GetPopularReads()
    {
        return _reads;
    }

    public List<Reads> GetRecentReads()
    {
        return (from reads in _reads
                orderby reads.Source ascending
                select reads).ToList();
    }
}
