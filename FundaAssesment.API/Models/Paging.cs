using System.Text.Json.Serialization;

namespace FundaAssesment.API.Models
{
    public class Paging
    {
        [JsonPropertyName("AantalPaginas")]
        public int NumberOfPages { get; set; }
        [JsonPropertyName("HuidigePagina")]
        public int CurrentPage { get; set; }
        [JsonPropertyName("VolgendeUrl")]
        public string NextUrl { get; set; }
    }
}
