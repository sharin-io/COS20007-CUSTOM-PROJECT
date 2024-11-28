using System;
using static Treasure_Hunter.Interface;
namespace Treasure_Hunter
{
    public class ThailandCountryFactory : ICountryFactory
    {
        public Country CreateCountry()
        {
            // Create collectible items for Thailand
            var emeraldBuddha = new CollectibleItem(
                "Emerald Buddha Replica",
                "A small replica of the famous Emerald Buddha",
                75
            );
            var siameseMask = new CollectibleItem(
                "Khon Mask",
                "A traditional Thai theatrical mask",
                55
            );
            var goldNielloBox = new CollectibleItem(
                "Gold Niello Box",
                "An intricate box with traditional Thai black metalwork",
                65
            );
            var templeCharm = new CollectibleItem(
                "Temple Guardian Charm",
                "A blessed charm from an ancient temple",
                45
            );

            // Create shops
            var bangkokShop = new Shop("Bangkok Antiquities", new List<ICollectible>
            {
                emeraldBuddha,
                siameseMask,
                new CollectibleItem("Silk Tapestry", "A decorative silk wall hanging", 25)
            });

            var ayutthayaShop = new Shop("Ayutthaya Treasures", new List<ICollectible>
            {
                goldNielloBox,
                templeCharm,
                new CollectibleItem("Bronze Gong", "An ancient ceremonial gong", 20)
            });

            var shops = new List<IShop> { bangkokShop, ayutthayaShop };
            var questItems = new List<ICollectible>
            {
                emeraldBuddha,
                siameseMask,
                goldNielloBox,
                templeCharm
            };

            return new Country("Thailand", shops, questItems);
        }
    }
}