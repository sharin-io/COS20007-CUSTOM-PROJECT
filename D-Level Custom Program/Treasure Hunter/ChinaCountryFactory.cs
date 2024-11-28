using System;
using static Treasure_Hunter.Interface;
namespace Treasure_Hunter
{
    public class ChinaCountryFactory : ICountryFactory
    {
        public Country CreateCountry()
        {
            // Create collectible items
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
            var bronzeVessel = new CollectibleItem(
                "Bronze Ritual Vessel",
                "An ornate bronze vessel used in ancient ceremonies",
                120
            );
            var porcelainVase = new CollectibleItem(
                "Ming Dynasty Vase",
                "A blue and white porcelain vase with dragon motifs",
                150
            );
            var silkRobe = new CollectibleItem(
                "Imperial Silk Robe",
                "An embroidered silk robe worn by nobility",
                90
            );
            var goldPendant = new CollectibleItem(
                "Golden Dragon Pendant",
                "A detailed pendant featuring a traditional Chinese dragon",
                85
            );
            var inkstone = new CollectibleItem(
                "Scholar's Inkstone",
                "A carved stone used for grinding ink in calligraphy",
                45
            );
            var boneOracle = new CollectibleItem(
                "Oracle Bone",
                "An ancient bone with early Chinese writing",
                110
            );
            var celadonBowl = new CollectibleItem(
                "Celadon Bowl",
                "A delicate green-glazed ceramic bowl",
                70
            );

            // Create shops with expanded inventory
            var xianShop = new Shop("Xian Antiques", new List<ICollectible>
            {
                terracottaWarrior,
                bronzeVessel,
                boneOracle
            });

            var beijingShop = new Shop("Beijing Treasures", new List<ICollectible>
            {
                jadeBracelet,
                ancientScroll,
                silkRobe
            });

            var suzhouShop = new Shop("Suzhou Artifacts", new List<ICollectible>
            {
                porcelainVase,
                inkstone,
                celadonBowl
            });

            var hangzhouShop = new Shop("Hangzhou Relics", new List<ICollectible>
            {
                goldPendant,
                jadeBracelet,
                silkRobe
            });

            // Create lists for shops and quest items
            var shops = new List<IShop>
            {
                xianShop,
                beijingShop,
                suzhouShop,
                hangzhouShop
            };

            var questItems = new List<ICollectible>
            {
                terracottaWarrior,
                jadeBracelet,
                ancientScroll,
                bronzeVessel,
                porcelainVase,
                silkRobe,
                goldPendant,
                inkstone,
                boneOracle,
                celadonBowl
            };

            return new Country("China", shops, questItems);
        }
    }
}