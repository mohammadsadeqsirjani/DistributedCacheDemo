using Newtonsoft.Json;

namespace DistributedCacheDemo.Domain.Models.Actor
{
    public class Actor
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
