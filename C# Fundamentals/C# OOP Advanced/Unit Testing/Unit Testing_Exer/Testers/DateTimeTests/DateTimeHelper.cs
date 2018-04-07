using System;


public class DateTimeHelper : IDateTime
{
    public DateTime GetDateTimeNow()
    {
        return DateTime.Now;
    }
}
