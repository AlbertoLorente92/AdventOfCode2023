using System.Text;

void PrintLines(string[] lines)
{
    foreach (string line in lines)
    {
        Console.WriteLine(line);
    }
}

int GetCalibrationValue(string line)
{
    List<char> digits = line.Where(character => char.IsDigit(character)).ToList();
    StringBuilder FirstAndLastDigit = new StringBuilder().Append(digits.First()).Append(digits.Last());
    return int.Parse(FirstAndLastDigit.ToString());
}

int Day1(string file, bool showLines)
{
    string[] lines = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, file));

    if (showLines)
    {
        PrintLines(lines);
    }

    return lines.Select(line => GetCalibrationValue(line)).Sum();
}


Console.WriteLine(Day1(@"Tests\Day1\test1.txt", true));
Console.WriteLine(Day1(@"Tests\Day1\test2.txt", false));