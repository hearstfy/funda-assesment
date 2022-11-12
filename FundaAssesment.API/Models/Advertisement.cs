using System.Text.Json.Serialization;

namespace FundaAssesment.API.Models
{
    public class Advertisement
    {
        [JsonPropertyName("MakelaarId")]
        public int RealEstateAgencyId { get; set; }
        [JsonPropertyName("MakelaarNaam")]
        public string RealEstateAgencyName { get; set; }
        [JsonPropertyName("IsVerkocht")]
        public bool IsSold { get; set; }
    }
}
