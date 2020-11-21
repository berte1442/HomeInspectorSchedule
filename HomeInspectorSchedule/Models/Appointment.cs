using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace HomeInspectorSchedule
{
    public class Appointment
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [NotNull]
        public int InspectorID { get; set; }
        [NotNull]
        public int ClientID { get; set; }
        public int RealtorID { get; set; }
        [NotNull]
        public string InspectionTypeIDs { get; set; }
        [NotNull]
        public double PriceTotal { get; set; }
        [NotNull]
        public DateTime StartTime { get; set; }
        [NotNull]
        public double Duration { get; set; }
        [NotNull]
        public bool Paid { get; set; }
        [NotNull]
        public int AddressID { get; set; }
        public string Notes { get; set; }
    }
}
