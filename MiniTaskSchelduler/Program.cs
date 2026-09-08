using MiniTaskSchelduler.Models;
using MiniTaskSchelduler.Persistence;

namespace MiniTaskSchelduler
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            //JsonTaskRepository repository = new JsonTaskRepository(); // could work
            ITaskRepository repository = new JsonTaskRepository(); //we only need to see the interface (with only 2 methods) doesn't matter whats inside
            List<ScheduledTask> tasks = repository.Load(); //Load contains deserialize to ScheduledTask type

            var cts = new CancellationTokenSource();

            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                eventArgs.Cancel = true; // brutal exit
                cts.Cancel();            // just cancel
                Console.WriteLine("Exiting...");
            };

            if (tasks.Count ==0) { 
            tasks.Add(new ScheduledTask("Creating backup", 10));
            tasks.Add(new ScheduledTask("Testing", 15));
            tasks.Add(new ScheduledTask("Executing", 20));
            }
            foreach (var task in tasks)
            {
                Console.WriteLine($"Task: {task.Name}, in every {task.IntervalSeconds} seconds, next run: {task.NextRun}");
                
            }
            await RunLoopAsync(tasks, cts.Token);
            repository.Save(tasks); //save
            //location in ...\MiniTaskSchedulerProject\MiniTaskSchelduler\bin\Debug\net10.0
            //since its part of gitignore it won't be published since it a "RunTime data" not source code



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