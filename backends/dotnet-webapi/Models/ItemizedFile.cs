using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_webapi
{
    public class ItemizedFile
    {
		public string Name { get; set; }
		public string Path { get; set; }
		public int Version { get; set; }
		public bool IsEncrypted { get; set; }
		public string Body { get; set; }
		public List<Item> Items { get; set;}
    }
}
