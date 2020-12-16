using System.Collections.Generic;
using System.Threading.Tasks;

namespace DistributedCacheDemo.Infra.Data
{
    public interface ITmdbApiCall
    {
        Task<List<string>> GetMovies(string actor);
    }
}
