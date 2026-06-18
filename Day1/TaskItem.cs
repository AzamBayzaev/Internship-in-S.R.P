namespace term;

public class TaskItem
{
    private static int _counter;

    public int Id { get; }
    public string Title { get; }
    public Priority Priority { get; }
    public bool IsDone { get; set; }
    public DateTime CreatedAt { get; }

    public HashSet<string> Tags { get; } = new();

    public string Status => IsDone ? "Done" : "Open";

    public TaskItem(string title, Priority priority)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty");

        Id = ++_counter;
        Title = title;
        Priority = priority;
        CreatedAt = DateTime.UtcNow;
    }
    
}
