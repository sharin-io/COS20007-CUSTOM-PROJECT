using System;
using static Treasure_Hunter.Interface;

namespace Treasure_Hunter
{
    public class JapanCountryFactory : ICountryFactory
    {
        public Country CreateCountry()
        {
            // Create collectible items for Japan
            var samuraiSword = new CollectibleItem(
                "Samurai Sword",
                "A traditional Japanese sword",
                50
            );
            var ancientMap = new CollectibleItem(
                "Ancient Map" ,
                "A map that leads to hidden treasures",
                30
            );
            var goldenVase = new CollectibleItem(
                "Golden Vase",
                "An ornate golden vase from ancient times",
                70
            );

            // Create shops
            var tokyoShop = new Shop("Tokyo Shop", new List<ICollectible>
        {
            new CollectibleItem("Glasses", "A pair of glasses to improve vision", 10),
            ancientMap
        });

            var kyotoShop = new Shop("Kyoto Artifacts", new List<ICollectible>
        {
            samuraiSword,
            goldenVase
        });

            var shops = new List<IShop> { tokyoShop, kyotoShop };
            var questItems = new List<ICollectible>
        {
            samuraiSword,
            ancientMap,
            goldenVase
        };

            return new Country("Japan", shops, questItems);
        }
    }
}

