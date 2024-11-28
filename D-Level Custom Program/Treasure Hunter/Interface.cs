using System;
using System.Collections.Generic;

namespace Treasure_Hunter
{
    /// Contains various interfaces to define shared behaviors for collectible items, shops, and country creation.
    public class Interface
    {
        /// Represents a collectible item in the game, such as treasures or quest items.
        public interface ICollectible
        {
            /// Gets the name of the collectible item.
            string Name { get; }

            /// Gets the description of the collectible item.
            string Description { get; }

            /// Gets the value of the collectible item.
            int Value { get; }
        }

        /// Represents a shop that players can visit to collect items.
        public interface IShop
        {
            /// Gets the name of the shop.
            string Name { get; }

            /// Gets the list of collectible items currently available in the shop.
            List<ICollectible> AvailableItems { get; }

            /// Retrieves a specific item from the shop by its name.
            ICollectible GetItem(string itemName);

            /// Removes a specific item from the shop.
            void RemoveItem(ICollectible item);
        }

        /// Represents a factory responsible for creating countries in the game.
        public interface ICountryFactory
        {
            /// Creates and returns a new instance of a country.
            Country CreateCountry();
        }
    }
}
