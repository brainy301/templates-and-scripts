using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace dotnet_webapi
{
    public class ItemsManager
    {
		private static List<ItemizedFile> _files;		
		
		public static List<ItemizedFile> GetFiles()
        {
			if (_files == null)
				_files = new List<ItemizedFile>();
			
			string[] filePaths = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Files", "*.itf");
			foreach (string filePath in filePaths)
			{
				if (_files.Any(f=> f.Path == filePath))
					continue;
				
				string text = File.ReadAllText(filePath, System.Text.Encoding.UTF8);
				int newlineIndex = text.IndexOf("\n");
				if (newlineIndex == -1)
					continue;
							
				ItemizedFile file = new ItemizedFile();
				file.Name = Path.GetFileName(filePath);
				file.Path = filePath;
				
				// Header
				string header = text.Substring(0, newlineIndex);				
				string[] parts = header.Split(';');
				for (int i=0; i<parts.Length; i++)
				{
					if (i==0)
						file.Version = int.Parse(parts[i]);
					else if (i==1)
						file.IsEncrypted = bool.Parse(parts[i]);
				}
				
				// Body
				string body = text.Substring(newlineIndex+1).Trim();
				file.Body = body;
				
				_files.Add(file);
			}
			return _files;
        }
		
		public static bool LoadFile(string name, string password)
		{
			var file = ItemsManager.GetFiles().FirstOrDefault(f=> f.Name == name);
			if (file == null)
				return false;
			if (file.Items != null)
				return true;
			if (!string.IsNullOrEmpty(password))
			{
				try
				{
					file.Body = EncryptionManager.DecryptString(file.Body, password);
				}
				catch 
				{
					return false;
				}
				
			}
            if (string.IsNullOrEmpty(file.Body))
            {
                file.Items = new List<Item>();
                return true;
            }
			file.Items = JsonConvert.DeserializeObject<List<Item>>(file.Body);
			foreach (var item in file.Items)
			{
				switch (item.ItemType)
				{
					case ItemTypes.ScheduledItem:
						item.ItemObject = JsonConvert.DeserializeObject<ScheduleItem>(item.ItemBody);
						break;
					case ItemTypes.Task:
						item.ItemObject = JsonConvert.DeserializeObject<TaskItem>(item.ItemBody);
						break;					
					default:
                        break;
				}
			}
			return true;
		}

        public static bool SaveFile(string name, string password)
        {
            var file = ItemsManager.GetFiles().FirstOrDefault(f => f.Name == name);
            if (file == null)
                return false;

            // Check if file has been loaded
            if (file.Items == null)
                return false;

            // Convert the items back to JSON
            foreach (var item in file.Items)
            {
                item.ItemBody = JsonConvert.SerializeObject(item.ItemObject);
            }

            using (StreamWriter sw = new StreamWriter(file.Path, false, System.Text.Encoding.UTF8))
            {
                bool isEncrypted = !string.IsNullOrEmpty(password);
                string header = file.Version.ToString() + ";" + isEncrypted.ToString().ToLower();
                sw.WriteLine(header);
                string itemsJson = JsonConvert.SerializeObject(file.Items);
                if (isEncrypted)
                    itemsJson = EncryptionManager.EncryptString(itemsJson, password);
                sw.Write(itemsJson);
            }
            return true;
        }
    }
}
