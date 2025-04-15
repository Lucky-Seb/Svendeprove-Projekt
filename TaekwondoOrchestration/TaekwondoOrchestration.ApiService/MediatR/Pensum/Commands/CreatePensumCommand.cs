using MediatR;
using TaekwondoApp.Shared.DTO;

namespace TaekwondoOrchestration.ApiService.MediatR.Pensum.Commands
{
    public class CreatePensumCommand : IRequest<PensumDTO>
    {
        public PensumDTO Pensum { get; }

        public CreatePensumCommand(PensumDTO pensum)
        {
            Pensum = pensum;
        }
    }
}
