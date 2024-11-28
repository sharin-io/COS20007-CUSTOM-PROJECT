using System;
using static System.Formats.Asn1.AsnWriter;
using static Treasure_Hunter.Interface;

public class Player
{
    /// Gets the name of the player.
    public string Name { get; private set; }

    /// Gets the list of items the player has in their inventory.
    public List<ICollectible> Inventory { get; private set; }

    /// Gets the amount of coins the player currently holds.
    public int Coins { get; private set; }

    /// Initializes a new instance of the <see cref="Player"/> class.
    public Player(string name)
    {
        Name = name;
        Inventory = new List<ICollectible>();
        Coins = 100; // Starting coins
    }

    /// Checks if the player has a specific item in their inventory.
    public bool HasItem(string itemName)
    {
        return Inventory.Any(item => item.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
    }

    /// Adds an item to the player's inventory.
    public void AddToInventory(ICollectible item)
    {
        Inventory.Add(item);
    }
}

