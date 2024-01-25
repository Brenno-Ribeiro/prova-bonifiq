using ProvaPub.Services.Interfaces;

namespace ProvaPub.Services
{
    public class RandomService : IRandomService
	{
		public RandomService()
		{
		}

		public int GetRandom()
		{
            return new Random(GenerateSeed()).Next(100);
        }

		private int GenerateSeed()
		{
			return Guid.NewGuid().GetHashCode();
        }

	}
}
