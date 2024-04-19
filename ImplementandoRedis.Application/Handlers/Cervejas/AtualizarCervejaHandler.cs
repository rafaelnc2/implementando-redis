using ImplementandoRedis.Application.Commands.Cervejas;
using ImplementandoRedis.Shared.Responses.Cervejas;

namespace ImplementandoRedis.Application.Handlers.Cervejas;

public class AtualizarCervejaHandler : IRequestHandler<AtualizarCervejaCommand, CustomResult<CriarCervejaResponse>>
{
    private readonly ICervejaRepository _cervejaRepo;
    private readonly ITipoCervejaRepository _tipoCervejaRepo;
    private readonly IPublisher _eventPublisher;

    public AtualizarCervejaHandler([FromKeyedServices(KeyedServicesName.CERVEJA_REDIS_REPO)] ICervejaRepository repository,
        [FromKeyedServices(KeyedServicesName.TIPO_CERVEJA_EF_REPO)] ITipoCervejaRepository tipoCervejaRepo,
        IPublisher eventPublisher)
    {
        _cervejaRepo = repository;
        _tipoCervejaRepo = tipoCervejaRepo;
        _eventPublisher = eventPublisher;
    }

    public Task<CustomResult<CriarCervejaResponse>> Handle(AtualizarCervejaCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
