using MediatR;
using TaekwondoApp.Shared.DTO;

namespace TaekwondoOrchestration.ApiService.MediatR.Pensum.Commands
{
    public class UpdatePensumCommand : IRequest<bool>
    {
        public Guid Id { get; }
        public PensumDTO Pensum { get; }

        public UpdatePensumCommand(Guid id, PensumDTO pensum)
        {
            Id = id;
            Pensum = pensum;
        }
    }
}