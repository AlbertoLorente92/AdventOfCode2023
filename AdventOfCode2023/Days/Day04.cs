using AdventOfCode2023.Interfaces;
using System.Collections.Generic;
using System.Text;
using static AdventOfCode2023.Days.Day02.Game;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode2023.Days
{
    internal sealed class Day04 : IDay
    {
        public static int DayNumber => 4;

        void IDay.Part1()
        {
            Console.WriteLine(Day(@"Tests\Day" + DayNumber + @"\Part1\test1.txt", printLines: true, executePart1: true));
            Console.WriteLine(Day(@"Tests\Day" + DayNumber + @"\Part1\test2.txt", printLines: false, executePart1: true));
        }

        void IDay.Part2()
        {
            Console.WriteLine(Day(@"Tests\Day" + DayNumber + @"\Part1\test1.txt", printLines: true, executePart1: false));
            Console.WriteLine(Day(@"Tests\Day" + DayNumber + @"\Part1\test2.txt", printLines: false, executePart1: false));
        }
        private static void PrintLines(string[] lines)
        {
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
        }

        private static List<Game> GetGamesFromLines(string[] lines)
        {
            List<Game> games = new();
            string[] game;
            string[] numbers;
            foreach (string line in lines)
            {
                game = line.Split(":");
                numbers = game[1].Split("|");
                games.Add(new Game(
                    Id: int.Parse(game[0].Replace("Card", "").Trim())
                    , NumbersYouHave: numbers[0].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(s => int.TryParse(s.Trim(), out var n) ? (int?)n : null)
                            .Where(x => x != null && x != 0)
                            .Select(i => i.Value)
                            .ToList()
                    , WinningNumbers: numbers[1].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(s => int.TryParse(s.Trim(), out var n) ? (int?)n : null)
                            .Where(x => x != null && x != 0)
                            .Select(i => i.Value)
                            .ToList()
                    ));
            }
            return games;
        }

        private static int DoubleIt(int number)
        {
            if (number <= 0)
                return 0;

            int result = 1;
            number--;
            for (int i = 0; i < number; i++) {
                result *= 2;
            }
            return result;
        }

        public static int NumberOfScratchcards(List<Game> games)
        {
            Dictionary<int,int> NumberOfScratchcardsForEveryCard = new();
            for(int i = 0; i < games.Count; i++)
            {
                NumberOfScratchcardsForEveryCard.Add(i, 1);
            }

            int aux;
            for(int i = 0; i < games.Count; i++)
            {
                aux = games[i].NumbersYouHaveInWinningNumbers();
                for(int j = 1; j <= aux; j++)
                {
                    NumberOfScratchcardsForEveryCard[i + j] += 1 * NumberOfScratchcardsForEveryCard[i];
                }
            }

            int numberOfScratchcards = 0;
            for (int i = 0; i < games.Count; i++)
            {
                numberOfScratchcards += NumberOfScratchcardsForEveryCard[i];
            }

            return numberOfScratchcards;
        }

        private static int Day(string file, bool printLines, bool executePart1)
        {
            string[] lines = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, file));

            List<Game> games = GetGamesFromLines(lines);

            if (printLines)
            {
                PrintLines(lines);
            }

            if (executePart1)
            {
                int total = 0;

                foreach(Game game in games) {
                    total += DoubleIt(game.NumbersYouHaveInWinningNumbers());
                }

                return total;
            }
            else
            {
                return NumberOfScratchcards(games);
            }
        }

        internal sealed class Game
        {
            public int Id { get; set; }
            public List<int> NumbersYouHave { get; set; }
            public List<int> WinningNumbers { get; set; }
            public Game(int Id, List<int> NumbersYouHave, List<int> WinningNumbers)
            {
                this.Id = Id;
                this.NumbersYouHave = NumbersYouHave;
                this.WinningNumbers = WinningNumbers;
            }

            public int NumbersYouHaveInWinningNumbers()
            {
                int coincidenceNumbers = 0;
                foreach(int number in NumbersYouHave)
                {
                    if (WinningNumbers.Contains(number))
                    {
                        coincidenceNumbers++;
                    }
                }
                return coincidenceNumbers;
            }
        }
    }
}
