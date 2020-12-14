using Newtonsoft.Json;
using System.Collections.Generic;

namespace DistributedCacheDemo.Domain.Models.Actor
{
    public class ActorList
    {
        [JsonProperty("results")]
        public List<Actor> Actors { get; set; }
    }
}
