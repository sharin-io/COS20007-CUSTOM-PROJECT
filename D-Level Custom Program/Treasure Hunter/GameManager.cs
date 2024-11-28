using System;
using System.Collections.Generic;
using System.Linq;
using static Treasure_Hunter.Interface;

namespace Treasure_Hunter
{
    /// Manages the core game logic, including player initialization,
    /// country progression, quest status, and shop interactions.
    public partial class GameManager
    {
        private Player? _player; // nullable reference type
        private Country? _currentCountry;
        private CountryProgressionManager _countryProgression;

        /// Gets or sets the player for the game.
        public Player Player
        {
            get => _player ?? throw new InvalidOperationException("Player not initialized");
            private set => _player = value;
        }

        /// Gets or sets the current country the player is in.
        public Country CurrentCountry
        {
            get => _currentCountry ?? throw new InvalidOperationException("Current country not set");
            private set => _currentCountry = value;
        }

        /// Initializes a new instance of the <see cref="GameManager"/> class.
        public GameManager()
        {
            _countryProgression = new CountryProgressionManager();
        }

        /// Starts a new game by initializing the player and setting the starting country.
        public void StartNewGame(string playerName)
        {
            _player = new Player(playerName);
            _currentCountry = _countryProgression.GetFirstCountry();
        }

        /// Attempts to progress to the next country if all quest items in the current country are collected.
        public bool TryProgressToNextCountry()
        {
            if (_player == null)
            {
                throw new InvalidOperationException("Player not initialized");
            }

            if (_currentCountry != null && _currentCountry.AreAllQuestItemsCollected(_player))
            {
                var nextCountry = _countryProgression.GetNextCountry();
                if (nextCountry != null)
                {
                    _currentCountry = nextCountry;
                    return true;
                }
            }

            return false;
        }

        /// Displays the player's quest status, including missing quest items in the current country.
        public void LookQuest()
        {
            Console.WriteLine("\n------- Quest Status -------");
            Console.WriteLine($"Location: {CurrentCountry.Name}");
            Console.WriteLine("\nItems to collect:");

            bool foundMissingItems = false;

            foreach (var item in CurrentCountry.QuestItems)
            {
                if (!Player.HasItem(item.Name))
                {
                    Console.WriteLine($"- {item.Name}");
                    foundMissingItems = true;
                }
            }

            if (!foundMissingItems)
            {
                Console.WriteLine("You have collected all quest items in this country!");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        /// Allows the player to visit a shop, view available items, and collect items.
        public void VisitShop(IShop shop)
        {
            if (_player == null)
            {
                throw new InvalidOperationException("Player not initialized");
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"\n--- Entering {shop.Name} ---");
                Console.WriteLine("Items available in the shop:");

                // Display available items
                for (int i = 0; i < shop.AvailableItems.Count; i++)
                {
                    Console.WriteLine($"- {shop.AvailableItems[i].Name}: {shop.AvailableItems[i].Description}");
                }

                if (shop.AvailableItems.Count == 0)
                {
                    Console.WriteLine("There is no item available to collect");
                }

                Console.WriteLine("\nEnter the item you want to collect (or type 'back' to return):");
                string input = Console.ReadLine()?.Trim() ?? "";

                if (input.Equals("back", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                // Find the item in the shop
                var item = shop.GetItem(input);
                if (item != null)
                {
                    _player.AddToInventory(item);
                    shop.RemoveItem(item);

                    // Special display for rare items
                    Console.WriteLine($"\nYou have collected {item.Name}!");
                    Console.WriteLine($"Description: {item.Description}");

                    if (_currentCountry != null && _currentCountry.AreAllQuestItemsCollected(_player))
                    {
                        Console.WriteLine("\nCongratulations! You have collected all items in this country!");
                    }

                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Item not found in this shop.");
                    Console.WriteLine("Press any key to try again...");
                    Console.ReadKey();
                }
            }
        }
    }
}
