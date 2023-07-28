namespace hw;

public class Meeting
{
    private string Title { get; }
    public DateTime StartTime { get; }
    public DateTime EndTime { get; }
    private int ReminderMinutes { get; }

    public Meeting(string title, DateTime startTime, DateTime endTime, int reminderMinutes)
    {
        Title = title;
        StartTime = startTime;
        EndTime = endTime;
        ReminderMinutes = reminderMinutes;
    }

    public override string ToString()
    {
        return $"{Title} | {StartTime.ToShortTimeString()} - {EndTime.ToShortTimeString()} | Напомнить за {ReminderMinutes} мин.";
    }
}