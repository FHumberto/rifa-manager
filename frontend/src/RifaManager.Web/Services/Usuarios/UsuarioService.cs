using System.Net.Http.Headers;
using System.Net.Http.Json;
using RifaManager.Web.Constants;
using RifaManager.Web.Models.Common;
using RifaManager.Web.Models.Usuarios;

namespace RifaManager.Web.Services.Usuarios;

public sealed class UsuarioService(HttpClient httpClient) : IUsuarioService
{
    public async Task<ApiResult<GetUsuarioByIdResponse>> GetByIdAsync(Guid id, string accessToken, CancellationToken cancellationToken = default)
    {
        using HttpRequestMessage request = new(HttpMethod.Get, ApiRoutes.Usuario.ById(id));
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        HttpResponseMessage response = await httpClient.SendAsync(request, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            GetUsuarioByIdResponse? usuario = await response.Content.ReadFromJsonAsync<GetUsuarioByIdResponse>(cancellationToken);

            if (usuario is not null)
            {
                return ApiResult<GetUsuarioByIdResponse>.Ok(usuario);
            }

            return ApiResult<GetUsuarioByIdResponse>.Fail(new ApiErrorResponse
            {
                Title = "Resposta inválida da API.",
                Status = (int)response.StatusCode
            });
        }

        ApiErrorResponse? error = await response.Content.ReadFromJsonAsync<ApiErrorResponse>(cancellationToken);

        return ApiResult<GetUsuarioByIdResponse>.Fail(error ?? new ApiErrorResponse
        {
            Title = "Não foi possível carregar os dados do usuário.",
            Status = (int)response.StatusCode
        });
    }
}
