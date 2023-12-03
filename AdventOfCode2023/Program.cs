using AdventOfCode2023.Implementations;
using AdventOfCode2023.Interfaces;

IDayFactory operatorFactory = new DayFactoryImpl();
IExecuteDay calculator = new DayImpl(operatorFactory);
//calculator.Execute(day: 1, part: 1);
//calculator.Execute(day: 1, part: 2);
//calculator.Execute(day: 2, part: 1);
//calculator.Execute(day: 2, part: 2);
//calculator.Execute(day: 3, part: 1);
calculator.Execute(day: 3, part: 2);