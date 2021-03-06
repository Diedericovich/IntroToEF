using IntroToEF.Data.Entities;
using IntroToEF.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

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
            //_samuraiRepo.AddSamurais();
            //_battleRepo.AddBattles();
            ShowMenu();
            Console.ReadLine();
        }

        public void ShowMenu()
        {
            Console.WriteLine("===============================================");
            Console.WriteLine("===============================================");
            
            Console.WriteLine("1. Add Samurai");
            Console.WriteLine("2. View details samurai");
            Console.WriteLine("3. Update Samurai");
            Console.WriteLine("4. Delete Samurai");
            Console.WriteLine("5. View details battle");
            Console.WriteLine("6. Exit application");
            Console.WriteLine();
            Console.WriteLine("===============================================");
            Console.WriteLine("Choose one of the options");

            int selection = Convert.ToInt32(Console.ReadLine());

            switch (selection)
            {
                case 1:
                    AskInputSamurai();
                    break;

                case 2:
                    ShowSamurai();
                    break;

                case 3:
                    UpdateInfoSamurai();
                    break;

                case 4:
                    ChooseSamuraiToDelete();
                    break;

                case 5:
                    ShowAllSamuraiInBattle();
                    break;
                case 6:
                    Environment.Exit(0);
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

            AddHorses(samurai);

            AddQuotes(samurai);

            AddBattles(samurai);

            _samuraiRepo.AddSamurai(samurai);
            ShowMenu();
        }

        public void UpdateInfoSamurai()
        {
            PrintListofSamurai(_samuraiRepo.GetSamurais());
            Console.WriteLine("Choose Samurai you want to edit by ID:");
            Samurai samurai = _samuraiRepo.GetSamurai(Convert.ToInt32(Console.ReadLine()));
            Console.WriteLine("Change the following info:");
            samurai.Name = AskName();
            Console.WriteLine("2. Dynasty:");
            samurai.Dynasty = Console.ReadLine();

            if (samurai.Horses.Count != 0)
            {
                PrintListofHorses(samurai.Horses);
                Console.WriteLine("Do you want to delete horses? Y/N");

                if (Console.ReadLine().ToLower() == "y")
                {
                    DeleteHorses(samurai);
                }
            }

            AddHorses(samurai);

            if (samurai.Quotes.Count != 0)
            {
                PrintListofQuotes(samurai.Quotes);
                Console.WriteLine("Do you want to delete quotes? Y/N");

                if (Console.ReadLine().ToLower() == "y")
                {
                    DeleteQuotes(samurai);
                }
            }

            AddQuotes(samurai);

            if (samurai.Battles.Count != 0)
            {
                PrintListofBattles(samurai.Battles);
                Console.WriteLine("Do you want to delete battles? Y/N");

                if (Console.ReadLine().ToLower() == "y")
                {
                    DeleteBattles(samurai);
                }
            }

            AddBattles(samurai);
            _samuraiRepo.AddSamurai(samurai);
            ShowMenu();
        }

        private Samurai AddBattles(Samurai samurai)
        {
            Console.WriteLine("Did the samurai fight in battles? Y/N");

            if (Console.ReadLine().ToLower() == "y")
            {
                PrintListofBattles(_battleRepo.GetBattles());
                samurai = AddMultipleBattles(samurai);
            }

            return samurai;
        }

        private void AddQuotes(Samurai samurai)
        {
            Console.WriteLine("Do you want to add quotes? Y/N");

            if (Console.ReadLine().ToLower() == "y")
            {
                AddMultipleQuotes(samurai);
            }
        }

        private void AddHorses(Samurai samurai)
        {
            Console.WriteLine("Do you want to add horses? Y/N");

            if (Console.ReadLine().ToLower() == "y")
            {
                AddMultipleHorses(samurai);
            }
        }

        private static void DeleteHorses(Samurai samurai)
        {
            Console.WriteLine("Enter the ID of the horse you want to delete");
            int id = Convert.ToInt32(Console.ReadLine());
            samurai.Horses.Remove(samurai.Horses.Where(x => x.Id == id).First());

            Console.WriteLine("Want to delete more horses? Y/N");

            if (Console.ReadLine().ToLower() == "y")
            {
                DeleteHorses(samurai);
            }
        }

        private static void DeleteQuotes(Samurai samurai)
        {
            Console.WriteLine("Enter the ID of the quote you want to delete");
            int id = Convert.ToInt32(Console.ReadLine());
            samurai.Quotes.Remove(samurai.Quotes.Where(x => x.Id == id).First());

            Console.WriteLine("Want to delete more quotes? Y/N");

            if (Console.ReadLine().ToLower() == "y")
            {
                DeleteQuotes(samurai);
            }
        }

        private static void DeleteBattles(Samurai samurai)
        {
            Console.WriteLine("Enter the ID of the battle you want to delete");
            int id = Convert.ToInt32(Console.ReadLine());
            samurai.Battles.Remove(samurai.Battles.Where(x => x.Id == id).First());

            Console.WriteLine("Want to delete more battles? Y/N");

            if (Console.ReadLine().ToLower() == "y")
            {
                DeleteBattles(samurai);
            }
        }

        private Samurai AddMultipleBattles(Samurai samurai)
        {
            Console.WriteLine();
            Console.WriteLine("Select existing battle or create new battle (+):");
            AddBattle(samurai);

            Console.WriteLine("Add more battles? (y/n)");
            if (Console.ReadLine().ToLower() == "y")
            {
                samurai = AddMultipleBattles(samurai);
            }
            return samurai;
        }

        private void AddBattle(Samurai samurai)
        {
            string input = Console.ReadLine();

            if (input == "+")
            {
                samurai.Battles.Add(AskInputBattle());
            }
            else
            {
                var battle = _battleRepo.GetBattle(Convert.ToInt32(input));
                if (battle != null)
                {
                    samurai.Battles.Add(battle);
                }
                else
                {
                    Console.WriteLine("** ERROR - BATTLE NOT FOUND **");
                }
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

        public void ShowSamurai()
        {
            List<Samurai> samuraiFiltered;
            Console.WriteLine("===============================================");

            Console.WriteLine("1. View all samurai");
            Console.WriteLine("2. View samurai with quotes");
            Console.WriteLine("3. View samurai with horses");
            Console.WriteLine("4. View samurai with battles");
            Console.WriteLine("5. Nevermind, take me back to the menu!");

            Console.WriteLine("===============================================");
            Console.WriteLine("Choose an option");

            switch (Convert.ToInt32(Console.ReadLine()))
            {
                case 1:
                    PrintListofSamurai(_samuraiRepo.GetSamurais());
                    ShowAllDetails();
                    break;
                case 2:
                    samuraiFiltered = _samuraiRepo.GetSamuraisWithQuotes();
                    CheckIfFilteredSamuraiIsEmpty(samuraiFiltered);
                    break;
                case 3:
                    samuraiFiltered = _samuraiRepo.GetSamuraisWithHorses();
                    CheckIfFilteredSamuraiIsEmpty(samuraiFiltered);
                    break;
                case 4:
                    samuraiFiltered = _samuraiRepo.GetSamuraisWithBattles();
                    CheckIfFilteredSamuraiIsEmpty(samuraiFiltered);

                    break;
                case 5:
                    ShowMenu();
                    break;
                default:
                    Console.WriteLine("Wrong input!");
                    ShowSamurai();
                    break;
            }
            
        }

        private void CheckIfFilteredSamuraiIsEmpty(List<Samurai> samuraiFiltered)
        {
            if (!samuraiFiltered.Any())
            {
                Console.WriteLine("There are no samurai with this filter!");
                ShowSamurai();
            }
            else
            {
                PrintListofSamurai(samuraiFiltered);
                ShowAllDetails();
            }
        }

        private void ShowAllDetails()
        {
            Console.WriteLine("Select samurai from list:");
            Samurai samurai = _samuraiRepo.GetSamuraiWithIncludedData(Convert.ToInt32(Console.ReadLine()));
            Console.WriteLine($"Name: {samurai.Name}, Dynasty: {samurai.Dynasty}");
            if (samurai.Quotes.Count != 0)
            {
                PrintListofQuotes(samurai.Quotes);
            }
            if (samurai.Horses.Count != 0)
            {
                PrintListofHorses(samurai.Horses);
            }
            if (samurai.Battles.Count != 0)
            {
                PrintListofBattles(samurai.Battles);
            }
            Console.WriteLine("Check different samurai? Y/N");
            if (Console.ReadLine().ToLower() == "y")
            {
                ShowSamurai();
            }
            ShowMenu();
        }

        public void ShowAllSamuraiInBattle()
        {
            PrintListofBattles(_battleRepo.GetBattles());

            var battle = _battleRepo.GetBattleWithSamurai(Convert.ToInt32(Console.ReadLine()));
            if (battle != null)
            {
                Console.WriteLine(battle.Name);
                PrintListofSamurai(battle.Samurai);
            }
            else
            {
                Console.WriteLine("Battle not found");
            }
        }

        public void RenameSamurai(int id, string name)
        {
            Samurai samuraiToBeUpdated = _samuraiRepo.GetSamurai(id);
            samuraiToBeUpdated.Name = name;
            _samuraiRepo.UpdateSamurai(samuraiToBeUpdated);
        }

        public void ChooseSamuraiToDelete()
        {
            PrintListofSamurai(_samuraiRepo.GetSamurais());
            Console.WriteLine("Choose Samurai to delete by id:");
            _samuraiRepo.DeleteSamurai(Convert.ToInt32(Console.ReadLine()));
            ShowMenu();
        }

        private void PrintListofSamurai(List<Samurai> samuraiList)
        {
            Console.WriteLine(" Samurai:");
            foreach (var samurai in samuraiList)
            {
                Console.WriteLine($"   {samurai.Id,4}. Name: {samurai.Name,22}, Dynasty: {samurai.Dynasty,10}");
            }
        }

        private void PrintListofHorses(List<Horse> horseList)
        {
            Console.WriteLine(" Horses:");
            foreach (var horse in horseList)
            {
                Console.WriteLine($"   {horse.Id}. Name: {horse.Name,15}, Age: {horse.Age,5}, Type: {(horse.IsWarHorse ? "War Horse" : "Domesticated"),15}");
            }
        }

        private void PrintListofQuotes(List<Quote> quoteList)
        {
            Console.WriteLine(" Quotes:");
            foreach (var quote in quoteList)
            {
                Console.WriteLine($"   {quote.Id}. '{quote.Text}'");
            }
        }

        private void PrintListofBattles(List<Battle> battlesList)
        {
            Console.WriteLine(" Battles:");
            foreach (var battle in battlesList)
            {
                Console.WriteLine($"   {battle.Id}. Name: {battle.Name,20}, Year: {battle.Year,5}, Location: {battle.Location,15}");
            }
        }
    }
}