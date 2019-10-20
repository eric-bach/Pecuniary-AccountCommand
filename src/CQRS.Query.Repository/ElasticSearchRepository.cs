using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CQRS.Query.Repository.Response;
using Newtonsoft.Json;

namespace CQRS.Query.Repository
{
    public class ElasticSearchRepository<T> : IReadRepository<T> where T : class
    {
        public string ElasticSearchDomain { get; set; }
        public HttpClient HttpClient { get; set; }

        public ElasticSearchRepository()
        {
            ElasticSearchDomain = Environment.GetEnvironmentVariable("ElasticSearchDomain");
            HttpClient = new HttpClient();
        }

        public async Task<string> GetByIdAsync(Guid id)
        {
            var model = Regex.Matches(typeof(T).Name, @"([A-Z][a-z]+)").Select(m => m.Value).First().ToLower();
            var url = $"https://{ElasticSearchDomain}/{model}/_doc/{id}";
            
            var response = await HttpClient.GetAsync(url);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<ElasticSearchResponse> AddOrUpdateAsync(string message, string model, string id)
        {
            var url = $"https://{ElasticSearchDomain}/{model}/_doc/{id}";
            var content = new StringContent(message, Encoding.UTF8, "application/json");

            var response = await HttpClient.PutAsync(url, content);

            var body = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ElasticSearchResponse>(body);
        }
    }
}
