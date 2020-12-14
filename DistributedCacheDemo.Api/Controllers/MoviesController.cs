using DistributedCacheDemo.Application;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DistributedCacheDemo.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly ITmdbApiCall _tmdbApiCall;

        public MoviesController(ITmdbApiCall tmdbApiCall)
        {
            _tmdbApiCall = tmdbApiCall;
        }

        [HttpGet("[action]/{actor}")]
        public async Task<List<string>> GetActorMovies(string actor)
        {
            return await _tmdbApiCall.GetMovies(actor);
        }
    }
}
