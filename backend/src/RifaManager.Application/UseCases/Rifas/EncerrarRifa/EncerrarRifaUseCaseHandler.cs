using RifaManager.Application.Exceptions;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Errors;
using RifaManager.Domain.Persistence;

namespace RifaManager.Application.UseCases.Rifas.EncerrarRifa;

public sealed class EncerrarRifaUseCaseHandler(IRifaRepository rifaRepository, IUnitOfWork unitOfWork) : IEncerrarRifaUseCase
{
    public async Task Execute(Guid id, CancellationToken cancellationToken)
    {
        Rifa rifa = await rifaRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException(RifaErrors.RifaNaoEncontrada.Description);

        rifa.Encerrar();

        await rifaRepository.UpdateAsync(rifa);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}
