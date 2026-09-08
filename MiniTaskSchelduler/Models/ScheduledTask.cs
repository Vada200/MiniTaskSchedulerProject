using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MiniTaskSchelduler.Models
{
    internal class ScheduledTask
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public int IntervalSeconds { get; set; }

        public DateTime? LastRun { get; set; } //? - nullable

        public DateTime NextRun
        {
            get {
                if (LastRun.HasValue)
                    return LastRun.Value.AddSeconds(IntervalSeconds);
                else
                    return DateTime.Now;
            }
        }

        public ScheduledTask( string name, int intervalSeconds) //contructor for new task
        {
            this.Id = Guid.NewGuid(); //new ID every run. problematic
            this.Name = name;
            this.IntervalSeconds = intervalSeconds;
            this.LastRun = null;
        }

        [JsonConstructor] //JsonSerializer.Deserialize() using this contructor
        public ScheduledTask(Guid id, string name, int intervalSeconds, DateTime? lastRun)
        {
            this.Id = id;   //static ID from the JSON file
            this.Name = name;
            this.IntervalSeconds = intervalSeconds;
            this.LastRun = lastRun;
        }
    }
}
