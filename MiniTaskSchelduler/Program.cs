using MiniTaskSchelduler.Models;

namespace MiniTaskSchelduler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<ScheduledTask> tasks = new List<ScheduledTask>();
            tasks.Add(new ScheduledTask("Creating backup", 10));
            tasks.Add(new ScheduledTask("Testing", 10));
            tasks.Add(new ScheduledTask("Executing", 10));

            foreach (var task in tasks)
            {
                Console.WriteLine($"Task: {task.Name}, in every {task.IntervalSeconds} seconds, next run: {task.NextRun}");
            }


        }
    }
}