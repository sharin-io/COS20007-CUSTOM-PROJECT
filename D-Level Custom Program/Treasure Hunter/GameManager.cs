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
        private const int TRAVEL_COST = 50; // Cost to travel between countries


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
                    // Add item to player's inventory
                    _player.AddToInventory(item);

                    // Remove item from shop
                    shop.RemoveItem(item);

                    // Add coins when collecting the item
                    _player.Coins += item.Value;

                    if (item.Name == "Samurai Sword")
                    {
                        Console.WriteLine("\n            /\\\n/vvvvvvvvvvvv \\--------------------------------------,\n`^^^^^^^^^^^^ /=====================================\"\n            \\/");
                    }
                    else if (item.Name == "Ancient Map")
                    {
                        Console.WriteLine("                 _,__        .:\n         Darwin <*  /        | \\\n            .-./     |.     :  :,\n           /           '-._/     \\_\n          /                '       \\\n        .'                         *: Brisbane\n     .-'                             ;\n     |                               |\n     \\                              /\n      |                            /\nPerth  \\*        __.--._          /\n        \\     _.'       \\:.       |\n        >__,-'             \\_/*_.-'\n                              Melbourne\n                             :--,\n                              '/");
                    }
                    else if (item.Name == "Golden Vase")
                    {
                        Console.WriteLine("   ,--,   \n   )\"\"(   \n  /    \\  \n /      \\ \n.        .\n|`-....-'|\n|        |\n|        |\n|`-....-'|\n|        |\n|        |\n `-....-' ");
                    }
                    Console.WriteLine($"\nYou have collected {item.Name}!");
                    Console.WriteLine($"Description: {item.Description}");
                    Console.WriteLine($"You earned {item.Value} coins!");
                    Console.WriteLine($"Current coins: {_player.Coins}");

                    // Check if all quest items are collected
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
        // Travel
        public bool TryTravelToCountry(string countryName)
        {
            if (_player == null)
            {
                throw new InvalidOperationException("Player not initialized");
            }

            // Don't charge if player has completed current country's quest
            if (!_currentCountry.AreAllQuestItemsCollected(_player))
            {
                // Check if player has enough coins
                if (_player.Coins < TRAVEL_COST)
                {
                    Console.WriteLine($"\nNot enough coins to travel! You need {TRAVEL_COST} coins.");
                    Console.WriteLine($"Current coins: {_player.Coins}");
                    return false;
                }

                // Deduct travel cost
                _player.Coins -= TRAVEL_COST;
                Console.WriteLine($"\nTravel cost: {TRAVEL_COST} coins");
                Console.WriteLine($"Remaining coins: {_player.Coins}");
            }

            // Get the requested country
            var newCountry = _countryProgression.GetCountryByName(countryName);
            if (newCountry != null)
            {
                _currentCountry = newCountry;
                return true;
            }

            return false;
        }
    }
}
