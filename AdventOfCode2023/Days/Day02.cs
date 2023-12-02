using AdventOfCode2023.Interfaces;
using static AdventOfCode2023.Days.Day02.Game;

namespace AdventOfCode2023.Days
{
    internal sealed class Day02 : IDay
    {
        public static int DayNumber => 2;

        void IDay.Part1()
        {
            Console.WriteLine(Day(@"Tests\Day"+DayNumber+@"\Part1\test1.txt", printLines: true, executePart1: true));
            Console.WriteLine(Day(@"Tests\Day"+DayNumber+@"\Part1\test2.txt", printLines: false, executePart1: true));
        }

        void IDay.Part2()
        {
            Console.WriteLine(Day(@"Tests\Day"+DayNumber+@"\Part2\test1.txt", printLines: true, executePart1: false));
            Console.WriteLine(Day(@"Tests\Day"+DayNumber+@"\Part1\test2.txt", printLines: false, executePart1: false));
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
            List<Round> roundsGame;
            string[] gameIdentifier;
            string[] gameRounds;
            string[] gameColors;
            int red, green, blue;
            foreach (string line in lines)
            {
                gameIdentifier = line.Split(":");
                gameRounds = gameIdentifier[1].Split(";");
                roundsGame = new List<Round>();
                foreach (string rounds in gameRounds)
                {
                    gameColors = rounds.Split(",");
                    red = green = blue = 0;
                    foreach (string color in gameColors)
                    {
                        if (color.Contains("blue"))
                        {
                            blue = int.Parse(color.Trim().Split(" ")[0]);
                        }
                        if (color.Contains("red"))
                        {
                            red = int.Parse(color.Trim().Split(" ")[0]);
                        }
                        if (color.Contains("green"))
                        {
                            green = int.Parse(color.Trim().Split(" ")[0]);
                        }
                    }
                    roundsGame.Add(new Round(red: red, green: green, blue: blue));
                }
                games.Add(new Game(int.Parse(gameIdentifier[0].Split(" ")[1]), roundsGame));
            }
            return games;
        }

        private static List<int> GamesIdThatArePosible(List<Game> games, Round elfBag)
        {           
            return games.Where(game => game.IsPosible(elfBag)).Select(game => game.Id).ToList(); 
        }

        private static List<Round> FewestColorsEachGame(List<Game> games)
        {
            return games.Select(game => game.FewestColorsFromGame()).ToList();
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
                return GamesIdThatArePosible(games, new Round(red: 12, green: 13, blue: 14)).Sum();
            }
            else
            {
                return FewestColorsEachGame(games).Select(round => round.Red * round.Green * round.Blue).Sum();
            }
        }
        internal sealed class Game
        {
            public int Id { get; set; }
            public List<Round> Rounds { get; set; }

            public Game(int id, List<Round> rounds)
            {
                this.Id = id;
                this.Rounds = rounds;
            }
            public bool IsPosible(Round elfbag)
            {
                foreach (Round round in Rounds)
                {
                    if (round.Green > elfbag.Green || round.Red > elfbag.Red || round.Blue > elfbag.Blue)
                    {
                        return false;
                    }
                }
                return true;
            }
            public Round FewestColorsFromGame()
            {
                int red, green, blue;
                red = green = blue = 0;
                foreach (Round round in Rounds)
                {
                    if (round.Green > green)
                    {
                        green = round.Green;
                    }
                    if (round.Red > red)
                    {
                        red = round.Red;
                    }
                    if (round.Blue > blue)
                    {
                        blue = round.Blue;
                    }
                }
                return new Round(red: red, green: green, blue: blue);
            }
            internal sealed class Round
            {
                public int Blue { get; set; }
                public int Red { get; set; }
                public int Green { get; set; }
                public Round(int blue, int red, int green) {
                    this.Blue = blue;
                    this.Red = red;
                    this.Green = green;
                }
            }
        }
    }
}
