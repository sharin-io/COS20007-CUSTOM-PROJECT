using System;
namespace Treasure_Hunter
{
	public class Interface
	{
        public interface ICollectible
        {
            string Name { get; }
            string Description { get; }
            int Value { get; }
        }

        // Interface for shops that can be visited
        public interface IShop
        {
            string Name { get; }
            List<ICollectible> AvailableItems { get; }
            ICollectible GetItem(string itemName);
            void RemoveItem(ICollectible item);
        }

        public interface ICountryFactory
        {
            Country CreateCountry();
        }
    }
}

