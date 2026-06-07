using RifaManager.Application.Exceptions;
using RifaManager.Domain.Abstractions;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Enums;
using RifaManager.Domain.Errors;
using RifaManager.Domain.Persistence.Repositories;

namespace RifaManager.Application.UseCases.Rifas.SortearRifa;

public sealed class SortearRifaHandler(IRifaRepository rifaRepository) : ISortearRifaUseCase
{
    public async Task<SortearRifaResponse> Execute(Guid id, CancellationToken cancellationToken)
    {
        Rifa rifa = await rifaRepository.GetByIdWithBilhetesAsync(id, cancellationToken)
            ?? throw new NotFoundException(RifaErrors.RifaNaoEncontrada.Description);

        List<Bilhete> bilhetesPagos = rifa.Bilhetes
            .Where(bilhete => bilhete.Status == StatusPagamento.Pago)
            .ToList();

        if (bilhetesPagos.Count == 0)
            throw new DomainException(RifaErrors.SemBilhetesPagosParaSorteio);

        Bilhete bilheteSorteado = bilhetesPagos[Random.Shared.Next(bilhetesPagos.Count)];

        return new SortearRifaResponse
        (
            rifa.Id,
            bilheteSorteado.Id,
            bilheteSorteado.Numero,
            bilheteSorteado.ParticipanteId,
            bilheteSorteado.Participante.Nome,
            bilheteSorteado.Participante.Telefone
        );
    }
}
