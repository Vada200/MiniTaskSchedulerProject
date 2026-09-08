using MiniTaskSchelduler.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniTaskSchelduler.Persistence
{
    internal interface ITaskRepository //de miért interface
    {
        List<ScheduledTask> Load();
        void Save(List<ScheduledTask> tasks);
    }
}
