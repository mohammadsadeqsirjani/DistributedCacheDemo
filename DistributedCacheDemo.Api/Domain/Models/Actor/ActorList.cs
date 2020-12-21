using System.Collections.Generic;
using Newtonsoft.Json;

namespace DistributedCacheDemo.Api.Domain.Models.Actor
{
    public class ActorList
    {
        [JsonProperty("results")]
        public List<Actor> Actors { get; set; }
    }
}
