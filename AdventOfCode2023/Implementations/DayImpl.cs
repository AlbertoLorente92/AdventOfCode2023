using AdventOfCode2023.Interfaces;

namespace AdventOfCode2023.Implementations
{
    internal sealed class DayImpl : IExecuteDay
    {
        private readonly IDayFactory _operatorFactory;

        public DayImpl(IDayFactory operatorFactory)
        {
            this._operatorFactory = operatorFactory;
        }

        public void Execute(int day, int part)
        {
            if (part == 1) {
                _operatorFactory.GetOperator(day).Part1();
            }
            if (part == 2)
            {
                _operatorFactory.GetOperator(day).Part2();
            }
        }
    }
}
