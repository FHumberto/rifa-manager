using RifaManager.Application.Exceptions;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Errors;
using RifaManager.Domain.Persistence.Repositories;

namespace RifaManager.Application.UseCases.Participantes.GetById;

public sealed class GetParticipanteByIdHandler(IParticipanteRepository participanteRepository) : IGetParticipanteByIdUseCase
{
    public async Task<GetParticipanteByIdResponse> Execute(Guid id, CancellationToken cancellationToken)
    {
        Participante participante = await participanteRepository.GetByIdWithBilhetesAsync(id, cancellationToken)
            ?? throw new NotFoundException(ParticipanteErrors.ParticipanteNaoEncontrado.Description);

        return new GetParticipanteByIdResponse
        (
            participante.Id,
            participante.Nome,
            participante.Telefone,
            participante.Observacao,
            participante.Bilhetes
                .Select(bilhete => new ParticipanteBilheteResponse(bilhete.Id, bilhete.Numero, bilhete.RifaId, bilhete.Status.ToString()))
                .ToList()
        );
    }
}
