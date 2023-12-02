using AdventOfCode2023.Days;

namespace AdventOfCode2023.Factory
{
    internal sealed class DayFactoryImpl : IDayFactory
    {
        private static Dictionary<int, Type> _days;
        static DayFactoryImpl()
        {
            _days = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => !t.IsInterface && typeof(IDay).IsAssignableFrom(t))
                .ToDictionary(t => (int)t.GetProperty(nameof(IDay.DayNumber)).GetValue(null), t => t);
        }
        public IDay GetOperator(int day) => (IDay)Activator.CreateInstance(_days[day]);
    }
}
