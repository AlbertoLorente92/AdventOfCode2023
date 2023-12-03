using AdventOfCode2023.Interfaces;
using System.Text;
using static AdventOfCode2023.Days.Day02.Game;

namespace AdventOfCode2023.Days
{
    internal sealed class Day3 : IDay
    {
        public static int DayNumber => 3;

        void IDay.Part1()
        {
            Console.WriteLine(Day(@"Tests\Day"+DayNumber+@"\Part1\test1.txt", printLines: true, executePart1: true));
            Console.WriteLine(Day(@"Tests\Day"+DayNumber+@"\Part1\test2.txt", printLines: false, executePart1: true));
        }

        void IDay.Part2()
        {
            Console.WriteLine(Day(@"Tests\Day"+DayNumber+@"\Part1\test1.txt", printLines: true, executePart1: false));
            Console.WriteLine(Day(@"Tests\Day"+DayNumber+@"\Part1\test2.txt", printLines: false, executePart1: false));
        }
        private static void PrintLines(string[] lines)
        {
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
        }

        private static bool IsAdjacentToASimbol(char[][] engineSchematic, int i, int j, StringBuilder currentNumber)
        {
            bool isAdjacent = false;
            int x = j - currentNumber.Length - 1;
            if ((engineSchematic[i][j] != '.') || (x >= 0 && x < engineSchematic[0].Length && !char.IsDigit(engineSchematic[i][x]) && engineSchematic[i][x] != '.'))
            {
                return true;
            }
            for (x = j - currentNumber.Length - 1; x <= j; x++)
            {
                if ((i-1 >= 0 && x >= 0 && x < engineSchematic[0].Length && !char.IsDigit(engineSchematic[i-1][x]) && engineSchematic[i-1][x] != '.')
                    || (i + 1 < engineSchematic.Length && x >= 0 && x < engineSchematic[0].Length && !char.IsDigit(engineSchematic[i + 1][x]) && engineSchematic[i + 1][x] != '.'))
                {
                    return true;
                }
            }
            return isAdjacent;
        }

        private static List<int> NumbersAdjacentToASimbol(char[][] engineSchematic)
        {
            List<int> numbersAdjacentToASimbol = new();
            StringBuilder currentNumber;
            for(int i = 0; i < engineSchematic.Length; i++)
            {
                currentNumber = new();
                for (int j = 0;j < engineSchematic[i].Length; j++)
                {
                    if (char.IsNumber(engineSchematic[i][j]))
                    {
                        currentNumber.Append(engineSchematic[i][j].ToString());
                    }
                    else
                    {
                        if (currentNumber.ToString() != string.Empty && IsAdjacentToASimbol(engineSchematic, i, j, currentNumber))
                        {
                            numbersAdjacentToASimbol.Add(int.Parse(currentNumber.ToString()));
                        }

                        currentNumber = new();
                    }
                }
            }
            return numbersAdjacentToASimbol;
        }

        private static int ReadNumberToTheLeft(char[][] engineSchematic, int i, int j)
        {
            StringBuilder sb = new();
            sb.Append(engineSchematic[i][j]);
            for (int x = j - 1; x >= 0; x--)
            {
                if (char.IsNumber(engineSchematic[i][x]))
                {
                    sb.Insert(0, engineSchematic[i][x]);
                }
                else
                {
                    break;
                }
            }
            return int.Parse(sb.ToString());
        }

        private static int ReadNumberInTheMiddle(char[][] engineSchematic, int i, int j)
        {
            StringBuilder sb = new();
            sb.Append(engineSchematic[i][j]);
            for (int x = j - 1; x >= 0; x--)
            {
                if (char.IsNumber(engineSchematic[i][x]))
                {
                    sb.Insert(0, engineSchematic[i][x]);
                }
                else
                {
                    break;
                }
            }
            for (int x = j + 1; x < engineSchematic[i - 1].Length; x++)
            {
                if (char.IsNumber(engineSchematic[i][x]))
                {
                    sb.Append(engineSchematic[i][x]);
                }
                else
                {
                    break;
                }
            }
            return int.Parse(sb.ToString());
        }

        private static int ReadNumberToTheRight(char[][] engineSchematic, int i, int j)
        {
            StringBuilder sb = new();
            sb.Append(engineSchematic[i][j]);
            for (int x = j + 1; x < engineSchematic[i].Length; x++)
            {
                if (char.IsNumber(engineSchematic[i][x]))
                {
                    sb.Append(engineSchematic[i][x]);
                }
                else
                {
                    break;
                }
            }
            return int.Parse(sb.ToString());
        }


        private static int GetGearRatio(char[][] engineSchematic, int i, int j)
        {
            List <int> numbersAroundTheAsterisk = new();
            bool topLeft, topMiddle, topRight;
            bool left, right;
            bool bottomLeft, bottomMiddle, bottomRight;

            topLeft   = char.IsNumber(engineSchematic[i - 1][j - 1]);
            topMiddle = char.IsNumber(engineSchematic[i - 1][j]);
            topRight  = char.IsNumber(engineSchematic[i - 1][j + 1]);

            left  = char.IsNumber(engineSchematic[i][j - 1]);
            right = char.IsNumber(engineSchematic[i][j + 1]);

            bottomLeft   = char.IsNumber(engineSchematic[i + 1][j - 1]);
            bottomMiddle = char.IsNumber(engineSchematic[i + 1][j]);
            bottomRight  = char.IsNumber(engineSchematic[i + 1][j + 1]);

            // check the left side
            if (left)
            {
                numbersAroundTheAsterisk.Add(ReadNumberToTheLeft(engineSchematic, i, j - 1));
            }
            // check the right side
            if (right)
            {
                numbersAroundTheAsterisk.Add(ReadNumberToTheRight(engineSchematic, i, j + 1));
            }

            //check top side
            if (!topMiddle)
            {
                if (topLeft)
                {
                    numbersAroundTheAsterisk.Add(ReadNumberToTheLeft(engineSchematic, i - 1, j - 1));
                }
                if (topRight)
                {
                    numbersAroundTheAsterisk.Add(ReadNumberToTheRight(engineSchematic, i - 1, j + 1));
                }
            }
            else
            {
                numbersAroundTheAsterisk.Add(ReadNumberInTheMiddle(engineSchematic, i - 1, j));
            }

            //check bottom side
            if (!bottomMiddle)
            {
                if (bottomLeft)
                {
                    numbersAroundTheAsterisk.Add(ReadNumberToTheLeft(engineSchematic, i + 1, j - 1));
                }
                if (bottomRight)
                {
                    numbersAroundTheAsterisk.Add(ReadNumberToTheRight(engineSchematic, i + 1, j + 1));
                }
            }
            else
            {
                numbersAroundTheAsterisk.Add(ReadNumberInTheMiddle(engineSchematic, i + 1, j));
            }


            if (numbersAroundTheAsterisk.Count == 2)
            {
                return numbersAroundTheAsterisk[0] * numbersAroundTheAsterisk[1];
            }
            else
            {
                return 0;
            }
        }

        private static List<int> GearsRatio(char[][] engineSchematic)
        {
            List<int> numbersAdjacentToASimbol = new();
            int gearsRatio;
            for (int i = 1; i < engineSchematic.Length - 1; i++)
            {
                for (int j = 1; j < engineSchematic[i].Length - 1; j++)
                {
                    if (engineSchematic[i][j] == '*')
                    {
                        gearsRatio = GetGearRatio(engineSchematic, i, j);
                        if (gearsRatio != 0)
                        {
                            numbersAdjacentToASimbol.Add(gearsRatio);
                        }
                    }
                }
            }
            return numbersAdjacentToASimbol;
        }

        private static char[][] CreateEngineSchematicMap(string[] lines)
        {
            // To make everything easier we will surround the map in '.' chars
            char[][] engineSchematic = new char[lines.Length + 2][];    // Thats why +2
            engineSchematic[0] = new char[lines[0].Length + 2];
            engineSchematic[lines.Length + 1] = new char[lines[0].Length + 2];
            for (int i = 0; i <= lines.Length + 1; i++)
            {
                engineSchematic[0][i] = '.';
                engineSchematic[lines.Length + 1][i] = '.';
            }
            for (int i = 1; i <= lines.Length; i++)
            {
                engineSchematic[i] = ("." + lines[i - 1].ToString() + ".").ToCharArray();
            }
            return engineSchematic;
        }

        private static int Day(string file, bool printLines, bool executePart1)
        {
            string[] lines = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, file));

            char[][] engineSchematic = CreateEngineSchematicMap(lines);

            if (printLines)
            {
                PrintLines(lines);
            }

            if (executePart1)
            {
                return NumbersAdjacentToASimbol(engineSchematic).Sum();
            }
            else
            {
                return GearsRatio(engineSchematic).Sum();
            }
        }
    }
}
