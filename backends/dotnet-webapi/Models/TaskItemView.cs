using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_webapi
{
    public class TaskItemView : TaskItem
    {
        public TaskItemView(TaskItem task)
        {
            this.CompletedOn = task.CompletedOn;
            this.ID = task.ID;
            this.IsClosed = task.IsClosed;
            this.Status = task.Status;
            this.Text = task.Text;
        }

        public List<TaskItemView> ChildItems { get; set; }

        public int CompletedCount
        {
            get
            {
                int completedCount = 0;
                if (ChildItems != null) // Child items will not have child items set. The check is therefore necessary
                {
                    foreach (var childItem in ChildItems)
                    {
                        if (childItem.CompletedOn != null)
                            completedCount++;
                    }
                }
                if (completedCount == 0 && this.CompletedOn != null)
                    completedCount++;
                return completedCount;
            }
        }

        public string MostRecentCompletedOnDate
        {
            get
            {
                DateTime? mostRecent = null;
                if (ChildItems != null)
                {
                    foreach (var childItem in ChildItems)
                    {
                        if (childItem.CompletedOn != null && (mostRecent == null || childItem.CompletedOn > mostRecent))
                            mostRecent = childItem.CompletedOn;
                    }
                }
                if (this.CompletedOn != null && (mostRecent == null || this.CompletedOn > mostRecent))
                    mostRecent = this.CompletedOn;
                if (mostRecent == null)
                    return "";
                return mostRecent.Value.ToString("ddd yyyy-MM-dd");
            }
        }

        public string CompletedOnDate
        {
            get
            {
                if (CompletedOn == null)
                    return "";
                return CompletedOn.Value.ToString("ddd yyyy-MM-dd");
            }
        }

        public string Html
        {
            get
            {
                return this.Text;
            }
        }
    }
}
