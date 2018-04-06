using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

public class MathTester
{
    [Test]
    public void Abs_ReturnsModuleOfGivenNumber()
    {
        var numberToTest = -3;

        Assert.That(() => Math.Abs(numberToTest),
            Is.GreaterThan(0));
    }

    [Test]
    public void Floor_RoundsToLowerBoundary()
    {
        var numberToTest = 5.9;
        var expectedResult = 5;

        Assert.That(() => Math.Floor(numberToTest),
            Is.EqualTo(expectedResult));
    }
}
