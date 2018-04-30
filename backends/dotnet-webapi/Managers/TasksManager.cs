using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_webapi
{
    public class TasksManager
    {
        public static List<TaskItemView> GetFirstLevelItems(ItemizedFile file, bool? closed)
        {
            if (file.Items == null)
                return null;
            return file.Items.Where(i => i.ItemType == ItemTypes.Task
                && (closed == null || (i.ItemObject as TaskItem).IsClosed == closed.Value)
                && (i.ItemObject as TaskItem).ParentID == null)
                .Select(i => new TaskItemView(i.ItemObject as TaskItem))
                .ToList();
        }

        public static void FilterClosedItems(List<TaskItemView> items)
        {
            for (int i=0; i<items.Count; i++)
            {
                var item = items[i];
                if (item.IsClosed == false)
                {
                    if (!item.ChildItems.Any(ii=> ii.IsClosed))
                    {
                        items.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        public static void SetChildItems(ItemizedFile file, List<TaskItemView> items, bool? closed)
        {
            if (items == null)
                return;
            foreach (var item in items)
            {
                item.ChildItems = file.Items.Where(i => i.ItemType == ItemTypes.Task
                    && (i.ItemObject as TaskItem).ParentID == item.ID
                    && (closed == null || (i.ItemObject as TaskItem).IsClosed == closed.Value))
                    .Select(i => new TaskItemView(i.ItemObject as TaskItem))
                    .ToList();
                if (item.ChildItems == null)
                    item.ChildItems = new List<TaskItemView>();
            }
        }
    }
}
