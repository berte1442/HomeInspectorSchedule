using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace HomeInspectorSchedule
{
    public class Report
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [NotNull, Unique]
        public string FilePath { get; set; }
        [NotNull]
        public DateTime timeStamp { get; set; }
        [NotNull]
        public string FileName { get; set; }
        [NotNull]
        public int InspectorID { get; set; }
    }
}
