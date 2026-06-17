using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using RifaManager.Web.Constants;
using RifaManager.Web.Models.Common;
using RifaManager.Web.Models.Rifas;

namespace RifaManager.Web.Services.Rifas;

public sealed class RifaService(HttpClient httpClient) : IRifaService
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    public async Task<ApiResult<IReadOnlyList<ListarRifasResponse>>> ListarAsync(
        string accessToken,
        CancellationToken cancellationToken = default)
    {
        using HttpRequestMessage httpRequest = CreateRequest(HttpMethod.Get, ApiRoutes.Rifas.Base, accessToken);
        HttpResponseMessage response = await httpClient.SendAsync(httpRequest, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            List<ListarRifasResponse>? rifas = await response.Content.ReadFromJsonAsync<List<ListarRifasResponse>>(cancellationToken);
            return ApiResult<IReadOnlyList<ListarRifasResponse>>.Ok(rifas ?? []);
        }

        return ApiResult<IReadOnlyList<ListarRifasResponse>>.Fail(await ReadErrorAsync(response, "Não foi possível listar as rifas.", cancellationToken));
    }

    public async Task<ApiResult<GetRifaByIdResponse>> GetByIdAsync(
        Guid id,
        string accessToken,
        CancellationToken cancellationToken = default)
    {
        using HttpRequestMessage httpRequest = CreateRequest(HttpMethod.Get, ApiRoutes.Rifas.ById(id), accessToken);
        HttpResponseMessage response = await httpClient.SendAsync(httpRequest, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            GetRifaByIdResponse? rifa = await response.Content.ReadFromJsonAsync<GetRifaByIdResponse>(cancellationToken);

            if (rifa is not null)
            {
                return ApiResult<GetRifaByIdResponse>.Ok(rifa);
            }

            return ApiResult<GetRifaByIdResponse>.Fail(new ApiErrorResponse
            {
                Title = "Resposta inválida da API.",
                Status = (int)response.StatusCode
            });
        }

        return ApiResult<GetRifaByIdResponse>.Fail(await ReadErrorAsync(response, "Não foi possível carregar a rifa.", cancellationToken));
    }

    public async Task<ApiResult<CadastrarRifaResponse>> CadastrarAsync(
        CadastrarRifaRequest request,
        string accessToken,
        CancellationToken cancellationToken = default)
    {
        CadastrarRifaApiRequest apiRequest = new()
        {
            Nome = request.Nome,
            Descricao = request.Descricao,
            ValorBilhete = request.ValorBilhete!.Value,
            DataSorteio = request.DataSorteio!.Value.ToString("yyyy-MM-dd"),
            Premio = request.Premio
        };

        using HttpRequestMessage httpRequest = CreateRequest(HttpMethod.Post, ApiRoutes.Rifas.Base, accessToken, apiRequest);
        HttpResponseMessage response = await httpClient.SendAsync(httpRequest, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            CadastrarRifaResponse? rifa = await response.Content.ReadFromJsonAsync<CadastrarRifaResponse>(cancellationToken);

            if (rifa is not null)
            {
                return ApiResult<CadastrarRifaResponse>.Ok(rifa);
            }

            return ApiResult<CadastrarRifaResponse>.Fail(new ApiErrorResponse
            {
                Title = "Resposta inválida da API.",
                Status = (int)response.StatusCode
            });
        }

        return ApiResult<CadastrarRifaResponse>.Fail(await ReadErrorAsync(response, "Não foi possível cadastrar a rifa.", cancellationToken));
    }

    public async Task<ApiResult<bool>> EditarAsync(
        Guid id,
        EditarRifaRequest request,
        string accessToken,
        CancellationToken cancellationToken = default)
    {
        EditarRifaApiRequest apiRequest = new()
        {
            Nome = request.Nome,
            Descricao = request.Descricao,
            ValorBilhete = request.ValorBilhete!.Value,
            DataSorteio = request.DataSorteio!.Value.ToString("yyyy-MM-dd"),
            Premio = request.Premio
        };

        using HttpRequestMessage httpRequest = CreateRequest(HttpMethod.Put, ApiRoutes.Rifas.ById(id), accessToken, apiRequest);
        HttpResponseMessage response = await httpClient.SendAsync(httpRequest, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            return ApiResult<bool>.Ok(true);
        }

        return ApiResult<bool>.Fail(await ReadErrorAsync(response, "Não foi possível editar a rifa.", cancellationToken));
    }

    public async Task<ApiResult<bool>> EncerrarAsync(
        Guid id,
        string accessToken,
        CancellationToken cancellationToken = default)
    {
        using HttpRequestMessage httpRequest = CreateRequest(HttpMethod.Patch, ApiRoutes.Rifas.Encerrar(id), accessToken);
        HttpResponseMessage response = await httpClient.SendAsync(httpRequest, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            return ApiResult<bool>.Ok(true);
        }

        return ApiResult<bool>.Fail(await ReadErrorAsync(response, "Não foi possível encerrar a rifa.", cancellationToken));
    }

    public async Task<ApiResult<SortearRifaResponse>> SortearAsync(
        Guid id,
        string accessToken,
        CancellationToken cancellationToken = default)
    {
        using HttpRequestMessage httpRequest = CreateRequest(HttpMethod.Post, ApiRoutes.Rifas.Sortear(id), accessToken);
        HttpResponseMessage response = await httpClient.SendAsync(httpRequest, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            SortearRifaResponse? sorteio = await response.Content.ReadFromJsonAsync<SortearRifaResponse>(cancellationToken);

            if (sorteio is not null)
            {
                return ApiResult<SortearRifaResponse>.Ok(sorteio);
            }

            return ApiResult<SortearRifaResponse>.Fail(new ApiErrorResponse
            {
                Title = "Resposta inválida da API.",
                Status = (int)response.StatusCode
            });
        }

        return ApiResult<SortearRifaResponse>.Fail(await ReadErrorAsync(response, "Não foi possível sortear a rifa.", cancellationToken));
    }

    private static HttpRequestMessage CreateRequest(HttpMethod method, string uri, string accessToken, object? body = null)
    {
        HttpRequestMessage request = new(method, uri);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        if (body is not null)
        {
            request.Content = JsonContent.Create(body, options: JsonOptions);
        }

        return request;
    }

    private static async Task<ApiErrorResponse> ReadErrorAsync(
        HttpResponseMessage response,
        string fallbackTitle,
        CancellationToken cancellationToken)
    {
        ApiErrorResponse? error = await response.Content.ReadFromJsonAsync<ApiErrorResponse>(cancellationToken);

        return error ?? new ApiErrorResponse
        {
            Title = fallbackTitle,
            Status = (int)response.StatusCode
        };
    }
}
