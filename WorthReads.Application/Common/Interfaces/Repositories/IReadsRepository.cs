namespace Application.Common.Interfaces.Repositories;

public interface IReadsRepository
{
    List<Domain.Reads.Reads> GetPopularReads();
    List<Domain.Reads.Reads> GetRecentReads();
}
