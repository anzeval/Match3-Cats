using System;

namespace Match3.Model.Utils
{
    public class Rng
    {
        Random random;

        public Rng(int seed)
        {
            random = new Random(seed);
        }

        public Rng()
        {
            random = new Random();
        }

        public int NextInt(int minValue, int maxValue)
        {
            return random.Next(minValue, maxValue);
        }
    }
}