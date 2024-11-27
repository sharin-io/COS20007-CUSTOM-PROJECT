using System;
using static Treasure_Hunter.Interface;

namespace Treasure_Hunter
{
    public class Shop : IShop
    {
        public string Name { get; private set; }
        public List<ICollectible> AvailableItems { get; private set; }

        public Shop(string name, List<ICollectible> items)
        {
            Name = name;
            AvailableItems = items;
        }

        public ICollectible GetItem(string itemName)
        {
            return AvailableItems.FirstOrDefault(item => item.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        }

        public void RemoveItem(ICollectible item)
        {
            AvailableItems.Remove(item);
        }
    }
}

