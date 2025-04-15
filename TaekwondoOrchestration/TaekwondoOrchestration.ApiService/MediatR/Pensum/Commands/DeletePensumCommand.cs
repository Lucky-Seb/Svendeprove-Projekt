using MediatR;

namespace TaekwondoOrchestration.ApiService.MediatR.Pensum.Commands
{
    public class DeletePensumCommand : IRequest<bool>
    {
        public Guid Id { get; }

        public DeletePensumCommand(Guid id)
        {
            Id = id;
        }
    }
}