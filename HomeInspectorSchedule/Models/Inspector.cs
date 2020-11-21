using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace HomeInspectorSchedule
{
    public class Inspector
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [Unique, NotNull]
        public string Name { get; set; }
        [NotNull]
        public bool Admin { get; set; }
        [Unique, NotNull]
        public string UserName { get; set; }
        [NotNull]
        public string Password { get; set; }
        [Unique, NotNull]
        public string InspectorColor { get; set; }
    }
}
