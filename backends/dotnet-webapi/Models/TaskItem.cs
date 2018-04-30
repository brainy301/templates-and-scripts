using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_webapi
{
    public class TaskItem : ItemId
    {
        public Guid? ParentID { get; set; }
        public string Text { get; set; }
        public TaskStatuses Status { get; set; }
        public DateTime? CompletedOn { get; set; }
        public bool IsClosed { get; set; }
    }
}
