using System;
using System.Collections.Generic;
using System.Text;

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

        public ScheduledTask( string name, int intervalSeconds)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.IntervalSeconds = intervalSeconds;
            this.LastRun = null;
        }
    }
}
