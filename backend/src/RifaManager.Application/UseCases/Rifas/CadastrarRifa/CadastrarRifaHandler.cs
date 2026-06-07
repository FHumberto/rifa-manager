using FluentValidation;
using FluentValidation.Results;
using RifaManager.Application.Exceptions;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Persistence;
using RifaManager.Domain.Persistence.Repositories;

namespace RifaManager.Application.UseCases.Rifas.CadastrarRifa;

public sealed class CadastrarRifaHandler : ICadastrarRifaUseCase
{
    #region [ DEPENDÊNCIAS ]

    private readonly IValidator<CadastrarRifaRequest> _validator;
    private readonly IRifaRepository _rifaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CadastrarRifaHandler(IValidator<CadastrarRifaRequest> validator, IRifaRepository rifaRepository, IUnitOfWork unitOfWork)
    {
        _validator = validator;
        _rifaRepository = rifaRepository;
        _unitOfWork = unitOfWork;
    }

    #endregion

    public async Task<CadastrarRifaResponse> Execute(CadastrarRifaRequest request, CancellationToken cancellationToken)
    {
        await ValidarRequisicao(request, cancellationToken);

        Rifa rifa = new(request.Nome, request.Descricao, request.ValorBilhete, request.DataSorteio, request.Premio);

        await _rifaRepository.AddAsync(rifa, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new CadastrarRifaResponse(rifa.Id);
    }

    private async Task ValidarRequisicao(CadastrarRifaRequest request, CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult);
    }
}
