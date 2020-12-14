using Newtonsoft.Json;

namespace DistributedCacheDemo.Domain.Models.Movie
{
    public class Movie
    {
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
