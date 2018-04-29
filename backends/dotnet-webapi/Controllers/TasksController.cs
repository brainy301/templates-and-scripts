using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace dotnet_webapi.Controllers
{
    [Route("api/[controller]")]
    public class TasksController : Controller
    {
		// GET api/tasks/ping
        [HttpGet("ping")]
        public string Ping()
        {
            return $"Hello. The time is {DateTime.Now.ToString()}";
        }
        
        // GET api/tasks/getalltasks
        [HttpGet("getalltasks")]
        public IEnumerable<TaskItem> GetAllTasks(string filename)
        {
            var file = ItemsManager.GetFiles().FirstOrDefault(f => f.Name == filename);
            if (file == null)
                return null;
            if (!file.IsEncrypted)
                ItemsManager.LoadFile(filename, null);
            return file.Items.Where(i => i.ItemType == ItemTypes.Task)
                .Select(i => i.ItemObject as TaskItem)
                .ToList();
        }

        // GET api/tasks/getopentasks
        [HttpGet("getopentasks")]
        public IEnumerable<TaskItem> GetOpenTasks(string filename)
        {
            var file = ItemsManager.GetFiles().FirstOrDefault(f => f.Name == filename);
            if (file == null)
                return null;
            if (!file.IsEncrypted)
                ItemsManager.LoadFile(filename, null);
            return file.Items.Where(i => i.ItemType == ItemTypes.Task && (i.ItemObject as TaskItem).IsClosed == false)
                .Select(i => i.ItemObject as TaskItem)
                .ToList();
        }

        // GET api/tasks/getclosedtasks
        [HttpGet("getclosedtasks")]
        public IEnumerable<TaskItem> GetClosedTasks(string filename)
        {
            var file = ItemsManager.GetFiles().FirstOrDefault(f => f.Name == filename);
            if (file == null)
                return null;
            if (!file.IsEncrypted)
                ItemsManager.LoadFile(filename, null);
            return file.Items.Where(i => i.ItemType == ItemTypes.Task && (i.ItemObject as TaskItem).IsClosed == true)
                .Select(i => i.ItemObject as TaskItem)
                .ToList();
        }

        // GET api/tasks/addtask
        [HttpGet("addtask")]
        public Item AddTask(string filename, string task)
        {
            var file = ItemsManager.GetFiles().FirstOrDefault(f => f.Name == filename);
            if (file == null)
                return null;
            Guid id = Guid.NewGuid();
            var item = new Item()
            {
                ID = id,
                DateCreated = DateTime.Now,
                ItemType = ItemTypes.Task,
                ItemObject = new TaskItem()
                {
                    ID = id,
                    Status = TaskStatuses.New,
                    Text = task
                }
            };
            file.Items.Add(item);
            return item;
        }

        // GET api/tasks/addtask
        [HttpGet("completetask")]
        public bool CompleteTask(string filename, string itemid)
        {
            var file = ItemsManager.GetFiles().FirstOrDefault(f => f.Name == filename);
            if (file == null)
                return false;
            var item = file.Items.FirstOrDefault(i => i.ID == Guid.Parse(itemid));
            if (item == null)
                return false;
            if (item.ItemType != ItemTypes.Task)
                return false;
            var taskItem = item.ItemObject as TaskItem;
            taskItem.CompletedOn = DateTime.Now;
            taskItem.IsClosed = true;
            return true;
        }

    }
}
