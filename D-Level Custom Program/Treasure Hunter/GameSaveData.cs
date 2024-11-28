using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using static Treasure_Hunter.Interface;

namespace Treasure_Hunter
{
    /// Represents the structure of the game's save data.
    public class GameSaveData
    {
        /// Gets or sets the player's name.
        public string PlayerName { get; set; }

        /// Gets or sets the player's inventory as a list of item names.
        public List<string> Inventory { get; set; }

        /// Gets or sets the name of the current country.
        public string CurrentCountry { get; set; }

        /// Gets or sets the list of collected quest items' names.
        public List<string> CollectedQuestItems { get; set; }

        /// Gets or sets the date and time the game was saved.
        public DateTime SaveDate { get; set; }

        private const int TRAVEL_COST = 50; 

    }

    /// Manages the game's main logic, including saving and loading game progress.
    public partial class GameManager
    {
        private const string SAVE_DIRECTORY = "SavedGames";

        /// Retrieves all countries in the game progression.
        private List<Country> Countries => _countryProgression.GetAllCountries();

        /// Saves the current game state to a JSON file.
        public void SaveCurrentGame(string saveFilePath = null)
        {
            try
            {
                Directory.CreateDirectory(SAVE_DIRECTORY);

                var saveData = new GameSaveData
                {
                    PlayerName = Player.Name,
                    Inventory = Player.Inventory.ConvertAll(item => item.Name),
                    CurrentCountry = CurrentCountry.Name,
                    CollectedQuestItems = Player.Inventory
                        .FindAll(item => CurrentCountry.QuestItems.Exists(q => q.Name == item.Name))
                        .ConvertAll(item => item.Name),
                    SaveDate = DateTime.Now
                };

                // If a save file path is provided, use it. Otherwise, create a new one.
                string savePath = saveFilePath ?? Path.Combine(SAVE_DIRECTORY, $"{Player.Name}_Save.json");

                string jsonString = JsonSerializer.Serialize(saveData, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                // Overwrite the existing save file
                File.WriteAllText(savePath, jsonString);

                Console.WriteLine("Game saved successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving game: {ex.Message}");
                throw;
            }
        }


        /// Retrieves a list of saved game summaries.
        public List<string> GetSavedGames()
        {
            try
            {
                var savedGames = new List<string>();

                if (Directory.Exists(SAVE_DIRECTORY))
                {
                    foreach (string file in Directory.GetFiles(SAVE_DIRECTORY, "*.json"))
                    {
                        try
                        {
                            string jsonString = File.ReadAllText(file);
                            var saveData = JsonSerializer.Deserialize<GameSaveData>(jsonString);
                            savedGames.Add($"{saveData.PlayerName} - {saveData.SaveDate:yyyy-MM-dd HH:mm:ss}");
                        }
                        catch
                        {
                            // Skip corrupted save files
                            continue;
                        }
                    }
                }

                return savedGames;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading save games list: {ex.Message}");
                return new List<string>();
            }
        }

        /// Loads a saved game based on the player's selection.
        public bool LoadGame(string saveSelection)
        {
            try
            {
                if (!Directory.Exists(SAVE_DIRECTORY))
                {
                    return false;
                }

                string[] saveFiles = Directory.GetFiles(SAVE_DIRECTORY, "*.json");
                foreach (string file in saveFiles)
                {
                    string jsonString = File.ReadAllText(file);
                    var saveData = JsonSerializer.Deserialize<GameSaveData>(jsonString);
                    string saveDisplay = $"{saveData.PlayerName} - {saveData.SaveDate:yyyy-MM-dd HH:mm:ss}";

                    if (saveDisplay == saveSelection)
                    {
                        // Create new player
                        _player = new Player(saveData.PlayerName);

                        // Set current country
                        _currentCountry = _countryProgression.GetCountryByName(saveData.CurrentCountry);
                        if (_currentCountry == null)
                        {
                            throw new Exception("Could not find saved country data");
                        }

                        // Restore inventory items
                        foreach (string itemName in saveData.Inventory)
                        {
                            var item = FindItemByName(itemName);
                            if (item != null)
                            {
                                _player.AddToInventory(item);

                                // Remove collected items from shops
                                foreach (var shop in _currentCountry.Shops)
                                {
                                    var shopItem = shop.GetItem(itemName);
                                    if (shopItem != null)
                                    {
                                        shop.RemoveItem(shopItem);
                                    }
                                }
                            }
                        }

                        // Restore collected quest items
                        foreach (string questItemName in saveData.CollectedQuestItems)
                        {
                            var questItem = _currentCountry.QuestItems.FirstOrDefault(q => q.Name == questItemName);
                            if (questItem != null)
                            {
                                // Ensure that the collected quest item is properly added to the player's inventory
                                _player.AddToInventory(questItem);
                            }
                        }

                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading save game: {ex.Message}");
                return false;
            }
        }

        /// Finds an item by its name across all countries and shops.
        private ICollectible FindItemByName(string name)
        {
            // First check quest items
            if (_currentCountry != null)
            {
                var questItem = _currentCountry.QuestItems.FirstOrDefault(i => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
                if (questItem != null) return questItem;

                // Then check shop items
                foreach (var shop in _currentCountry.Shops)
                {
                    var shopItem = shop.GetItem(name);
                    if (shopItem != null) return shopItem;
                }
            }

            // Check other countries if not found
            foreach (var country in _countryProgression.GetAllCountries())
            {
                if (country.Name == _currentCountry?.Name) continue;

                var questItem = country.QuestItems.FirstOrDefault(i => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
                if (questItem != null) return questItem;

                foreach (var shop in country.Shops)
                {
                    var shopItem = shop.GetItem(name);
                    if (shopItem != null) return shopItem;
                }
            }

            return null;
        }

        public static void CheckInventory(GameManager gameManager)
        {
            Console.WriteLine("\n------- Checking Inventory -------");
            Console.WriteLine($"\nInventory of {gameManager.Player.Name}:");

            if (gameManager.Player.Inventory.Count == 0)
            {
                Console.WriteLine("Your inventory is empty.");
            }
            else
            {
                foreach (var item in gameManager.Player.Inventory)
                {
                    Console.WriteLine($"- {item.Name}: {item.Description}");
                }
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        // In LookQuest method 
        public static void LookQuest(GameManager gameManager)
        {
            Console.WriteLine("\n------- Quest Status -------");
            Console.WriteLine($"Location: {gameManager.CurrentCountry.Name}");
            Console.WriteLine("\nItems to collect:");

            bool foundMissingItems = false;
            foreach (var item in gameManager.CurrentCountry.QuestItems)
            {
                if (!gameManager.Player.HasItem(item.Name)) 
                {
                    Console.WriteLine($"- {item.Name}");
                    foundMissingItems = true;
                }
            }

            if (!foundMissingItems)
            {
                Console.WriteLine("You have collected all quest items in this country!");
            }
        }

        


    }
}
