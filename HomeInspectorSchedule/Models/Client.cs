using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace HomeInspectorSchedule
{
    public class Client
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [NotNull]
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
