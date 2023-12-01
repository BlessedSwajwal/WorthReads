using Domain.Reads;

namespace Application.Common.Services;

public interface IGenerateContainerPdf
{
    Task<byte[]> GenerateContainerPdf(List<Read> container);
}
