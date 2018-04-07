public class Alarm : IAlarm
{
    private const double LowPressureThreshold = 17;
    private const double HighPressureThreshold = 21;

    private readonly ISensor _sensor;

    public Alarm(ISensor sensor)
    {
        _sensor = sensor;
    }

    public void Check()
    {
        double psiPressureValue = _sensor.PopNextPressurePsiValue();

        if (psiPressureValue < LowPressureThreshold ||
            HighPressureThreshold < psiPressureValue)
        {
            AlarmOn = true;
        }
    }

    public bool AlarmOn { get; private set; } = false;
}
