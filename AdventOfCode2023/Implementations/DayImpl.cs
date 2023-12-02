using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Factory
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
            _operatorFactory.GetOperator(day).Part1();
        }
    }
}
