﻿using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;

namespace HomeInspectorSchedule
{
    public class Client : Person
    {
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
