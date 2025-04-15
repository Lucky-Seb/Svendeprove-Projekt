using TaekwondoApp.Shared.DTO;

namespace TaekwondoOrchestration.ApiService.MediatR.Pensum
{
    public record CreatePensumCommand(PensumDTO Pensum) : IRequest<PensumDTO>;

}
