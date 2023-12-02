﻿using System.Text;

namespace AdventOfCode2023.Days
{
    internal sealed class Day01 : IDay
    {
        public static int DayNumber => 1;

        void IDay.Part1()
        {
            Console.WriteLine(Day1(@"Tests\Day1\Part1\test1.txt", printLines: true, executePart1: true));
            Console.WriteLine(Day1(@"Tests\Day1\Part1\test2.txt", printLines: false, executePart1: true));
        }

        void IDay.Part2()
        {
            Console.WriteLine(Day1(@"Tests\Day1\Part2\test1.txt", printLines: true, executePart1: false));
            Console.WriteLine(Day1(@"Tests\Day1\Part1\test2.txt", printLines: false, executePart1: false));
        }
        private void PrintLines(string[] lines)
        {
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
        }
        private int GetCalibrationValuePart1(string line)
        {
            List<char> digits = line.Where(character => char.IsDigit(character)).ToList();
            StringBuilder FirstAndLastDigit = new StringBuilder().Append(digits.First()).Append(digits.Last());
            return int.Parse(FirstAndLastDigit.ToString());
        }

        Dictionary<string, string> CreateDictionaryWithNumbers()
        {
            Dictionary<string, string> digits = new()
        {
            { "1"    , "1" },
            { "2"    , "2" },
            { "3"    , "3" },
            { "4"    , "4" },
            { "5"    , "5" },
            { "6"    , "6" },
            { "7"    , "7" },
            { "8"    , "8" },
            { "9"    , "9" },
            { "one"  , "1" },
            { "two"  , "2" },
            { "three", "3" },
            { "four" , "4" },
            { "five" , "5" },
            { "six"  , "6" },
            { "seven", "7" },
            { "eight", "8" },
            { "nine" , "9" }
        };
            return digits;
        }

        string GetFirstDigitPart2(Dictionary<string, string> digits, string line)
        {
            int firstDigitIndex = line.Length;
            string firstDigit = string.Empty;
            int index;
            foreach (string number in digits.Keys)
            {
                index = line.IndexOf(number);
                if (index != -1 && index < firstDigitIndex)
                {
                    firstDigitIndex = index;
                    firstDigit = number;
                }
            }
            return digits[firstDigit];
        }
        private string GetLastDigitPart2(Dictionary<string, string> digits, string line)
        {
            int LastDigitIndex = -1;
            string LastDigit = string.Empty;
            int index;
            foreach (string number in digits.Keys)
            {
                index = line.LastIndexOf(number);
                if (index != -1 && index > LastDigitIndex)
                {
                    LastDigitIndex = index;
                    LastDigit = number;
                }
            }
            return digits[LastDigit];
        }

            private int GetCalibrationValuePart2(string line)
        {
            Dictionary<string, string> digits = CreateDictionaryWithNumbers();
            return int.Parse(new StringBuilder().Append(GetFirstDigitPart2(digits, line)).Append(GetLastDigitPart2(digits, line)).ToString());
        }

        private int Day1(string file, bool printLines, bool executePart1)
        {
            string[] lines = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, file));

            if (printLines)
            {
                PrintLines(lines);
            }

            if (executePart1)
            {
                return lines.Select(line => GetCalibrationValuePart1(line)).Sum();
            }
            else
            {
                return lines.Select(line => GetCalibrationValuePart2(line)).Sum();
            }
        }
    }
}
