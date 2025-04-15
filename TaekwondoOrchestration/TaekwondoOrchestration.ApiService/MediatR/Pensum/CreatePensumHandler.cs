using TaekwondoApp.Shared.DTO;
using TaekwondoOrchestration.ApiService.Services;

namespace TaekwondoOrchestration.ApiService.MediatR.Pensum
{
    public class CreatePensumHandler : IRequestHandler<CreatePensumCommand, PensumDTO>
    {
        private readonly IPensumService _service;

        public CreatePensumHandler(IPensumService service)
        {
            _service = service;
        }

        public async Task<PensumDTO> Handle(CreatePensumCommand request, CancellationToken cancellationToken)
        {
            return await _service.CreatePensumAsync(request.Pensum);
        }
    }
}
