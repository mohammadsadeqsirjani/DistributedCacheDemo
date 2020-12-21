using Newtonsoft.Json;

namespace DistributedCacheDemo.Api.Domain.Models.Movie
{
    public class Movie
    {
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
