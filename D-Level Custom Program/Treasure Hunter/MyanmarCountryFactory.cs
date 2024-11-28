using System;
using static Treasure_Hunter.Interface;
namespace Treasure_Hunter
{
    public class MyanmarCountryFactory : ICountryFactory
    {
        public Country CreateCountry()
        {
            // Create collectible items for Myanmar
            var goldenBuddha = new CollectibleItem(
                "Golden Buddha",
                "An ancient golden Buddha statue from a temple",
                80
            );
            var rubyGem = new CollectibleItem(
                "Mogok Ruby",
                "A precious red ruby from the famous Mogok mines",
                60
            );
            var lacquerBox = new CollectibleItem(
                "Bagan Lacquerware",
                "A beautifully crafted traditional lacquer box",
                40
            );
            var jadePendant = new CollectibleItem(
                "Jade Pendant",
                "A traditional Myanmar jade pendant with carvings",
                50
            );

            // Create shops
            var mandalayShop = new Shop("Mandalay Antiques", new List<ICollectible>
            {
                goldenBuddha,
                rubyGem,
                new CollectibleItem("Bronze Bell", "A traditional temple bell", 15)
            });

            var baganShop = new Shop("Bagan Heritage", new List<ICollectible>
            {
                lacquerBox,
                jadePendant,
                new CollectibleItem("Palm Leaf Manuscript", "Ancient writings on palm leaves", 20)
            });

            var shops = new List<IShop> { mandalayShop, baganShop };
            var questItems = new List<ICollectible>
            {
                goldenBuddha,
                rubyGem,
                lacquerBox,
                jadePendant
            };

            return new Country("Myanmar", shops, questItems);
        }
    }
}