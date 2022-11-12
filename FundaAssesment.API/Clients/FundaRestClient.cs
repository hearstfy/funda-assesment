using FundaAssesment.API.Models.Responses;
using System.Text.Json;

namespace FundaAssesment.API.Clients
{
    public class FundaRestClient : IRestClient<Response<FundaApiResponse>>
    {
        private readonly HttpClient _httpClient;

        public FundaRestClient(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("FundaHttpClient");
        }

        public async Task<Response<FundaApiResponse>> GetAsync(string query)
        {
            try
            {
                var apiResponse = await _httpClient.GetAsync(query);
                if (!apiResponse.IsSuccessStatusCode)
                {
                    return new Response<FundaApiResponse>()
                    {
                        ErrorCode = (int)apiResponse.StatusCode,
                        ErrorMessage = apiResponse.Content.ReadAsStringAsync().Result,
                        IsSuccess = false
                    };
                }

                var jsonString = await apiResponse.Content.ReadAsStringAsync();
                var json = JsonSerializer.Deserialize<FundaApiResponse>(jsonString);

                return new Response<FundaApiResponse>()
                {
                    IsSuccess = true,
                    Result = json
                };
            }
            catch (Exception ex )
            {
                return new Response<FundaApiResponse>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }

        }
    }
}
