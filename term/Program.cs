using term;

INotifier notifier = new ConsoleNotifier();
var tracker = new TaskTracker(notifier);

while (true)
{
    Console.WriteLine("\n1) Add task");
    Console.WriteLine("2) List tasks");
    Console.WriteLine("3) Complete task");
    Console.WriteLine("4) Reports");
    Console.WriteLine("5) Exit");

    var input = Console.ReadLine();

    switch (input)
    {
        case "1":
            Console.Write("Title: ");
            var title = Console.ReadLine();

            Console.WriteLine("Priority: 0=Low, 1=Medium, 2=High");
            var pInput = Console.ReadLine();

            if (!int.TryParse(pInput, out int p))
            {
                Console.WriteLine("Invalid priority");
                break;
            }

            tracker.AddTask(title!, (Priority)p);
            break;

        case "2":
            tracker.ListTasks();
            break;

        case "3":
            Console.Write("Enter task id: ");
            var idInput = Console.ReadLine();

            if (int.TryParse(idInput, out int id))
            {
                tracker.CompleteTask(id);
            }
            break;

        case "4":
            tracker.ShowReports();
            break;

        case "5":
            return;

        default:
            Console.WriteLine("Wrong option");
            break;
    }
}