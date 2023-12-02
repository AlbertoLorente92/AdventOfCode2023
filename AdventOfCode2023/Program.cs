using AdventOfCode2023;
using AdventOfCode2023.Factory;

IDayFactory operatorFactory = new DayFactoryImpl();
IExecuteDay calculator = new DayImpl(operatorFactory);
calculator.Execute(day: 1, part: 1);
calculator.Execute(day: 1, part: 2);
