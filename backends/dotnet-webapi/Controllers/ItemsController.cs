using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace dotnet_webapi.Controllers
{
    [Route("api/[controller]")]
    public class ItemsController : Controller
    {
		// GET api/items/ping
        [HttpGet("ping")]
        public string Ping()
        {
            return $"Hello. The time is {DateTime.Now.ToString()}";
        }

        [HttpGet("currentdirectory")]
        public string CurrentDirectory()
        {
            return System.IO.Directory.GetCurrentDirectory();
        }
        
        // GET api/items/getfiles
        [HttpGet("getfiles")]
        public IEnumerable<ItemizedFile> GetFiles()
        {
			return ItemsManager.GetFiles().Select(file=> new ItemizedFile(){
				Name = file.Name,
				Version = file.Version,
				IsEncrypted = file.IsEncrypted
			});
        }

        // GET api/items/getitems
        [HttpGet("getallitems")]
        public IEnumerable<Item> GetAllItems(string name)
        {
            var file = ItemsManager.GetFiles().FirstOrDefault(f => f.Name == name);
            if (file == null)
                return null;
            return file.Items;
        }

        // GET api/items/loadfile
        [HttpGet("loadfile")]
        public bool LoadFile(string name, string password)
        {
			return ItemsManager.LoadFile(name, password);
        }

        // GET api/items/reloadfile
        [HttpGet("reloadfile")]
        public bool ReloadFile(string name, string password)
        {
            return ItemsManager.LoadFile(name, password, true);
        }


        // GET api/items/savefile
        [HttpGet("savefile")]
        public bool SaveFile(string name, string password)
        {
            return ItemsManager.SaveFile(name, password);
        }

        // GET api/items/deleteitem
        [HttpGet("deleteitem")]
        public bool DeleteItem(string name, string itemid)
        {
            var file = ItemsManager.GetFiles().FirstOrDefault(f => f.Name == name);
            if (file == null)
                return false;
            var item = file.Items.FirstOrDefault(i => i.ID == Guid.Parse(itemid));
            if (item == null)
                return false;
            file.Items.Remove(item);
            return true;
        }
    }
}
