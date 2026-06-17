using System.Net.Http.Json;
using RifaManager.Web.Constants;
using RifaManager.Web.Models.Auth;
using RifaManager.Web.Models.Common;

namespace RifaManager.Web.Services.Auth;

public sealed class AuthService(HttpClient httpClient) : IAuthService
{
    public async Task<ApiResult<LoginResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        HttpResponseMessage response = await httpClient.PostAsJsonAsync(ApiRoutes.Auth.Login, request, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            LoginResponse? loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>(cancellationToken);

            if (loginResponse is not null && !string.IsNullOrWhiteSpace(loginResponse.AccessToken))
            {
                return ApiResult<LoginResponse>.Ok(loginResponse);
            }

            return ApiResult<LoginResponse>.Fail(new ApiErrorResponse
            {
                Title = "Resposta inválida da API.",
                Status = (int)response.StatusCode
            });
        }

        ApiErrorResponse? error = await response.Content.ReadFromJsonAsync<ApiErrorResponse>(cancellationToken);

        return ApiResult<LoginResponse>.Fail(error ?? new ApiErrorResponse
        {
            Title = "Não foi possível realizar o login.",
            Status = (int)response.StatusCode
        });
    }
}
