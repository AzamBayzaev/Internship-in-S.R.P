using System.Linq;

namespace term;

public class TaskTracker
{
    private readonly Dictionary<int, TaskItem> _tasks = new();
    private readonly INotifier _notifier;

    public TaskTracker(INotifier notifier)
    {
        _notifier = notifier;
    }

    public void AddTask(string title, Priority priority)
    {
        var task = new TaskItem(title, priority);
        _tasks.Add(task.Id, task);

        if (task.Priority == Priority.High)
            _notifier.Notify(task);
    }

    public void ListTasks()
    {
        if (_tasks.Count == 0)
        {
            Console.WriteLine("No tasks.");
            return;
        }

        foreach (var t in _tasks.Values)
        {
            Console.WriteLine(
                $"Id: {t.Id} | Title: {t.Title} | Priority: {t.Priority} | Status: {t.Status}"
            );

            if (t.Tags.Count > 0)
                Console.WriteLine("Tags: " + string.Join(", ", t.Tags));
        }
    }

    public void CompleteTask(int id)
    {
        if (_tasks.TryGetValue(id, out var task))
            task.IsDone = true;
        else
            Console.WriteLine("Task not found.");
    }
    
    public void ShowReports()
    {
        Console.WriteLine("\n===== REPORTS =====");

        var openTasks = _tasks.Values
            .Where(t => !t.IsDone)
            .OrderByDescending(t => t.Priority)
            .ThenBy(t => t.CreatedAt);

        Console.WriteLine("\n-- Open Tasks (sorted) --");
        foreach (var t in openTasks)
        {
            Console.WriteLine($"#{t.Id} [{t.Priority}] {t.Title} — {t.Status}");
        }

        var groupedByPriority = _tasks.Values
            .GroupBy(t => t.Priority);

        Console.WriteLine("\n-- Tasks by Priority --");
        foreach (var group in groupedByPriority)
        {
            Console.WriteLine($"{group.Key}: {group.Count()}");
        }

        var groupedByStatus = _tasks.Values
            .GroupBy(t => t.Status);

        Console.WriteLine("\n-- Tasks by Status --");
        foreach (var group in groupedByStatus)
        {
            Console.WriteLine($"{group.Key}: {group.Count()}");
        }

        bool anyOldTasks = _tasks.Values
            .Any(t => t.CreatedAt < DateTime.UtcNow.AddDays(-7));

        Console.WriteLine("\n-- Overdue  --");
        Console.WriteLine(anyOldTasks ? "There are overdue tasks!" : "No overdue tasks.");
        Console.WriteLine("====================\n");
    }
}