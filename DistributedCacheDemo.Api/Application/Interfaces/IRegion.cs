using System.Collections.Generic;

namespace DistributedCacheDemo.Api.Application.Interfaces
{
    public interface IRegion
    {
        public List<string> GetRegions();
        public void SetRegion(string region);
    }
}
