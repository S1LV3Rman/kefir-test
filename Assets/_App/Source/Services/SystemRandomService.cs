using System;

namespace Source
{
    public sealed class SystemRandomService : IRandomService
    {
        private readonly Random _rng;

        public SystemRandomService(int? seed = null)
        {
            _rng = seed.HasValue ? new Random(seed.Value) : new Random();
        }
        
        public int Range(int min, int max)
        {
            return _rng.Next(min, max + 1);
        }
    }
}