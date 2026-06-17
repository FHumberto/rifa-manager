using RifaManager.Web.Models.Common;
using RifaManager.Web.Models.Usuarios;

namespace RifaManager.Web.Services.Usuarios;

public interface IUsuarioService
{
    Task<ApiResult<GetUsuarioByIdResponse>> GetByIdAsync(Guid id, string accessToken, CancellationToken cancellationToken = default);
}
