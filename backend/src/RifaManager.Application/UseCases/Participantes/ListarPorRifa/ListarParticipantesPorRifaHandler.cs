using RifaManager.Application.Exceptions;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Errors;
using RifaManager.Domain.Persistence;

namespace RifaManager.Application.UseCases.Participantes.ListarPorRifa;

public sealed class ListarParticipantesPorRifaHandler(IParticipanteRepository participanteRepository, IRifaRepository rifaRepository) : IListarParticipantesPorRifaUseCase
{
    public async Task<IReadOnlyList<ListarParticipantesPorRifaResponse>> Execute(Guid rifaId)
    {
        if (await rifaRepository.GetByIdAsync(rifaId) is null)
            throw new NotFoundException(RifaErrors.RifaNaoEncontrada.Description);

        IReadOnlyList<Participante> participantes = await participanteRepository.GetByRifaIdAsync(rifaId);

        return participantes
            .Select(participante => new ListarParticipantesPorRifaResponse(participante.Id, participante.Nome, participante.Telefone, participante.Observacao))
            .ToList();
    }
}
