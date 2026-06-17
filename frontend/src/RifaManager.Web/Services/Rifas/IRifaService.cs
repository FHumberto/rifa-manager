using RifaManager.Web.Models.Common;
using RifaManager.Web.Models.Rifas;

namespace RifaManager.Web.Services.Rifas;

public interface IRifaService
{
    Task<ApiResult<IReadOnlyList<ListarRifasResponse>>> ListarAsync(string accessToken, CancellationToken cancellationToken = default);
    Task<ApiResult<GetRifaByIdResponse>> GetByIdAsync(Guid id, string accessToken, CancellationToken cancellationToken = default);
    Task<ApiResult<CadastrarRifaResponse>> CadastrarAsync(CadastrarRifaRequest request, string accessToken, CancellationToken cancellationToken = default);
    Task<ApiResult<bool>> EditarAsync(Guid id, EditarRifaRequest request, string accessToken, CancellationToken cancellationToken = default);
    Task<ApiResult<bool>> EncerrarAsync(Guid id, string accessToken, CancellationToken cancellationToken = default);
    Task<ApiResult<SortearRifaResponse>> SortearAsync(Guid id, string accessToken, CancellationToken cancellationToken = default);
}
