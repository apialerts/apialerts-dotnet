using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using ApiAlerts.Common.network.contract;
using ApiAlerts.Common.util;

namespace ApiAlerts.Common.network;

internal class Network
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;

    internal Network(HttpClient? httpClient = null)
    {
        _httpClient = httpClient ?? new HttpClient
        {
            BaseAddress = new Uri(Constants.BaseUrl)
        };
        _jsonOptions = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
    }

    internal async Task<Result<T>> ApiRequest<T>(string apiKey, HttpMethod method, string route, object? payload)
    {
        try
        {
            using var request = new HttpRequestMessage(method, route);
            request.Headers.Add("Authorization", $"Bearer {apiKey}");
            request.Headers.Add("X-Integration", Constants.IntegrationName);
            request.Headers.Add("X-Version", Constants.Version);

            if (payload != null)
            {
                request.Content = JsonContent.Create(payload, options: _jsonOptions);
            }

            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<T>(content, _jsonOptions);
                if (result != null)
                {
                    return Result<T>.Success(result);
                }
            }

            var error = JsonSerializer.Deserialize<ErrorResponse>(content, _jsonOptions);
            if (error != null)
            {
                return Result<T>.Failure(error);
            }

            var fallback = new ErrorResponse { Message = $"Server responded with {(int)response.StatusCode}" };
            return Result<T>.Failure(fallback);
        }
        catch (Exception ex)
        {
            return Result<T>.Failure(new ErrorResponse { Message = ex.Message });
        }
    }
}