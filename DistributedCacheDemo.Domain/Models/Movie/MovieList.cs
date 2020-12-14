using Newtonsoft.Json;
using System.Collections.Generic;

namespace DistributedCacheDemo.Domain.Models.Movie
{
    public class MovieList
    {
        [JsonProperty("cast")]
        public List<Movie> Movies { get; set; }
    }
}
