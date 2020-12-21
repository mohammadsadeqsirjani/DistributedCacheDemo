using System.Collections.Generic;

namespace DistributedCacheDemo.Api.Application.ViewModels
{
    public class MovieDto
    {
        public List<string> Movies { get; set; }
        public long ElapsedTime { get; set; }
    }
}
