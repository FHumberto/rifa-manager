using Ardalis.Result;
using RifaManager.Application.Abstractions.Persistence;
using RifaManager.Application.Abstractions.Results;
using RifaManager.Domain.Abstractions;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Errors;

namespace RifaManager.Application.Features.Cadastrar;

public sealed class CadastrarRifaHandler(IRifaRepository rifaRepository, IUnitOfWork unitOfWork) : ICadastrarRifaUseCase
{
    public async Task<Result<CadastrarRifaResponse>> Execute(CadastrarRifaRequest request)
    {
        if (request is null)
            return RifaErrors.RequestObrigatorio.ToResult<CadastrarRifaResponse>();

        try
        {
            Rifa rifa = new(request.Nome, request.Descricao, request.ValorBilhete, request.DataSorteio, request.Premio);

            await rifaRepository.AddAsync(rifa);
            await unitOfWork.SaveChangesAsync();

            return Result<CadastrarRifaResponse>.Created(new CadastrarRifaResponse(rifa.Id));
        }
        catch (DomainException exception)
        {
            return exception.Error.ToResult<CadastrarRifaResponse>();
        }
    }
}
