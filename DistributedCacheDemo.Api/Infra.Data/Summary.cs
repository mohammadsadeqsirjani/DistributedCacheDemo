using System;
using DistributedCacheDemo.Api.Application.Interfaces;

namespace DistributedCacheDemo.Api.Infra.Data
{
    public class Summary : ISummary
    {
        private static readonly string[] Summaries =
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public string GetSummary()
        {
            var rng = new Random();

            return Summaries[rng.Next(Length)];
        }

        private static int Length => Summaries.Length;
    }
}
