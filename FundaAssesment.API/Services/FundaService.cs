using FundaAssesment.API.Clients;
using FundaAssesment.API.Models;
using FundaAssesment.API.Models.DTOs;
using FundaAssesment.API.Models.Responses;

namespace FundaAssesment.API.Services
{
    public class FundaService : IFundaService
    {
        private readonly IRestClient<Response<FundaApiResponse>> _fundaClient;

        public FundaService(IRestClient<Response<FundaApiResponse>> fundaClient)
        {
            _fundaClient = fundaClient;
        }

        public async Task<Response<List<AgenciesDTO>>> GetTop10(string location, string type)
        {
            var response = new Response<List<AgenciesDTO>>();
            var ads = new List<Advertisement>();
            int totalPages, currentPage = 1;

            do
            {
                var query = GetQuery(location, type, currentPage);
                var fundaResponse = await _fundaClient.GetAsync(query);
                if (!fundaResponse.IsSuccess)
                {
                    return new Response<List<AgenciesDTO>>()
                    {
                        ErrorCode = fundaResponse.ErrorCode,
                        ErrorMessage = fundaResponse.ErrorMessage
                    };
                }

                ads.AddRange(fundaResponse.Result.Advertisements);
                totalPages = fundaResponse.Result.Paging.NumberOfPages;
                currentPage++;
            } while (currentPage <= totalPages);

            var result = ads.Where(ad => ad.IsSold == false)
                .GroupBy(ad => new { ad.RealEstateAgencyId, ad.RealEstateAgencyName })
                .Select(ad => new AgenciesDTO() { AgencyName = ad.Key.RealEstateAgencyName, NumberOfProperties = ad.Count() })
                .OrderByDescending(dto => dto.NumberOfProperties)
                .Take(10)
                .ToList();

            return new Response<List<AgenciesDTO>>()
            {
                IsSuccess = true,
                Result = result
            }; 
        }

        private string GetQuery(string location, string type, int page = 1)
        {
            var query = $"?type={type}&zo=/{location}&pageSize={25}&page={page}";
            return query;
        }
    }
}
