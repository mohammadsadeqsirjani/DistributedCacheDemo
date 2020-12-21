using System.Threading.Tasks;

namespace DistributedCacheDemo.Api.Application.Interfaces
{
    public interface IRepository<T> where T : class, new()
    {
        public Task<T> TryGetAsync(string key);
        public Task<bool> TrySetAsync(string key, T model);
        public Task<bool> TryModifyAsync(string key, T model);
        public Task<bool> TryDeleteAsync(string key);
    }
}
