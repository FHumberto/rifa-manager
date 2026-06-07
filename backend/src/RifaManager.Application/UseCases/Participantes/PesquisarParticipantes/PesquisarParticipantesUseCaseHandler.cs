using RifaManager.Application.Exceptions;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Enums;
using RifaManager.Domain.Persistence.Repositories;

namespace RifaManager.Application.UseCases.Participantes.PesquisarParticipantes;

public sealed class PesquisarParticipantesUseCaseHandler(IParticipanteRepository participanteRepository) : IPesquisarParticipantesUseCase
{
    public async Task<IReadOnlyList<PesquisarParticipantesResponse>> Execute(PesquisarParticipantesRequest request, CancellationToken cancellationToken)
    {
        if (request.StatusPagamento.HasValue && !Enum.IsDefined(typeof(StatusPagamento), request.StatusPagamento.Value))
            throw new BadRequestException("Status de pagamento invalido.");

        IReadOnlyList<Participante> participantes = await participanteRepository.SearchAsync(request.Nome, request.Telefone, request.NumeroBilhete, request.StatusPagamento, cancellationToken);

        return participantes
            .Select(participante => new PesquisarParticipantesResponse(participante.Id, participante.Nome, participante.Telefone, participante.Observacao))
            .ToList();
    }
}
