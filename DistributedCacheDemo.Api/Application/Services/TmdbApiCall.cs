using DistributedCacheDemo.Api.Application.Interfaces;
using DistributedCacheDemo.Api.Domain.Models.Actor;
using DistributedCacheDemo.Api.Domain.Models.Movie;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DistributedCacheDemo.Api.Application.Services
{
    public class TmdbApiCall : ITmdbApiCall
    {
        private readonly string _url;
        private readonly string _apiKey;

        private readonly IDistributedCache _distributedCache;

        public TmdbApiCall(IDistributedCache distributedCache, IConfiguration configuration)
        {
            _distributedCache = distributedCache;

            var tmdbConfig = configuration.GetSection("Tmdb").Get<TmdbConfiguration>();

            _url = tmdbConfig.Url;
            _apiKey = tmdbConfig.ApiKey;
        }

        public async Task<List<string>> GetMovies(string actor)
        {
            var cacheKey = actor.ToLower();

            List<string> moviesList;
            string serializedMovies;

            var encodedMovies = await _distributedCache.GetAsync(cacheKey);

            if (encodedMovies != null)
            {
                serializedMovies = Encoding.UTF8.GetString(encodedMovies);
                moviesList = JsonConvert.DeserializeObject<List<string>>(serializedMovies);
            }
            else
            {
                moviesList = await GetMovieList(actor);
                serializedMovies = JsonConvert.SerializeObject(moviesList);
                encodedMovies = Encoding.UTF8.GetBytes(serializedMovies);
                var options = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1))
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(5));
                await _distributedCache.SetAsync(cacheKey, encodedMovies, options);
            }
            return moviesList;
        }

        private async Task<List<string>> GetMovieList(string actor)
        {
            var result = new List<string>();

            var id = await GetActorId(actor);

            var urlParameters = $"person/{id}/movie_credits?api_key={_apiKey}&language=en-US";
            var movieList = await GetAsync<MovieList>(_url, urlParameters);

            if (movieList == null) return result;

            result.AddRange(movieList.Movies.Select(movie => movie.Title));
            return result;
        }

        private async Task<int> GetActorId(string actor)
        {
            actor = WebUtility.UrlEncode(actor);

            var urlParameters = $"search/person?api_key={_apiKey}&query={actor}";
            var actorList = await GetAsync<ActorList>(_url, urlParameters);

            if (actorList != null && actorList.Actors.Count > 0)
                return actorList.Actors[0].Id;
            return -1;
        }

        private static HttpClient GetHttpClient(string url)
        {
            var client = new HttpClient { BaseAddress = new Uri(url) };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        private static async Task<T> GetAsync<T>(string url, string urlParameters)
        {
            using var client = GetHttpClient(url);

            var response = await client.GetAsync(urlParameters);

            if (response.StatusCode != HttpStatusCode.OK) return default;

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(json);

            return result;
        }

        private class TmdbConfiguration
        {
            public string Url { get; set; }
            public string ApiKey { get; set; }
        }
    }
}
