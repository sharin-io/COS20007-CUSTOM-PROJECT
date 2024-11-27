﻿namespace Treasure_Hunter;
using System.IO;
using System;

class Program
{
    static void Main(string[] args)
    {
        // Create saves directory if it doesn't exist
        Directory.CreateDirectory("SavedGames");

        Console.Title = "The Rare Item Collector Game";
        DisplayGameIntro();
        MainMenu();


    }

    static void DisplayGameIntro()
    {
        Console.WriteLine(" _____                                                                                      _____ \n( ___ )------------------------------------------------------------------------------------( ___ )\n |   |                                                                                      |   | \n |   |  ██████╗  █████╗ ███╗   ███╗███████╗     ██████╗██████╗ ███████╗██████╗ ██╗████████╗ |   | \n |   | ██╔════╝ ██╔══██╗████╗ ████║██╔════╝    ██╔════╝██╔══██╗██╔════╝██╔══██╗██║╚══██╔══╝ |   | \n |   | ██║  ███╗███████║██╔████╔██║█████╗      ██║     ██████╔╝█████╗  ██║  ██║██║   ██║    |   | \n |   | ██║   ██║██╔══██║██║╚██╔╝██║██╔══╝      ██║     ██╔══██╗██╔══╝  ██║  ██║██║   ██║    |   | \n |   | ╚██████╔╝██║  ██║██║ ╚═╝ ██║███████╗    ╚██████╗██║  ██║███████╗██████╔╝██║   ██║    |   | \n |   |  ╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝     ╚═════╝╚═╝  ╚═╝╚══════╝╚═════╝ ╚═╝   ╚═╝    |   | \n |___|                                                                                      |___| \n(_____)------------------------------------------------------------------------------------(_____)");
        Console.WriteLine("\nGame Credit: The Rare Treasure Hunter Game");
        Console.WriteLine("Developed by: SHIN THANT THI RI");
        Console.WriteLine("Version: 1.0");
        Console.WriteLine("\nPress any key to start the game...");
        Console.ReadKey();
    }

    static void MainMenu()
    {
        GameManager gameManager = new GameManager();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("╔─────────────────────────╗\n│RARE TREASURE HUNTER GAME│\n│─────────────────────────│\n│                         │\n│  1. New Player          │\n│                         │\n│  2. Load Player         │\n│                         │\n│  3. Exit                │\n│                         │\n╘═════════════════════════╛");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateNewPlayer(gameManager);
                    break;
                case "2":
                    LoadSavedGame(gameManager);
                    break;
                case "3":
                    ExitGame();
                    return;
                default:
                    Console.WriteLine("Invalid option. Press any key to try again.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void CreateNewPlayer(GameManager gameManager)
    {
        Console.WriteLine("\n----- Creating a New Player -----");
        Console.Write("Enter Name: ");
        string playerName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(playerName))
        {
            Console.WriteLine("Invalid name. Press any key to return.");
            Console.ReadKey();
            return;
        }

        gameManager.StartNewGame(playerName);
        Console.Clear();
        Console.WriteLine("╔─────────────────────────────────────────────────────────────────────────────────────────╗\n│                                                                                         │\n│                                                                                         │\n│   ██████╗  █████╗ ███╗   ███╗███████╗    ███████╗████████╗ █████╗ ██████╗ ████████╗██╗  │\n│  ██╔════╝ ██╔══██╗████╗ ████║██╔════╝    ██╔════╝╚══██╔══╝██╔══██╗██╔══██╗╚══██╔══╝██║  │\n│  ██║  ███╗███████║██╔████╔██║█████╗      ███████╗   ██║   ███████║██████╔╝   ██║   ██║  │\n│  ██║   ██║██╔══██║██║╚██╔╝██║██╔══╝      ╚════██║   ██║   ██╔══██║██╔══██╗   ██║   ╚═╝  │\n│  ╚██████╔╝██║  ██║██║ ╚═╝ ██║███████╗    ███████║   ██║   ██║  ██║██║  ██║   ██║   ██╗  │\n│   ╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝    ╚══════╝   ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═╝   ╚═╝   ╚═╝  │\n│                                                                                         │\n│                                                                                         │\n╚─────────────────────────────────────────────────────────────────────────────────────────╝");
        Console.WriteLine($"\nWelcome {playerName} to The Rare Treasure Hunter Game!");
        Console.WriteLine("You will act as an Archeologist and have received your quest!");
        Console.WriteLine("\nPress any key start the game.");
        Console.ReadKey();
        PlayGame(gameManager);
    }

    public static void PlayGame(GameManager gameManager)
    {
        while (true)
        {
            // Check if all quest items are collected
            if (gameManager.CurrentCountry.AreAllQuestItemsCollected(gameManager.Player))
            {
                Console.WriteLine($"Congratulations! You have completed all quests in {gameManager.CurrentCountry.Name}!");

                // Attempt to progress to next country
                if (gameManager.TryProgressToNextCountry())
                {
                    Console.WriteLine($"Traveling to next destination: {gameManager.CurrentCountry.Name}");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Congratulations! You have completed the game!");
                    Console.WriteLine("Press any key to return to main menu...");
                    Console.ReadKey();
                    return;
                }
            }

            DisplayCountryMenu(gameManager);

            Console.Write("\nChoose an action: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    VisitShops(gameManager);
                    break;
                case "2":
                    CheckInventory(gameManager);
                    break;
                case "3":
                    LookQuest(gameManager);
                    break;
                case "4":
                    Console.WriteLine("More implementation will be added in version 2.0");
                    Console.ReadKey();
                    break;
                case "5":
                    gameManager.SaveCurrentGame();
                    Console.WriteLine("Game saved successfully!");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;
                case "6":
                    return; // Exit to main menu
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }

    static void DisplayCountryMenu(GameManager gameManager)
    {
        Console.Clear();
        Console.WriteLine($"\n----- Entering {gameManager.CurrentCountry.Name} -----");
        if ( gameManager.CurrentCountry.Name == "Japan")
        {
            Console.WriteLine("╔───────────────────────────────────────────────────────-─────────────╗");
            Console.WriteLine("                             ^\n                _______     ^^^\n               |xxxxxxx|  _^^^^^_\n               |xxxxxxx| | [][]  |\n            ______xxxxx| |[][][] |\n           |++++++|xxxx| | [][][]|      JAPAN\n           |++++++|xxxx| |[][][] |\n           |++++++|_________ [][]|\n           |++++++|=|=|=|=|=| [] |\n           |++++++|=|=|=|=|=|[][]|\n___________|++HH++|  _HHHH__|   _________   _________  _________\n         _______________   ______________      ______________\n__________________  ___________    __________________    ____________");
            Console.WriteLine("\n╚─────────────────────────────────────────────────────────────────────╝");
        }
        else if (gameManager.CurrentCountry.Name == "China")
        {
            Console.WriteLine("╔───────────────────────────────────────────────────────-─────────────────╗");
            Console.WriteLine("                                       .\n              . .                     -:-             .  .  .\n            .'.:,'.        .  .  .     ' .           . \\ | / .\n            .'.;.`.       ._. ! ._.       \\          .__\\:/__.\n             `,:.'         ._\\!/_.                     .';`.      . ' .\n             ,'             . ! .        ,.,      ..======..       .:.\n            ,                 .         ._!_.     ||::: : | .        ',\n     .====.,                  .           ;  .~.===: : : :|   ..===.\n     |.::'||      .=====.,    ..=======.~,   |\"|: :|::::::|   ||:::|=====|\n  ___| :::|!__.,  |:::::|!_,   |: :: ::|\"|l_l|\"|:: |:;;:::|___!| ::|: : :|\n |: :|::: |:: |!__|; :: |: |===::: :: :|\"||_||\"| : |: :: :|: : |:: |:::::|\n |:::| _::|: :|:::|:===:|::|:::|:===F=:|\"!/|\\!\"|::F|:====:|::_:|: :|::__:|\n !_[]![_]_!_[]![]_!_[__]![]![_]![_][I_]!//_:_\\\\![]I![_][_]!_[_]![]_!_[__]!\n -----------------------------------\"---''''```---\"-----------------------\n _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ |= _ _:_ _ =| _ _ _ _ _ _ _ _ _ _ _ _\n                                     |=    :    =|                CHINA\n_____________________________________L___________J________________________\n--------------------------------------------------------------------------");
            Console.WriteLine("\n╚────────────────────────────────────────────────────────────────────--───╝");
        }

        Console.WriteLine($"Archeologist {gameManager.Player.Name}, you are in {gameManager.CurrentCountry.Name} and you have a quest to complete");
        LookQuest(gameManager);
        Console.WriteLine($"\nIn {gameManager.CurrentCountry.Name}, we have ");
        for (int i = 0; i < gameManager.CurrentCountry.Shops.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {gameManager.CurrentCountry.Shops[i].Name}");
        }

        Console.WriteLine("\n------ Choose an action to perform -----");
        Console.WriteLine("1. Visit the shop");
        Console.WriteLine("2. Check Inventory");
        Console.WriteLine("3. Look Quest");
        Console.WriteLine("4. Move to other country");
        Console.WriteLine("5. Save Game");
        Console.WriteLine("6. Exit");
    }

    static void VisitShops(GameManager gameManager)
    {
        Console.WriteLine("\nChoose a shop to visit:");

        // Display available shops
        for (int i = 0; i < gameManager.CurrentCountry.Shops.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {gameManager.CurrentCountry.Shops[i].Name}");
        }

        Console.Write("\nEnter shop number: ");
        if (int.TryParse(Console.ReadLine(), out int shopIndex) &&
            shopIndex > 0 && shopIndex <= gameManager.CurrentCountry.Shops.Count)
        {
            gameManager.VisitShop(gameManager.CurrentCountry.Shops[shopIndex - 1]);
        }
        else
        {
            Console.WriteLine("Invalid shop selection.");
            Console.ReadKey();
        }
    }

    public static void CheckInventory(GameManager gameManager)
        {
            Console.WriteLine("\n------- Checking Inventory -------");
            Console.WriteLine($"\nInventory for {gameManager.Player.Name}:");

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

    public static void LookQuest(GameManager gameManager)
    {
        Console.WriteLine("You still need to collect:");
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

    static void ExitGame()
    {
        Console.WriteLine("Thank you for playing The Rare Item Collector Game!");
        Console.WriteLine("Goodbye!");
    }


    static void LoadSavedGame(GameManager gameManager)
    {

        Console.Clear();
        Console.WriteLine("\n----- Load Saved Game -----");

        var savedGames = gameManager.GetSavedGames();
        if (savedGames.Count == 0)
        {
            Console.WriteLine("No saved games found.");
            Console.WriteLine("\nPress any key to return to main menu...");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("\nAvailable saved games:");
        for (int i = 0; i < savedGames.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {savedGames[i]}");
        }

        Console.Write("\nEnter the number of the save to load (1 to cancel): ");
        if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= savedGames.Count)
        {
            string selectedSave = savedGames[choice - 1];
            if (gameManager.LoadGame(selectedSave))
            {
                Console.WriteLine($"\nSuccessfully loaded game for {selectedSave}!");
                Console.Clear();
                Console.WriteLine("╔─────────────────────────────────────────────────────────────────────────────────────────╗\n│                                                                                         │\n│                                                                                         │\n│   ██████╗  █████╗ ███╗   ███╗███████╗    ███████╗████████╗ █████╗ ██████╗ ████████╗██╗  │\n│  ██╔════╝ ██╔══██╗████╗ ████║██╔════╝    ██╔════╝╚══██╔══╝██╔══██╗██╔══██╗╚══██╔══╝██║  │\n│  ██║  ███╗███████║██╔████╔██║█████╗      ███████╗   ██║   ███████║██████╔╝   ██║   ██║  │\n│  ██║   ██║██╔══██║██║╚██╔╝██║██╔══╝      ╚════██║   ██║   ██╔══██║██╔══██╗   ██║   ╚═╝  │\n│  ╚██████╔╝██║  ██║██║ ╚═╝ ██║███████╗    ███████║   ██║   ██║  ██║██║  ██║   ██║   ██╗  │\n│   ╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝    ╚══════╝   ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═╝   ╚═╝   ╚═╝  │\n│                                                                                         │\n│                                                                                         │\n╚─────────────────────────────────────────────────────────────────────────────────────────╝");
                Console.WriteLine($"Welcome back {gameManager.Player.Name} to The Rare Treasure Hunter Game!");
                Console.WriteLine("You will act as an Archeologist and have received your quest!");
                Console.WriteLine("\nPress any key start the game.");
                Console.ReadKey();

                PlayGame(gameManager);
            }
        }
        else if (choice != 0)
        {
            Console.WriteLine("Invalid selection.");
            Console.WriteLine("Press any key to try again...");
            Console.ReadKey();
        }
    }
}