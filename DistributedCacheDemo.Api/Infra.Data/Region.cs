using DistributedCacheDemo.Api.Application.Interfaces;
using System.Collections.Generic;

namespace DistributedCacheDemo.Api.Infra.Data
{
    public class Region : IRegion
    {
        private static readonly List<string> Regions = new List<string>()
        {
            "Usa", "Uk", "Australia", "Japan", "Spain", "Italy", "South Korea", "Pakistan"
        };

        public List<string> GetRegions()
        {
            return Regions;
        }

        public void SetRegion(string region)
        {
            Regions.Add(region);
        }

        private static int Length => Regions.Count;
    }
}
