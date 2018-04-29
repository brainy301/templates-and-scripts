using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_webapi
{
    public class Item
    {
        public Guid ID { get; set; }
        public DateTime DateCreated { get; set; }
        public ItemTypes ItemType { get; set; }
		public string ItemBody { get; set; }
		public object ItemObject { get; set; }
    }
}
