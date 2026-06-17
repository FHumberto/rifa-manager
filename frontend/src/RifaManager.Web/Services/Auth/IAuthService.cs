using RifaManager.Web.Models.Auth;
using RifaManager.Web.Models.Common;

namespace RifaManager.Web.Services.Auth;

public interface IAuthService
{
    Task<ApiResult<LoginResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
}
