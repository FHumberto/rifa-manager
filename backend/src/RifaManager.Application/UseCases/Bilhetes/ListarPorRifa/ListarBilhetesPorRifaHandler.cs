using RifaManager.Application.Exceptions;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Errors;
using RifaManager.Domain.Persistence;

namespace RifaManager.Application.UseCases.Bilhetes.ListarPorRifa;

public sealed class ListarBilhetesPorRifaHandler(IBilheteRepository bilheteRepository, IRifaRepository rifaRepository) : IListarBilhetesPorRifaUseCase
{
    public async Task<IReadOnlyList<ListarBilhetesPorRifaResponse>> Execute(Guid rifaId, CancellationToken cancellationToken)
    {
        if (await rifaRepository.GetByIdAsync(rifaId, cancellationToken) is null)
            throw new NotFoundException(RifaErrors.RifaNaoEncontrada.Description);

        IReadOnlyList<Bilhete> bilhetes = await bilheteRepository.GetByRifaIdAsync(rifaId, cancellationToken);

        return bilhetes
            .Select(bilhete => new ListarBilhetesPorRifaResponse
            (
                bilhete.Id,
                bilhete.Numero,
                bilhete.Status.ToString(),
                bilhete.CriadoEm,
                bilhete.PagoEm,
                bilhete.CanceladoEm,
                bilhete.ParticipanteId,
                bilhete.Participante.Nome,
                bilhete.UsuarioResponsavelId,
                bilhete.UsuarioResponsavel.Nome
            ))
            .ToList();
    }
}
