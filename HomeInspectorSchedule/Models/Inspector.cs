using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;


namespace HomeInspectorSchedule
{
    public class Inspector : Person
    {
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
