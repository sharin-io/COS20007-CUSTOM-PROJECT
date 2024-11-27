using System;
using static Treasure_Hunter.Interface;

namespace Treasure_Hunter
{
	public class CollectibleItem : IdentifiableObject, ICollectible
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int Value { get; private set; }

    public CollectibleItem(string name, string description, int value) 
        : base(new[] { name })
    {
        Name = name;
        Description = description;
        Value = value;
    }
}
}

