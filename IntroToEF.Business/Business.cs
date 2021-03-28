using IntroToEF.Data.Entities;
using IntroToEF.Data.Repositories;
using System;
using System.Collections.Generic;

namespace IntroToEF.Business
{
    public class Business
    {
        // Composition
        private ISamuraiRepo _samuraiRepo;
        private IBattleRepo _battleRepo;
        private IHorseRepo _horseRepo;
        private IQuoteRepo _quoteRepo;

        public Business()
        {
            _samuraiRepo = new SamuraiRepo();
            _horseRepo = new HorseRepo();
            _quoteRepo = new QuoteRepo();
            _battleRepo = new BattleRepo();
        }

        public void RunApp()
        {
            //Console.WriteLine("Hello. Please enter a samurai");
            //string name = Console.ReadLine();

            //_samuraiRepo.AddSamurai(name);
            //_samuraiRepo.AddSamurais();

            //_horseRepo.AddHorse();
            // _horseRepo.AddHorses();

            //_quoteRepo.AddQuote();
            //_quoteRepo.AddQuotes();

            //AddSamuraiWithQuotes();

            //GetAllSamurais();
            //GetAllQuotes();
            //GetAllHorses();

            //_samuraiRepo.GetSamurai(3);

            //RenameSamurai(9, "Renamed From App");
            //RenameMultipleSamurais();

            //RemoveSamurai(3);

            // AddSamuraiWhoFoughtInBattles();
            //var sam = GetSamuraiWithBattles(7);
            // var SPResult = GetSamuraiWhoSaidAWord("thank");

            ShowMenu();

            Console.ReadLine();
        }

        public void AddSamuraiWithQuotes()
        {
            var samurai = new Samurai
            {
                Name = "Suote Samurai 2",
                Dynasty = "Sengoku",
                Quotes = new List<Quote>
                {
                    new Quote
                    {
                        Text = "I have saved your life. Now will you feed me?"
                    },
                    new Quote
                    {
                        Text = "Thank you for feeding me!"
                    }
                },
                Horses = new List<Horse>
                {
                    new Horse
                    {
                        IsWarHorse = true,
                        Name = "Roach",
                    },
                    new Horse
                    {
                        IsWarHorse = false,
                        Name = "Fluff",
                    },
                }
            };
            _samuraiRepo.AddSamurai(samurai);
        }

        public void GetAllSamurais()
        {
            var samurais = _samuraiRepo.GetSamurais();
        }

        public void GetAllQuotes()
        {
            var quotes = _quoteRepo.GetQuotes();
        }

        public void GetAllHorses()
        {
            var horses = _horseRepo.GetHorses();
        }

        public void RenameSamurai(int id, string name)
        {
            Samurai samuraiToBeUpdated = _samuraiRepo.GetSamurai(id);
            samuraiToBeUpdated.Name = name;
            _samuraiRepo.UpdateSamurai(samuraiToBeUpdated);
        }

        public void RenameMultipleSamurais()
        {
            _samuraiRepo.UpdateSamurais();
        }

        public void RemoveSamurai(int id)
        {
            _samuraiRepo.DeleteSamurai(id);
        }

        public void AddSamuraiWhoFoughtInBattles()
        {
            Samurai veteran = new Samurai
            {
                Name = "A weary broken man",
                Battles = new List<Battle>
                {
                    new Battle
                    {
                        Name = "Okinagwa",
                        Year = 1557
                    },
                    new Battle
                    {
                        Name = "Fukushima",
                        Year = 2011
                    },
                }
            };
            _samuraiRepo.AddSamurai(veteran);
        }

        public Samurai GetSamuraiWithBattles(int id)
        {
            return _samuraiRepo.GetSamurai(id);
        }

        public List<Samurai> GetSamuraiWhoSaidAWord(string word)
        {
            var result = _samuraiRepo.GetResultFromStoredProcedure(word);
            return result;
        }

        public void ShowMenu()
        {
            Console.WriteLine("===============================================");
            Console.WriteLine("===============================================");
            Console.WriteLine("===============================================");

            Console.WriteLine("1. Add Samurai");
            Console.WriteLine("2. View details samurai");
            Console.WriteLine("3. Update Samurai");
            Console.WriteLine("4. Delete Samurai");
            Console.WriteLine();
            Console.WriteLine("Choose one of the options");

            int selection = Convert.ToInt32(Console.ReadLine());

            switch (selection)
            {
                case 1:
                    AskInputSamurai();
                    break;

                case 2:
                    ShowAllDetailsSamurai();
                    break;

                case 3:

                    break;

                case 4:
                    DeleteSamurai();
                    break;

                default:
                    break;
            }
        }

        public void AskInputSamurai()
        {
            Samurai samurai = new Samurai();

            Console.WriteLine("Enter the following information(required *)");
            samurai.Name = AskName();
            Console.WriteLine("2. Dynasty:");
            samurai.Dynasty = Console.ReadLine();
            Console.WriteLine("Does the samurai have horses? Y/N");

            if (Console.ReadLine().ToLower() == "y")
            {
                AddMultipleHorses(samurai);
            }

            Console.WriteLine("Does the samurai have quotes? Y/N");


            if (Console.ReadLine().ToLower() == "y")
            {
                 AddMultipleQuotes(samurai);
            }

            Console.WriteLine("Did the samurai fight in battles? Y/N");
           
            if (Console.ReadLine().ToLower() == "y")
            {
                PrintExistingBattles();
                samurai = AddMultipleBattles(samurai);
            }
            _samuraiRepo.AddSamurai(samurai);
            ShowMenu();
        }

        private Samurai AddMultipleBattles(Samurai samurai)
        {
            Console.WriteLine();
            Console.WriteLine("Select existing battle or create new battle (+):");

            string input = Console.ReadLine();

            var battle = _battleRepo.GetBattle(Convert.ToInt32(input));

            if (input == "+")
            {
                samurai.Battles.Add(AskInputBattle());
            }
            else if (battle != null)
            {
                samurai.Battles.Add(battle);
            }
            else
            {
                Console.WriteLine("** ERROR - BATTLE NOT FOUND **");
            }
            Console.WriteLine("Add more battles? (y/n)");
            if (Console.ReadLine().ToLower() == "y")
            {
                samurai = AddMultipleBattles(samurai);
            }
            return samurai;
        }


        private void PrintExistingSamurais()
        {
            Console.WriteLine("Existing Samurais:");
            foreach (var samurai in _samuraiRepo.GetSamurais())
            {
                Console.WriteLine($"{samurai.Id}. {samurai.Name}");
            }
        }

        private void PrintExistingBattles()
        {
            Console.WriteLine("Existing Battles:");
            foreach (var battle in _battleRepo.GetBattles())
            {
                Console.WriteLine($"{battle.Id}. {battle.Name}, {battle.Year}, {battle.Location}");
            }
        }

        private Battle AskInputBattle()
        {
            Battle battle = new Battle();
            battle.Name = AskName();

            Console.WriteLine(" Enter the location of the battle:");
            battle.Location = Console.ReadLine();

            Console.WriteLine(" Enter the year:");
            battle.Year = Convert.ToInt32(Console.ReadLine());
            
            return battle;

        }

        private void AddMultipleHorses(Samurai samurai)
        {
            samurai.Horses.Add(AskInputHorse());
            Console.WriteLine("Add more horses? Y/N");
            if (Console.ReadLine().ToLower() == "y")
            {
                AddMultipleHorses(samurai);
            }
        }

        public string AskName()
        {
            Console.WriteLine("* Enter the name:");
            string name = Console.ReadLine();
            if (name == "")
            {
                Console.WriteLine("Name is required, please enter the name.");
                AskName();
            }
            return name;
        }

        public Horse AskInputHorse()
        {
            Horse horse = new Horse();
            horse.Name = AskName();

            Console.WriteLine("* Enter the age of the horse:");
            horse.Age = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("* Is this horse a warhorse? Y/N");
            if (Console.ReadLine().ToLower() == "y")
            {
                horse.IsWarHorse = true;
            }
            return horse;
        }

        private void AddMultipleQuotes(Samurai samurai)
        {
            samurai.Quotes.Add(AskInputQuote());
            Console.WriteLine("Add more quotes? Y/N");
            if (Console.ReadLine().ToLower() == "y")
            {
                AddMultipleQuotes(samurai);
            }
        }

        public Quote AskInputQuote()
        {
            Quote quote = new Quote();
            Console.WriteLine("* Enter your quote:");
            quote.Text = Console.ReadLine();
            return quote;
        }


        public void ShowAllDetailsSamurai ()
        {
            PrintExistingSamurais();
            Console.WriteLine("Select samurai from list:");
            Samurai samurai =_samuraiRepo.GetSamuraiWithIncludedData(Convert.ToInt32(Console.ReadLine()));
            Console.WriteLine(samurai.Name, samurai.Dynasty);

        }

        public void DeleteSamurai()
        {
            PrintExistingSamurais();
            Console.WriteLine("Select the samurai you wish to remove:");
            _samuraiRepo.DeleteSamurai(Convert.ToInt32(Console.Read()));
        }

    }
}