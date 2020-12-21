using Newtonsoft.Json;

namespace DistributedCacheDemo.Api.Domain.Models.Actor
{
    public class Actor
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
