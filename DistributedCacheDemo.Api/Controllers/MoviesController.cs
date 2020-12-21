using DistributedCacheDemo.Api.Application.Interfaces;
using DistributedCacheDemo.Api.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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
        public async Task<MovieDto> GetActorMovies(string actor)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            var movies = await _tmdbApiCall.GetMovies(actor);

            stopwatch.Stop();

            return new MovieDto()
            {
                Movies = movies,
                ElapsedTime = stopwatch.ElapsedMilliseconds
            };
        }
    }
}
