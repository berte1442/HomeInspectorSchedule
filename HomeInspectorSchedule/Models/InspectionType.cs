using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace HomeInspectorSchedule
{
    public class InspectionType
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [NotNull]
        public string Name { get; set; }
        [NotNull]
        public double Price { get; set; }
        public string Description { get; set; }
        [NotNull]
        public double DurationHours { get; set; }
    }
}
