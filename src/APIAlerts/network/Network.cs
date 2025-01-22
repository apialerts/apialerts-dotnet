using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using APIAlerts.network.contract;
using APIAlerts.util;

namespace APIAlerts.network;

internal class Network
{
    private readonly HttpClient _httpClient;

    internal Network(HttpClient? httpClient = null)
    {
        _httpClient = httpClient ?? new HttpClient
        {
            BaseAddress = new Uri(Constants.BaseUrl)
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
                request.Content = JsonContent.Create(payload, options: Json.JsonOptions);
            }

            var response = await _httpClient.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var success = await response.Content.ReadFromJsonAsync<T>(Json.JsonOptions);
                if (success != null)
                {
                    return Result<T>.Success(success);
                }
            }
            
            var error = await response.Content.ReadFromJsonAsync<ErrorResponse>(Json.JsonOptions);
            return Result<T>.Failure(error ?? new ErrorResponse { Message = "Unknown response" });
        }
        catch (JsonException)
        {
            return Result<T>.Failure(new ErrorResponse { Message = "Failed to deserialize response" });
        }
        catch (TaskCanceledException)
        {
            return Result<T>.Failure(new ErrorResponse { Message = "Request cancelled" });
        }
        catch (Exception ex)
        {
            return Result<T>.Failure(new ErrorResponse { Message = $"Unknown error: {ex.Message}" });
        }
    }
}