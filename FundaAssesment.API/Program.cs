using FundaAssesment.API.Clients;
using FundaAssesment.API.Models;
using FundaAssesment.API.Models.Responses;
using FundaAssesment.API.Services;
using Polly.Extensions.Http;
using Polly;

namespace FundaAssesment.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<IFundaService, FundaService>();
            builder.Services.AddScoped<IRestClient<Response<FundaApiResponse>>, FundaRestClient>();
            builder.Services.AddHttpClient("FundaHttpClient", client =>
            {
                var url = "http://partnerapi.funda.nl/feeds/Aanbod.svc/json";
                var key = "ac1b0b1572524640a0ecc54de453ea9f";
                client.BaseAddress = new Uri($"{url}/{key}");
            }).AddPolicyHandler(GetRetryPolicy());

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode != System.Net.HttpStatusCode.OK)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
    }
}