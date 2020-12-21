using System.Collections.Generic;
using System.Threading.Tasks;

namespace DistributedCacheDemo.Api.Application.Interfaces
{
    public interface ITmdbApiCall
    {
        Task<List<string>> GetMovies(string actor);
    }
}
