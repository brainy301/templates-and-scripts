﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_webapi
{
    public class ScheduleItem : ItemId
    {
        public DateTime Date { get; set; }
        public string Text { get; set; }
    }
}
