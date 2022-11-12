using FundaAssesment.API.Models.DTOs;
using FundaAssesment.API.Models.Responses;

namespace FundaAssesment.API.Services
{
    public interface IFundaService
    {
        public Task<Response<List<AgenciesDTO>>> GetTop10(string location, string type);
    }
}
