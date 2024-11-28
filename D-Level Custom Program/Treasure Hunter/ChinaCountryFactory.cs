using System;
using static Treasure_Hunter.Interface;

namespace Treasure_Hunter
{
    public class ChinaCountryFactory : ICountryFactory
    {
        public Country CreateCountry()
        {
            // Example of how a new country would be created
            var terracottaWarrior = new CollectibleItem(
                "Terracotta Warrior",
                "A clay soldier from the ancient Chinese army",
                100
            );
            var jadeBracelet = new CollectibleItem(
                "Jade Bracelet",
                "A delicate jade bracelet with intricate carvings",
                60
            );
            var ancientScroll = new CollectibleItem(
                "Ancient Scroll",
                "A historical scroll with mysterious writings",
                75
            );

            var xianShop = new Shop("Xian Antiques", new List<ICollectible>
        {
            terracottaWarrior
        });

            var beijingShop = new Shop("Beijing Treasures", new List<ICollectible>
        {
            jadeBracelet,
            ancientScroll
        });

            var shops = new List<IShop> { xianShop, beijingShop };
            var questItems = new List<ICollectible>
        {
            terracottaWarrior,
            jadeBracelet,
            ancientScroll
        };

            return new Country("China", shops, questItems);
        }
    }
}

