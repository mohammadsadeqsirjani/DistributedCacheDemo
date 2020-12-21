using DistributedCacheDemo.Api.Application.Interfaces;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System.Threading.Tasks;

namespace DistributedCacheDemo.Api.Infra.Data
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly IRedisCacheClient _client;

        public Repository(IRedisCacheClient client)
        {
            _client = client;
        }

        public async Task<T> TryGetAsync(string key)
        {
            var content = await _client.Db0.GetAsync<T>(key);

            return content;
        }

        public async Task<bool> TrySetAsync(string key, T model)
        {
            var succeeded = await _client.Db0.AddAsync(key, model, 1.Minutes());

            return succeeded;
        }

        public async Task<bool> TryModifyAsync(string key, T model)
        {
            var content = await TryGetAsync(key);

            var succeeded = content.IsNull();

            succeeded.IfTrue(async () =>
            {
                succeeded = await _client.Db0.AddAsync(key, model, 1.Minutes());
            });

            return succeeded;
        }

        public async Task<bool> TryDeleteAsync(string key)
        {
            var content = await TryGetAsync(key);

            var succeeded = content.IsNull();

            succeeded.IfFalse(async () =>
            {
                succeeded = await _client.Db0.RemoveAsync(key);
            });

            return succeeded;
        }
    }
}
