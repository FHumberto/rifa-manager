using RifaManager.Application.Exceptions;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Enums;
using RifaManager.Domain.Errors;
using RifaManager.Domain.Persistence;

namespace RifaManager.Application.UseCases.Bilhetes.ListarPorStatus;

public sealed class ListarBilhetesPorStatusHandler(IBilheteRepository bilheteRepository, IRifaRepository rifaRepository) : IListarBilhetesPorStatusUseCase
{
    public async Task<IReadOnlyList<ListarBilhetesPorStatusResponse>> Execute(StatusPagamento status, Guid? rifaId)
    {
        if (!Enum.IsDefined(status))
            throw new BadRequestException("Status de pagamento invalido.");

        if (rifaId.HasValue && await rifaRepository.GetByIdAsync(rifaId.Value) is null)
            throw new NotFoundException(RifaErrors.RifaNaoEncontrada.Description);

        IReadOnlyList<Bilhete> bilhetes = await bilheteRepository.GetByStatusAsync(status, rifaId);

        return bilhetes
            .Select(bilhete => new ListarBilhetesPorStatusResponse
            (
                bilhete.Id,
                bilhete.Numero,
                bilhete.Status.ToString(),
                bilhete.CriadoEm,
                bilhete.PagoEm,
                bilhete.CanceladoEm,
                bilhete.RifaId,
                bilhete.ParticipanteId,
                bilhete.Participante.Nome,
                bilhete.UsuarioResponsavelId,
                bilhete.UsuarioResponsavel.Nome
            ))
            .ToList();
    }
}
