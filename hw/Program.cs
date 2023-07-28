using hw;

var meetings = new List<Meeting>();
while (true)
{
    Console.WriteLine("\nВыберите действие:");
    Console.WriteLine("1. Добавить встречу");
    Console.WriteLine("2. Посмотреть расписание на день");
    Console.WriteLine("3. Экспортировать расписание на день в файл");
    Console.WriteLine("4. Выйти");

    var choice = Console.ReadLine();
    Console.Clear();

    switch (choice)
    {
        case "1":
            AddMeeting(meetings);
            break;
        case "2":
            ShowSchedule(meetings);
            break;
        case "3":
            ExportScheduleToFile(meetings);
            break;
        case "4":
            Console.WriteLine("До свидания!");
            return;
        default:
            Console.WriteLine("Некорректный выбор. Попробуйте еще раз.");
            break;
    }
}

static void AddMeeting(List<Meeting> meetings)
{
    Console.WriteLine("Введите название встречи:");
    var title = Console.ReadLine();

    Console.WriteLine("Введите время начала встречи (например, 14:30):");
    if (!DateTime.TryParse(Console.ReadLine(), out var startTime))
    {
        Console.WriteLine("Ошибка ввода времени. Встреча не добавлена.");
        return;
    }

    Console.WriteLine("Введите примерное время окончания встречи (например, 15:30):");
    if (!DateTime.TryParse(Console.ReadLine(), out var endTime))
    {
        Console.WriteLine("Ошибка ввода времени. Встреча не добавлена.");
        return;
    }

    Console.WriteLine("Введите время напоминания о встрече (в минутах перед началом):");
    if (!int.TryParse(Console.ReadLine(), out var reminderMinutes))
    {
        Console.WriteLine("Ошибка ввода времени напоминания. Встреча не добавлена.");
        return;
    }

    if (title == null) return;
    var newMeeting = new Meeting(title, startTime, endTime, reminderMinutes);
    if (IsMeetingTimeValid(newMeeting, meetings))
    {
        meetings.Add(newMeeting);
        Console.WriteLine("Встреча успешно добавлена.");
    }
    else
    {
        Console.WriteLine("Ошибка. Время встречи пересекается с другой встречей.");
    }
}

static void ShowSchedule(List<Meeting> meetings)
{
    Console.WriteLine("Введите день для просмотра расписания (например, 2023-07-28):");
    if (!DateTime.TryParse(Console.ReadLine(), out var selectedDate))
    {
        Console.WriteLine("Ошибка ввода даты. Попробуйте еще раз.");
        return;
    }

    Console.WriteLine($"\nРасписание на {selectedDate.ToShortDateString()}:");
    foreach (var meeting in meetings.Where(meeting => meeting.StartTime.Date == selectedDate.Date))
    {
        Console.WriteLine(meeting);
    }
}

static void ExportScheduleToFile(List<Meeting> meetings)
{
    Console.WriteLine("Введите день для экспорта расписания (например, 2023-07-28):");
    if (!DateTime.TryParse(Console.ReadLine(), out var selectedDate))
    {
        Console.WriteLine("Ошибка ввода даты. Попробуйте еще раз.");
        return;
    }

    var fileName = $"Schedule_{selectedDate.ToShortDateString()}.txt";
    using var writer = new StreamWriter(fileName);
    writer.WriteLine($"Расписание на {selectedDate.ToShortDateString()}:");

    foreach (var meeting in meetings.Where(meeting => meeting.StartTime.Date == selectedDate.Date))
    {
        writer.WriteLine(meeting);
    }

    Console.WriteLine($"Расписание успешно экспортировано в файл '{fileName}'.");
}

static bool IsMeetingTimeValid(Meeting newMeeting, List<Meeting> meetings)
{
    foreach (var meeting in meetings)
    {
        if (newMeeting.StartTime >= meeting.StartTime && newMeeting.StartTime < meeting.EndTime)
            return false;
        if (newMeeting.EndTime > meeting.StartTime && newMeeting.EndTime <= meeting.EndTime)
            return false;
    }

    return true;
}