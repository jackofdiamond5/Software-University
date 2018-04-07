using System;

public class DateTimeHandler
{
    private IDateTime dateTimeHelper;

    public DateTimeHandler(IDateTime dateTime)
    {
        this.dateTimeHelper = dateTime;
    }

    public DateTime IncreaseDays(int value)
    {
        return
            this.dateTimeHelper.GetDateTimeNow().AddDays(value);
    }
}

