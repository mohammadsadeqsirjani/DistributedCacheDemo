using DistributedCacheDemo.Infra.Data;
using System;

namespace DistributedCacheDemo.Application
{
    public class Region : IRegion
    {
        private static readonly string[] Regions =
        {
            "Usa", "Uk", "Australia", "Japan", "Spain", "Italy", "South Korea", "Pakistan"
        };

        public string GetRegion()
        {
            var rng = new Random();

            return Regions[rng.Next(Length)];
        }

        private static int Length => Regions.Length;
    }
}
