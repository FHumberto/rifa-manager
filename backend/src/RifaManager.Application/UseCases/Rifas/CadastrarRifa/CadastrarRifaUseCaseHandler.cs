using FluentValidation;
using FluentValidation.Results;
using RifaManager.Application.Exceptions;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Persistence;

namespace RifaManager.Application.UseCases.Rifas.CadastrarRifa;

public sealed class CadastrarRifaUseCaseHandler : ICadastrarRifaUseCase
{
    #region [ DEPENDÊNCIAS ]

    private readonly IValidator<CadastrarRifaRequest> _validator;
    private readonly IRifaRepository _rifaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CadastrarRifaUseCaseHandler(IValidator<CadastrarRifaRequest> validator, IRifaRepository rifaRepository, IUnitOfWork unitOfWork)
    {
        _validator = validator;
        _rifaRepository = rifaRepository;
        _unitOfWork = unitOfWork;
    }

    #endregion

    public async Task<CadastrarRifaResponse> Execute(CadastrarRifaRequest request)
    {
        await ValidarRequisicao(request);

        Rifa rifa = new(request.Nome, request.Descricao, request.ValorBilhete, request.DataSorteio, request.Premio);

        await _rifaRepository.AddAsync(rifa);
        await _unitOfWork.CommitAsync();

        return new CadastrarRifaResponse(rifa.Id);
    }

    private async Task ValidarRequisicao(CadastrarRifaRequest request)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(request);

        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult);
    }
}
