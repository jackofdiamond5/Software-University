using Moq;
using NUnit.Framework;

public class PressureTester
{
    private const double LowPressureThreshold = 17;
    private const double HighPressureThreshold = 21;

    private Mock<ISensor> fakeSensor;
    private IAlarm alarm;

    [SetUp]
    public void FacilitateEntities()
    {
        fakeSensor = new Mock<ISensor>();
    }

    [Test]
    [TestCase(16)]
    [TestCase(10)]
    public void AlarmGoesOnIfPressureLevelBelowLowPressureThreshHold
        (double pressureLevel)
    {
        fakeSensor
            .Setup(s => s.PopNextPressurePsiValue())
            .Returns(pressureLevel);

        alarm = new Alarm(fakeSensor.Object);

        alarm.Check();

        Assert.That(alarm.AlarmOn, Is.EqualTo(true));
    }

    [Test]
    [TestCase(22)]
    [TestCase(28)]
    [TestCase(50)]
    public void AlarmGoesOnIfPressureLevelAboveHighPressureTreshHold
        (double pressureLevel)
    {
        fakeSensor
            .Setup(s => s.PopNextPressurePsiValue())
            .Returns(pressureLevel);

        alarm = new Alarm(fakeSensor.Object);

        alarm.Check();

        Assert.That(alarm.AlarmOn, Is.EqualTo(true));
    }

    [Test]
    [TestCase(LowPressureThreshold)]
    [TestCase(18)]
    [TestCase(20)]
    [TestCase(HighPressureThreshold)]
    public void AlarmDoesNotGoOnIfPressureIsWithinRange
        (double pressureLevel)
    {
        fakeSensor
            .Setup(s => s.PopNextPressurePsiValue())
            .Returns(pressureLevel);

        alarm = new Alarm(fakeSensor.Object);

        alarm.Check();

        Assert.That(alarm.AlarmOn, Is.EqualTo(false));
    }

    [Test]
    [TestCase(-1)]
    public void CheckThrowsExceptionIfGivenPressureIsNegative
        (double pressureLevel)
    {
        fakeSensor
           .Setup(s => s.PopNextPressurePsiValue())
           .Returns(pressureLevel);

        alarm = new Alarm(fakeSensor.Object);
        
        Assert.That(() => alarm.Check(), Throws.Exception);
    }
}
