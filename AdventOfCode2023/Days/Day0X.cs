using AdventOfCode2023.Interfaces;

namespace AdventOfCode2023.Days
{
    internal sealed class Day0X : IDay
    {
        public static int DayNumber => 0;

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


        private int Day(string file, bool printLines, bool executePart1)
        {
            string[] lines = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, file));

            // What we should do with data?

            if (printLines)
            {
                PrintLines(lines);
            }

            if (executePart1)
            {
                return 0;
            }
            else
            {
                return 0;
            }
        }
    }
}
