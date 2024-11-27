using System;
using static Treasure_Hunter.Interface;
using System.Numerics;

namespace Treasure_Hunter
{
    public class Country
    {
        public string Name { get; private set; }
        public List<IShop> Shops { get; private set; }
        public List<ICollectible> QuestItems { get; private set; }

        public Country(string name, List<IShop> shops, List<ICollectible> questItems)
        {
            Name = name;
            Shops = shops;
            QuestItems = questItems;
        }

        public bool AreAllQuestItemsCollected(Player player)
        {
            return QuestItems.All(item =>
                player.Inventory.Any(i => i.Name.Equals(item.Name, StringComparison.OrdinalIgnoreCase))
            );
        }
    }
}

