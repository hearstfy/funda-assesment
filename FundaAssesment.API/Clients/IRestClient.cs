namespace FundaAssesment.API.Clients
{
    public interface IRestClient<T>
    {
        public Task<T> GetAsync(string query);
    }
}
