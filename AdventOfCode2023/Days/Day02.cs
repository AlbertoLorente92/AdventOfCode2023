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
        private void PrintLines(string[] lines)
        {
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
        }

        private List<Game> GetGamesFromLines(string[] lines)
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

        private List<int> GamesIdThatArePosible(List<Game> games, Round elfBag)
        {           
            return games.Where(game => game.IsPosible(elfBag)).Select(game => game.id).ToList(); 
        }

        private List<Round> FewestColorsEachGame(List<Game> games)
        {
            return games.Select(game => game.FewestColorsFromGame()).ToList();
        }

        private int Day(string file, bool printLines, bool executePart1)
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
                return FewestColorsEachGame(games).Select(round => round.red * round.green * round.blue).Sum();
            }
        }
        internal sealed class Game
        {
            public int id { get; set; }
            public List<Round> rounds { get; set; }

            public Game(int id, List<Round> rounds)
            {
                this.id = id;
                this.rounds = rounds;
            }
            public bool IsPosible(Round elfbag)
            {
                foreach (Round round in rounds)
                {
                    if (round.green > elfbag.green || round.red > elfbag.red || round.blue > elfbag.blue)
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
                foreach (Round round in rounds)
                {
                    if (round.green > green)
                    {
                        green = round.green;
                    }
                    if (round.red > red)
                    {
                        red = round.red;
                    }
                    if (round.blue > blue)
                    {
                        blue = round.blue;
                    }
                }
                return new Round(red: red, green: green, blue: blue);
            }
            internal sealed class Round
            {
                public int blue { get; set; }
                public int red { get; set; }
                public int green { get; set; }
                public Round(int blue, int red, int green) {
                    this.blue = blue;
                    this.red = red;
                    this.green = green;
                }
            }
        }
    }
}
