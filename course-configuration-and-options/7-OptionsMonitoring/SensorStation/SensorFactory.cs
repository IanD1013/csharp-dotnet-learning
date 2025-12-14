namespace _7_OptionsMonitoring.SensorStation;

internal sealed class SensorFactory
{
    private readonly Dictionary<string, ISensorService> _sensors =
        new(comparer: StringComparer.OrdinalIgnoreCase);

    public ISensorService Create(string name)
    {
        if (_sensors.TryGetValue(name, out var service))
        {
            return service;
        }

        return _sensors[name] = name switch
        {
            _ => new FakeSensorService()
        };
    }
}