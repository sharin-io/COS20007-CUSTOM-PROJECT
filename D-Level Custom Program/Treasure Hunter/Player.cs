using System;
using static System.Formats.Asn1.AsnWriter;
using static Treasure_Hunter.Interface;

namespace Treasure_Hunter
{
    public class Player
    {
        public string Name { get; private set; }
        public List<ICollectible> Inventory { get; private set; }
        public int Coins { get; private set; }

        public Player(string name)
        {
            Name = name;
            Inventory = new List<ICollectible>();
            Coins = 100; // Starting coins
        }

        public void AddToInventory(ICollectible item)
        {
            Inventory.Add(item);
        }

        public bool HasItem(string itemName)
        {
            return Inventory.Any(item =>
                item.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase)
            );
        }


    }
}

