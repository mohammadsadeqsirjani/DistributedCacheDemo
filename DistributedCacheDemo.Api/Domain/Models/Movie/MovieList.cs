using System.Collections.Generic;
using Newtonsoft.Json;

namespace DistributedCacheDemo.Api.Domain.Models.Movie
{
    public class MovieList
    {
        [JsonProperty("cast")]
        public List<Movie> Movies { get; set; }
    }
}
