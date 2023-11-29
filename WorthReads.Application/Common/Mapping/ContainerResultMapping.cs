using Application.PdfContainers.Common;
using Domain.PdfContainer;
using Mapster;

namespace Application.Common.Mapping;

public class ContainerResultMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<PdfContainer, PdfContainerResult>()
            .Map(dest => dest.OwnerId, src => src.OwnerId.Value)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.IsPublic, src => src.IsPublic)
            .Map(dest => dest.ReadsUrls, src => src.ReadsUrl);
    }
}
