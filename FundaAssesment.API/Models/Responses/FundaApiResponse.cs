using System.Text.Json.Serialization;

namespace FundaAssesment.API.Models.Responses
{
    public class FundaApiResponse
    {
        [JsonPropertyName("Objects")]
        public List<Advertisement> Advertisements { get; set; }
        public Paging Paging { get; set; }
        [JsonPropertyName("TotaalAantalObjecten")]
        public int TotalNumberOfObjects { get; set; }
    }
}
