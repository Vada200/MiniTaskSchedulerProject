using MiniTaskSchelduler.Models;

namespace MiniTaskSchelduler
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            var cts = new CancellationTokenSource();

            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                eventArgs.Cancel = true; // brutal exit
                cts.Cancel();            // just cancel
                Console.WriteLine("Exiting...");
            };

            List<ScheduledTask> tasks = new List<ScheduledTask>();
            tasks.Add(new ScheduledTask("Creating backup", 10));
            tasks.Add(new ScheduledTask("Testing", 15));
            tasks.Add(new ScheduledTask("Executing", 20));

            foreach (var task in tasks)
            {
                Console.WriteLine($"Task: {task.Name}, in every {task.IntervalSeconds} seconds, next run: {task.NextRun}");
                
            }
            await RunLoopAsync(tasks, cts.Token);

        }

        private static async Task RunLoopAsync(List<ScheduledTask> tasks, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                foreach (var task in tasks)
                {
                    if (task.NextRun <= DateTime.Now)
                    {
                        Console.WriteLine($"Task ended: {task.Name}");
                        task.LastRun = DateTime.Now;
                    }
                }
                await Task.Delay(1000);
            }
        }
    }
}