namespace term;

public class ConsoleNotifier : INotifier
{
    public void Notify(TaskItem task)
    {
        Console.WriteLine($"[NOTIFY] High priority task created: {task.Title}");
    }
}   