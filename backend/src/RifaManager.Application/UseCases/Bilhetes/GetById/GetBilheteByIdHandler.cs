using RifaManager.Application.Exceptions;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Errors;
using RifaManager.Domain.Persistence;

namespace RifaManager.Application.UseCases.Bilhetes.GetById;

public sealed class GetBilheteByIdHandler(IBilheteRepository bilheteRepository) : IGetBilheteByIdUseCase
{
    public async Task<GetBilheteByIdResponse> Execute(Guid id, CancellationToken cancellationToken)
    {
        Bilhete bilhete = await bilheteRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException(BilheteErrors.BilheteNaoEncontrado.Description);

        return new GetBilheteByIdResponse
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
            bilhete.Participante.Telefone,
            bilhete.UsuarioResponsavelId,
            bilhete.UsuarioResponsavel.Nome
        );
    }
}
