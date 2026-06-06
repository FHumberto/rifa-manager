using RifaManager.Application.Exceptions;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Errors;
using RifaManager.Domain.Persistence;

namespace RifaManager.Application.UseCases.Bilhetes.CancelarBilhete;

public sealed class CancelarBilheteUseCaseHandler(IRifaRepository rifaRepository, IUnitOfWork unitOfWork) : ICancelarBilheteUseCase
{
    public async Task Execute(Guid id)
    {
        Rifa rifa = await rifaRepository.GetByBilheteIdWithBilhetesAsync(id)
            ?? throw new NotFoundException(BilheteErrors.BilheteNaoEncontrado.Description);

        Bilhete bilhete = rifa.Bilhetes.First(bilhete => bilhete.Id == id);

        rifa.MarcarBilheteComoCancelado(bilhete);

        await rifaRepository.UpdateAsync(rifa);
        await unitOfWork.CommitAsync();
    }
}
