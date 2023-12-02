using AdventOfCode2023.Days;
using AdventOfCode2023.Interfaces;

namespace AdventOfCode2023.Implementations
{
    internal sealed class DayFactoryImpl : IDayFactory
    {
        private static Dictionary<int, Type> _days;
        static DayFactoryImpl()
        {
            _days = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => !t.IsInterface && typeof(IDay).IsAssignableFrom(t))
                .ToDictionary(keySelector: t => (int)(t.GetProperty(nameof(IDay.DayNumber))!.GetValue(null) ?? 0), t => t);
        }
        public IDay GetOperator(int day) => (IDay)(Activator.CreateInstance(_days[day]) ?? new Day0X());
    }
}
