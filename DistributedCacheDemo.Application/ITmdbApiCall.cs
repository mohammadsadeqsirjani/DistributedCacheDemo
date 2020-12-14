using System.Collections.Generic;
using System.Threading.Tasks;

namespace DistributedCacheDemo.Application
{
    public interface ITmdbApiCall
    {
        Task<List<string>> GetMovies(string actor);
    }
}
