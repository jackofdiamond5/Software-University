using System;

public class DateModifier
{
    public static int CalcuateDifferenceBetweenDates(DateTime firstDate, DateTime secondDate)
    {
        return firstDate > secondDate ? (firstDate - secondDate).Days : (secondDate - firstDate).Days;
    }
}