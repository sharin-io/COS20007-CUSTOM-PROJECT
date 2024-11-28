using static Treasure_Hunter.Interface;

public class Shop : IShop
{
    /// Gets the name of the shop.
    public string Name { get; private set; }

    /// Gets the list of items available in the shop.
    public List<ICollectible> AvailableItems { get; private set; }

    /// Initializes a new instance of the <see cref="Shop"/> class with a list of items.
    public Shop(string name, List<ICollectible> items)
    {
        Name = name;
        AvailableItems = items;
    }

    /// Retrieves an item from the shop by its name.
    public ICollectible GetItem(string itemName)
    {
        return AvailableItems.FirstOrDefault(item => item.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
    }

    /// Removes an item from the shop's available items list.
    public void RemoveItem(ICollectible item)
    {
        AvailableItems.Remove(item);
    }
}
