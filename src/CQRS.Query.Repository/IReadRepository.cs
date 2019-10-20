using System;
using System.Threading.Tasks;
using CQRS.Query.Repository.Response;

namespace CQRS.Query.Repository
{
    public interface IReadRepository<T> where T : class
    {
        Task<string> GetByIdAsync(Guid id);
        Task<ElasticSearchResponse> AddOrUpdateAsync(string message, string model, string id);
    }
}