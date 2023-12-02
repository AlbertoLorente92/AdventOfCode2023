using AdventOfCode2023.Days;

namespace AdventOfCode2023
{
    internal interface IDayFactory
    {
        IDay GetOperator(int day);
    }
}
