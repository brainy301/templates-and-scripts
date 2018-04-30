using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_webapi
{
    public class TaskItemByDateView
    {
        public string Date { get; set; }
        public List<TaskItemView> Tasks { get; set; }
        public int CompletedCount { get; set; }
    }
}
