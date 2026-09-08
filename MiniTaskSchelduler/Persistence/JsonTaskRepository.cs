using MiniTaskSchelduler.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace MiniTaskSchelduler.Persistence
{
    internal class JsonTaskRepository : ITaskRepository
    {
        private readonly string _filePath = "tasks.json";

        public void Save(List<ScheduledTask> tasks)
        {
            string json = JsonSerializer.Serialize(tasks); //all task to JSON form
            File.WriteAllText(_filePath, json); //writing
        }

        public List<ScheduledTask> Load()
        {
            if (!File.Exists(_filePath)){ //exists?
                return new List<ScheduledTask>();
            } else {
                string json = File.ReadAllText(_filePath);
                List<ScheduledTask> tasks = JsonSerializer.Deserialize<List<ScheduledTask>>(json);
                return tasks ?? new List<ScheduledTask>(); //if null create empty
            }
        }    

    }
}
