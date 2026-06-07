using FluentValidation;
using FluentValidation.Results;
using RifaManager.Application.Exceptions;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Enums;
using RifaManager.Domain.Errors;
using RifaManager.Domain.Persistence;

namespace RifaManager.Application.UseCases.Bilhetes.AlterarStatusBilhete;

public sealed class AlterarStatusBilheteUseCaseHandler : IAlterarStatusBilheteUseCase
{
    #region [ DEPEDÊNCIAS ]

    private readonly IValidator<AlterarStatusBilheteRequest> _validator;
    private readonly IRifaRepository _rifaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AlterarStatusBilheteUseCaseHandler(IValidator<AlterarStatusBilheteRequest> validator, IRifaRepository rifaRepository, IUnitOfWork unitOfWork)
    {
        _validator = validator;
        _rifaRepository = rifaRepository;
        _unitOfWork = unitOfWork;
    }

    #endregion

    public async Task Execute(Guid id, AlterarStatusBilheteRequest request, CancellationToken cancellationToken)
    {
        await ValidarRequisicao(request, cancellationToken);

        Rifa rifa = await _rifaRepository.GetByBilheteIdWithBilhetesAsync(id, cancellationToken)
            ?? throw new NotFoundException(BilheteErrors.BilheteNaoEncontrado.Description);

        Bilhete bilhete = rifa.Bilhetes.First(bilhete => bilhete.Id == id);

        if (request.Status == StatusPagamento.Pago)
        {
            rifa.MarcarBilheteComoPago(bilhete);
        }
        else if (request.Status == StatusPagamento.Cancelado)
        {
            rifa.MarcarBilheteComoCancelado(bilhete);
        }
        else
        {
            throw new BadRequestException("Status de pagamento invalido.");
        }

        await _rifaRepository.UpdateAsync(rifa);
        await _unitOfWork.CommitAsync(cancellationToken);
    }

    private async Task ValidarRequisicao(AlterarStatusBilheteRequest request, CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult);
    }
}
