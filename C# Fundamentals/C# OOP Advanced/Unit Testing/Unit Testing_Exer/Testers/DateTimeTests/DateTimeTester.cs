using System;

using Moq;
using NUnit.Framework;

public class DateTimeTester
{
    Mock<IDateTime> fakeDateTime;

    [SetUp]
    public void CreateDateTimeObj()
    {
        fakeDateTime = new Mock<IDateTime>();
    }

    [TearDown]
    public void DestroyDateTimeObj()
    {
        fakeDateTime = null;
    }

    [Test]
    [TestCase(2013, 11, 25)]
    [TestCase(2016, 03, 07)]
    [TestCase(2023, 12, 11)]
    [TestCase(2012, 02, 28)] // Adding a day to a leap year => 28.02 -> 29.02
    public void AddingADayToTheMiddleOfTheMonthIncreasesDateByOne
        (int year, int month, int date)
    {
        var valueToTest = 1;
        var expectedResult = date + valueToTest;

        fakeDateTime
             .Setup(fdt => fdt.GetDateTimeNow())
             .Returns(new DateTime(year, month, date));

        var dateTimeHandler = new DateTimeHandler(fakeDateTime.Object);
        var finalDate = dateTimeHandler.IncreaseDays(valueToTest);

        Assert.That(finalDate.Day,
            Is.EqualTo(expectedResult));
    }

    [Test]
    [TestCase(2011, 06, 30)]
    [TestCase(2012, 10, 31)]
    [TestCase(2007, 02, 28)]
    public void AddingADayToTheLastDayOfTheMonthMovesToTheNextMonth
        (int year, int month, int date)
    {
        var valueToTest = 1;
        var expectedResult = month + 1;

        fakeDateTime
            .Setup(fdt => fdt.GetDateTimeNow())
            .Returns(new DateTime(year, month, date));

        var dateTimeHandler = new DateTimeHandler(fakeDateTime.Object);
        var finalDate = dateTimeHandler.IncreaseDays(valueToTest);

        Assert.That(finalDate.Month,
            Is.EqualTo(expectedResult));
    }

    [Test]
    [TestCase(2001, 01, 02)]
    [TestCase(2063, 10, 23)]
    public void AddingANegativeValueRevertsDateBackwards
       (int year, int month, int date)
    {
        var valueToTest = -1;
        var expectedResult = date - 1;

        fakeDateTime
            .Setup(fdt => fdt.GetDateTimeNow())
            .Returns(new DateTime(year, month, date));

        var dateTimeHandler = new DateTimeHandler(fakeDateTime.Object);
        var finalDate = dateTimeHandler.IncreaseDays(valueToTest);

        Assert.That(finalDate.Day,
            Is.EqualTo(expectedResult));
    }

    [Test]
    [TestCase(2001, 12, 01)]
    [TestCase(2010, 03, 01)]
    public void AddingANegativeValueRevertsMonthBackwards
       (int year, int month, int date)
    {
        var valueToTest = -1;
        var expectedResult = month - 1;

        fakeDateTime
            .Setup(fdt => fdt.GetDateTimeNow())
            .Returns(new DateTime(year, month, date));

        var dateTimeHandler = new DateTimeHandler(fakeDateTime.Object);
        var finalDate = dateTimeHandler.IncreaseDays(valueToTest);

        Assert.That(finalDate.Month,
            Is.EqualTo(expectedResult));
    }

    [Test]
    [TestCase(2001, 01, 01)]
    [TestCase(2012, 01, 01)]
    public void AddingANegativeValueRevertsYearBackwards
       (int year, int month, int date)
    {
        var valueToTest = -1;
        var expectedResult = year - 1;

        fakeDateTime
            .Setup(fdt => fdt.GetDateTimeNow())
            .Returns(new DateTime(year, month, date));

        var dateTimeHandler = new DateTimeHandler(fakeDateTime.Object);
        var finalDate = dateTimeHandler.IncreaseDays(valueToTest);

        Assert.That(finalDate.Year,
            Is.EqualTo(expectedResult));
    }

    [Test]
    [TestCase(2011, 01, 31)]
    [TestCase(2013, 02, 28)]
    public void AddingADayTotheLastDayOfTheMonthIncreasesTheMonthByOne
      (int year, int month, int date)
    {
        var valueToTest = 1;
        var expectedResult = month + 1;

        fakeDateTime
            .Setup(fdt => fdt.GetDateTimeNow())
            .Returns(new DateTime(year, month, date));

        var dateTimeHandler = new DateTimeHandler(fakeDateTime.Object);
        var finalDate = dateTimeHandler.IncreaseDays(valueToTest);

        Assert.That(finalDate.Month,
            Is.EqualTo(expectedResult));
    }

    [Test]
    [TestCase(2012, 12, 31)]
    [TestCase(2025, 12, 31)]
    public void AddingADayTotheLastDayOfTheYearIncreasesTheYearByOne
    (int year, int month, int date)
    {
        var valueToTest = 1;
        var expectedResult = year + 1;

        fakeDateTime
            .Setup(fdt => fdt.GetDateTimeNow())
            .Returns(new DateTime(year, month, date));

        var dateTimeHandler = new DateTimeHandler(fakeDateTime.Object);
        var finalDate = dateTimeHandler.IncreaseDays(valueToTest);

        Assert.That(finalDate.Year,
            Is.EqualTo(expectedResult));
    }

    [Test]
    [TestCase(1)]
    [TestCase(500)]
    public void AddingDaysToDateTimeMaxThrowsException(int daysToAdd)
    {
        var dateTimeMax = DateTime.MaxValue;

        Assert.That(() => dateTimeMax.AddDays(daysToAdd),
            Throws.Exception);
    }

    [Test]
    [TestCase(1)]
    [TestCase(500)]
    public void SubstractingDaysFromDateTimeMinThrowsException
        (int daysToSubstract)
    {
        var dateTimeMin = DateTime.MinValue;

        Assert.That(() => dateTimeMin.AddDays(-daysToSubstract),
            Throws.Exception);
    }

    [Test]
    [TestCase(1)]
    [TestCase(0)]
    public void AddingDaysToDateTimeMinDoesNotThrowException
        (int daysToAdd)
    {
        var expectedValue = DateTime.MinValue.Day + daysToAdd;

        var dateTimeMin = DateTime.MinValue;
        var result = dateTimeMin.AddDays(daysToAdd);

        Assert.That(result.Day,
            Is.EqualTo(expectedValue));
    }

    [Test]
    [TestCase(1)]
    [TestCase(0)]
    public void SubstractingDaysFromDateTimeMaxDoesNotThrowException
       (int daysToSubstract)
    {
        var expectedValue = DateTime.MaxValue.Day - daysToSubstract;

        var dateTimeMin = DateTime.MaxValue;
        var result = dateTimeMin.AddDays(-daysToSubstract);

        Assert.That(result.Day,
            Is.EqualTo(expectedValue));
    }
}
