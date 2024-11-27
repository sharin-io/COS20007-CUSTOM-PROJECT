using System;
using System.Collections.Generic;
using System.Linq;
using static Treasure_Hunter.Interface;

namespace Treasure_Hunter
{
    public partial class GameManager
    {
        private Player? _player;
        private Country? _currentCountry;
        private CountryProgressionManager _countryProgression;

        // Modified to include setters
        public Player Player
        {
            get => _player ?? throw new InvalidOperationException("Player not initialized");
            private set => _player = value;
        }

        public Country CurrentCountry
        {
            get => _currentCountry ?? throw new InvalidOperationException("Current country not set");
            private set => _currentCountry = value;
        }

        public GameManager()
        {
            _countryProgression = new CountryProgressionManager();
        }

        public void StartNewGame(string playerName)
        {
            // Create player
            _player = new Player(playerName);

            // Set initial country 
            _currentCountry = _countryProgression.GetFirstCountry();
        }

        public bool TryProgressToNextCountry()
        {
            if (_player == null)
            {
                throw new InvalidOperationException("Player not initialized");
            }

            // Check if all quest items are collected
            if (_currentCountry != null && _currentCountry.AreAllQuestItemsCollected(_player))
            {
                // Try to get next country
                var nextCountry = _countryProgression.GetNextCountry();

                if (nextCountry != null)
                {
                    _currentCountry = nextCountry;
                    return true;
                }
            }

            return false;
        }

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

        
    }
}